using System;
using System.Collections.Generic;

namespace EstimateSimulator.Data
{
    public class DataEntry
    {
        public string EntryId { get;  set; }

        public double GetValue(string columnName)
        {
            if (_data.ContainsKey(columnName))
                return _data[columnName];
            else
            {
                Console.WriteLine("DataEntry.GetValue error");
                return 0;
            }
        }

        public DataEntry()
        {
            _data= new Dictionary<string, double>();
        }

        public void AddValue(string columnName, double val)
        {
            _data.Add(columnName,val);
        }
        private Dictionary<string, double> _data;

    }
}