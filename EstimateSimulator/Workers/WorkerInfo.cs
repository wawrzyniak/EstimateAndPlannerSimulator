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

        public int GetMinWorker()
        {
            int i = 0;
            int min = Int32.MaxValue;
            foreach (var i1 in WorkersTimeToEnd)
            {
                if (i1.Value < min)
                {
                    min = i1.Value;
                    i = i1.Key;
                }
            }

            return i;
        }

        public WorkerInfo(WorkerInfo info)
        {
            NumberOfWorker = info.NumberOfWorker;
           WorkersTimeToEnd = new Dictionary<int, int>(info.WorkersTimeToEnd);
        }


    }
}
