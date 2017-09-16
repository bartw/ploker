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

        public Task Send(string value)
        {
            var player = Context.ConnectionId;
            _table.AddPlayer(Context.ConnectionId);

            int hand;
            if (int.TryParse(value, out hand))
            {
                _table.SetHandFor(player, hand);
            }

            return Clients.All.InvokeAsync("Send", _table.GetStatus());
        }
    }
}