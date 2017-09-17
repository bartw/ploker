using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain
{
    internal class Player
    {
        public string Name { get; private set; }
        public int? Hand { get; set; }
        public bool DealtIn { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = null;
            DealtIn = true;
        }

        public void Fold()
        {
            Hand = null;
        }
    }

    public class PlayerStatus
    {
        public string Name { get; private set; }
        public string Hand { get; private set; }

        public PlayerStatus(string name, string hand)
        {
            Name = name;
            Hand = hand;
        }
    }

    public class Table
    {
        private readonly List<Player> _players;

        public Table()
        {
            _players = new List<Player>();
        }

        public IEnumerable<PlayerStatus> GetStatus()
        {
            if (_players.All(p => p.Hand.HasValue))
            {
                return _players.Select(p => new PlayerStatus(p.Name, p.Hand.ToString()));
            }
            return _players.Select(p => new PlayerStatus(p.Name, p.Hand.HasValue ? "X" : ""));
        }

        public void AddPlayer(string name)
        {
            if (!_players.Any(p => p.Name == name))
            {
                _players.Add(new Player(name));
            }
        }

        public void SetHandFor(string player, int? hand)
        {
            if (_players.Any(p => p.Name == player))
            {
                _players.Single(p => p.Name == player).Hand = hand;
            }
        }

        public void RemovePlayer(string player)
        {
            var toRemove = _players.Find(p => p.Name == player);
            if (toRemove != null)
            {
                _players.Remove(toRemove);
            }
        }

        public void DealOut(string player)
        {
            RemovePlayer(player);
        }

        public void DealIn(string player)
        {
            AddPlayer(player);
        }

        public void Reset()
        {
            _players.ForEach(p => p.Fold());
        }
    }
}
