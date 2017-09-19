using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain
{
    public class Casino
    {
        private const int MAX_TABLES = 10;
        private readonly Dictionary<int, Table> _tables;

        public Casino()
        {
            _tables = new Dictionary<int, Table>();
        }
        public int CreateTable()
        {
            if (_tables.Count == MAX_TABLES)
            {
                throw new Exception($"Reached the maximum of {MAX_TABLES} tables.");
            }

            int id;
            do
            {
                id = new Random().Next(10000, 99999);
            } while (_tables.ContainsKey(id));

            _tables.Add(id, new Table(id));
            return id;
        }

        public Table GetTable(int id)
        {
            if (_tables.ContainsKey(id))
            {
                return _tables[id];
            }
            return null;
        }

        public void RemovePlayer(string name)
        {
            _tables.Values.ToList().ForEach(t => t.RemovePlayer(name));
            _tables.Where(kvp => kvp.Value.IsEmpty()).Select(kvp => kvp.Key).ToList().ForEach(id => _tables.Remove(id));
        }

        public IEnumerable<int> GetTablesFor(string name)
        {
            return _tables.Where(kvp => kvp.Value.HasPlayer(name)).Select(kvp => kvp.Key).ToList();
        }
    }
}