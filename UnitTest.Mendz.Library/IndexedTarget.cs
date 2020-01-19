using System;
using System.Collections.Generic;

namespace UnitTest.Mendz.Library
{
    public class IndexedTarget
    {
        private readonly Dictionary<string, object> _index = new Dictionary<string, object>();

        public object this[string key]
        {
            get => _index[key];
            set => _index[key] = value;
        }

        public IndexedTarget()
        {
            _index.Add("ID", null);
            _index.Add("Name", null);
            _index.Add("DOB", DateTime.Today);
            _index.Add("Value", 0);
            _index.Add("Worth", 0);
            _index.Add("Percentile", 0);
            _index.Add("Status", false);
        }
    }
}
