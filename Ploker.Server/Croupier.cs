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
        private static readonly Table _table = new Table();

        public override Task OnConnectedAsync()
        {
            _table.AddPlayer(Context.ConnectionId);
            base.OnConnectedAsync();
            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _table.RemovePlayer(Context.ConnectionId);
            base.OnDisconnectedAsync(exception);
            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }

        public Task SetHand(string value)
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

            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }

        public Task DealMeOut()
        {
            _table.DealOut(Context.ConnectionId);
            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }

        public Task DealMeIn()
        {
            _table.DealIn(Context.ConnectionId);
            return Clients.All.InvokeAsync("Status", _table.GetStatus());
        }
    }
}