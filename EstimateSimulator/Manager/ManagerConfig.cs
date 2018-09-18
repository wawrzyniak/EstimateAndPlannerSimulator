using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateSimulator.Manager
{
   public class ManagerConfig : IManagerConfig
    {
        public int NumberOfWorkers { get; set; }
        public int SimulationInterval { get; set; }
       public IEnumerable<string> DataSources { get; set; }
        public string EstimateDataSource { get; set; }
  
    }
}
