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

            _tables.Add(id, new Table());
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

        public void RemoveTable(int id)
        {
            if (_tables.ContainsKey(id))
            {
                _tables.Remove(id);
            }
        }
    }
}