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
            
            int handValue;
            if (int.TryParse(value, out handValue))
            {
                _table.SetHandFor(player, handValue);
            }

            return Clients.All.InvokeAsync("Send", _table.GetStatus());
        }
    }
}