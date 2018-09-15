using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace EstimateSimulator.Data
{
    public class DataFromCsv : IData
    {
       List<DataEntry> _dataEntries;
        private Dictionary<string, double> _coeffData;
       public DataFromCsv()
       {
           _dataEntries = new List<DataEntry>();
            _coeffData = new Dictionary<string, double>();
       }

        public int DataId { get; set; }

        public IEnumerable<DataEntry> GetData()
       {
           return _dataEntries;
       }

        public Dictionary<string, double> GetCoeffsData()
        {
            return _coeffData;
        }

        public void ParseCSVData(string fileName)
        {
            Dictionary<int,string> _indexToColumnName = new Dictionary<int, string>();
            bool firstLine = true;
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    if (firstLine)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            _indexToColumnName.Add(i,fields[i]);
                        }
                        firstLine = false;
                    }
                    else
                    {
                        DataEntry entry = new DataEntry();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (_indexToColumnName[i] == "Id")
                            {
                                entry.EntryId = fields[i];
                            }
                            else
                            {
                                double v = 0;
                              
                                
                               var test = Double.TryParse(fields[i], NumberStyles.Any, CultureInfo.InvariantCulture, out v);
                               if(test) entry.AddValue(_indexToColumnName[i],v);
                            }
                         
                        }
                        _dataEntries.Add(entry);
                    }
          
                }
            }
        }

        public void ParseCSVCoeffData(string fileName)
        {

            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    double v = Double.Parse(fields[1],CultureInfo.InvariantCulture);
                    _coeffData.Add(fields[0],v);

                }
            }
        }
    
    
   }
}
