using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Ploker
{
    public class Table : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}