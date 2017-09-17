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
        private static readonly Table _table = new Table();

        public override Task OnConnectedAsync()
        {
            return ActAndReportStatus(() =>
            {
                _table.AddPlayer(Context.ConnectionId);
                base.OnConnectedAsync();
            });
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return ActAndReportStatus(() =>
            {
                _table.RemovePlayer(Context.ConnectionId);
                base.OnDisconnectedAsync(exception);
            });
        }

        public Task SetHand(string value)
        {
            return ActAndReportStatus(() =>
            {
                int hand;
                if (int.TryParse(value, out hand))
                {
                    _table.SetHandFor(Context.ConnectionId, hand);
                }
                else if (string.IsNullOrEmpty(value))
                {
                    _table.SetHandFor(Context.ConnectionId, null);
                }
            });
        }

        public Task DealMeOut()
        {
            return ActAndReportStatus(() => _table.DealOut(Context.ConnectionId));
        }

        public Task DealMeIn()
        {
            return ActAndReportStatus(() => _table.DealIn(Context.ConnectionId));
        }

        public Task Reset()
        {
            return ActAndReportStatus(_table.Reset);
        }

        private Task ActAndReportStatus(Action action)
        {
            action.Invoke();
            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }
    }
}