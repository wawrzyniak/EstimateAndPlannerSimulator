using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateSimulator.Task;

namespace EstimateSimulator.Planner
{
    public interface IPlan
    {
        Dictionary<ITask, int> Plan { get; }
    }

    public class BasicPlan : IPlan
    {
        public Dictionary<ITask, int> Plan { get; }

        public BasicPlan()
        {
            Plan = new Dictionary<ITask, int>();
        }

    }
}
