using System.Collections.Generic;
using EstimateSimulator.Task;
using EstimateSimulator.Workers;

namespace EstimateSimulator.Planner
{
    public interface IPlanner
    {
        IPlan PreparePlan(WorkerInfo workerInfo, IEnumerable<ITask> taskList);
        IPlan RecaulculatePlan(WorkerInfo workerInfo);
    }

    public class TaskComparer : IComparer<ITask>
    {
        public int Compare(ITask x, ITask y)
        {
            return x.EstimatedTime.CompareTo(y.EstimatedTime);
        }
    }
}