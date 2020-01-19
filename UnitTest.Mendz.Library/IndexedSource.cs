using System;
using System.Collections.Generic;

namespace UnitTest.Mendz.Library
{
    public class IndexedSource
    {
        private readonly Dictionary<string, object> _index = new Dictionary<string, object>();

        public object this[string key]
        {
            get => _index[key];
        }

        public IndexedSource()
        {
            _index.Add("ID", 1);
            _index.Add("Name", "Test");
            _index.Add("DOB", DateTime.Today);
            _index.Add("Value", 100d);
            _index.Add("Worth", 100m);
            _index.Add("Percentile", 1f);
            _index.Add("Status", false);
        }
    }
}
