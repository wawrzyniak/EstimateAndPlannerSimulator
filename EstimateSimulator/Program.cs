using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateSimulator.Data;
using EstimateSimulator.Manager;

namespace EstimateSimulator
{
   public class Program
    {
        static void Main(string[] args)
        {
          //  var temp = new DataFromCsv();
          //  temp.ParseCSVData(@"D:\doktorat\junit412.csv");

           // temp.ParseCSVCoeffData(@"C:\Users\wawrzyiak\Documents\dupa");
            ManagerConfig config = new ManagerConfig();
            config.SimulationInterval = 1;
            config.NumberOfWorkers = 4;
            var sources = new List<Tuple<string, string>>();
            sources.Add(new Tuple<string, string>(@"D:\doktorat\junit412.csv", @"C:\Users\wawrzyiak\Documents\dupa"));
            config.DataSources = sources;

            var manager = new Manager.Manager(config);
            manager.RunSimulation();


        }
    }
}
