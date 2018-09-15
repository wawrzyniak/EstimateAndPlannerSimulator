using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateSimulator.Workers
{
   public class WorkerInfo
    {
       public int NumberOfWorker { get; set; }
       public Dictionary<int, int> WorkersTimeToEnd { get; }

       public WorkerInfo()
       {
           WorkersTimeToEnd = new Dictionary<int, int>();
       }


    }
}
