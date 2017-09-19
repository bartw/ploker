using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ploker.Domain;

namespace Ploker.Server
{
    public class Croupier : Hub
    {
        private static readonly Casino _casino = new Casino();

        public override Task OnConnectedAsync()
        {
            Report();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _casino.RemovePlayer(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public Task CreateTable()
        {
            return ActAndReportStatus(async () =>
            {
                var id = _casino.CreateTable();
                var table = _casino.GetTable(id);
                table.AddPlayer(Context.ConnectionId);
                await Groups.AddAsync(Context.ConnectionId, id.ToString());
            });
        }

        public Task JoinTable(int id)
        {
            return ActAndReportStatus(async () =>
            {
                var table = _casino.GetTable(id);
                if (table != null)
                {
                    table.AddPlayer(Context.ConnectionId);
                }
                await Groups.AddAsync(Context.ConnectionId, id.ToString());
            });
        }

        public Task SetHand(int id, string value)
        {
            return ActAndReportStatus(() =>
            {
                int hand;
                if (int.TryParse(value, out hand))
                {
                    _casino.GetTable(id).SetHandFor(Context.ConnectionId, hand);
                }
                else if (string.IsNullOrEmpty(value))
                {
                    _casino.GetTable(id).SetHandFor(Context.ConnectionId, null);
                }
            });
        }

        public Task DealMeOut(int id)
        {
            return ActAndReportStatus(() => _casino.GetTable(id).DealOut(Context.ConnectionId));
        }

        public Task DealMeIn(int id)
        {
            return ActAndReportStatus(() => _casino.GetTable(id).DealIn(Context.ConnectionId));
        }

        public Task Reset(int id)
        {
            return ActAndReportStatus(_casino.GetTable(id).Reset);
        }

        private Task ActAndReportStatus(Action action)
        {
            action.Invoke();
            return Report();
        }

        private Task Report()
        {
            var tables = _casino.GetTablesFor(Context.ConnectionId);
            if (!tables.Any())
            {
                return Clients.Client(Context.ConnectionId).InvokeAsync("Status", "");
            }
            var tasks = tables.Select(table => Clients.Group(table.ToString()).InvokeAsync("Status", _casino.GetTable(table).GetStatus()));
            return Task.WhenAll(tasks);
        }
    }
}