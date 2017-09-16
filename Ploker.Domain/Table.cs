using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain
{
    public class Table
    {
        private readonly Dictionary<string, int?> _players;

        public Table()
        {
            _players = new Dictionary<string, int?>();
        }

        public IReadOnlyDictionary<string, string> GetStatus()
        {
            if (!_players.All(p => p.Value.HasValue))
            {
                return _players.ToDictionary(p => p.Key, p => p.Value.HasValue ? "X" : "");    
            }
            return _players.ToDictionary(p => p.Key, p => p.Value.HasValue ? p.Value.ToString() : "");
        }

        public void AddPlayer(string name)
        {
            if (!_players.ContainsKey(name))
            {
                _players.Add(name, null);
            }
        }

        public void SetHandFor(string player, int? value)
        {
            if (_players.ContainsKey(player))
            {
                _players[player] = value;
            }
        }
    }
}
