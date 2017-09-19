using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain
{
    internal class Player
    {
        public string Name { get; private set; }
        public int? Hand { get; set; }
        public bool SittingOut { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = null;
            SittingOut = false;
        }

        public void Fold()
        {
            Hand = null;
        }
    }

    public class TableStatus
    {
        public int Id { get; }
        public IEnumerable<PlayerStatus> Players { get; }

        public TableStatus(int id, IEnumerable<PlayerStatus> players)
        {
            Id = id;
            Players = players;
        }
    }

    public class PlayerStatus
    {
        public string Name { get; }
        public string Hand { get; }
        public bool SittingOut { get; }

        public PlayerStatus(string name, string hand, bool sittingOut)
        {
            Name = name;
            Hand = hand;
            SittingOut = sittingOut;
        }
    }

    public class Table
    {
        private readonly int _id;
        private readonly List<Player> _players;

        public Table(int id)
        {
            _id = id;
            _players = new List<Player>();
        }

        public TableStatus GetStatus()
        {
            var GetHand = GetHandFunc();
            return new TableStatus(_id, _players.Select(p => new PlayerStatus(p.Name, GetHand(p), p.SittingOut)));
        }

        private Func<Player, string> GetHandFunc()
        {
            if (_players.All(p => p.Hand.HasValue))
            {
                return p => p.Hand.ToString();
            }
            return p => p.Hand.HasValue ? "X" : "";
        }

        public void AddPlayer(string name)
        {
            if (!_players.Any(p => p.Name == name))
            {
                _players.Add(new Player(name));
            }
        }

        internal bool HasPlayer(string name)
        {
            return _players.Any(p => p.Name == name);
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
            var toDealOut = _players.Find(p => p.Name == player);
            if (toDealOut != null)
            {
                toDealOut.Fold();
                toDealOut.SittingOut = true;
            }
        }

        public void DealIn(string player)
        {
            var toDealIn = _players.Find(p => p.Name == player);
            if (toDealIn != null)
            {
                toDealIn.SittingOut = false;
            }
        }

        public void Reset()
        {
            _players.ForEach(p => p.Fold());
        }

        public bool IsEmpty()
        {
            return _players.Count == 0;
        }
    }
}
