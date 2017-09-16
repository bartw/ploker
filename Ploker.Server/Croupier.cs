using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Ploker.Server
{
    public class Croupier : Hub
    {
        private static readonly Dictionary<string, string> hands = new Dictionary<string, string>();

        public Task Send(string value)
        {
            hands[Context.ConnectionId] = value;

            if (hands.All(hand => !string.IsNullOrWhiteSpace(hand.Value)))
            {
                return Clients.All.InvokeAsync("Send", hands);
            }
            else
            {
                return Clients.All.InvokeAsync("Send", hands.ToDictionary(hand => hand.Key, hand => ""));
            }
        }
    }
}