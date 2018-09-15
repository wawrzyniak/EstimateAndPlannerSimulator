using System;
using System.Collections.Generic;
using EstimateSimulator.Task;
using EstimateSimulator.Workers;

namespace EstimateSimulator.Planner
{
    public class SimpleNaivePlanner : IPlanner
    {

        public IPlan PreparePlan(WorkerInfo workerInfo, IEnumerable<ITask> taskList)
        {
            Dictionary< int, int> workersTime =new Dictionary<int,int>(workerInfo.WorkersTimeToEnd);
            var plan = new BasicPlan();
            var tasks = new List<ITask>(taskList);
            tasks.Sort(new TaskComparer());
            foreach (var task in tasks)
            {
                var minIndex = GetMin(workersTime);
                plan.Plan.Add(task,minIndex);
                workersTime[minIndex] += task.EstimatedTime;
            }

            return plan;
        }

        public IPlan RecaulculatePlan(WorkerInfo workerInfo)
        {
            throw new NotImplementedException();
        }


        private int GetMin(Dictionary<int, int> workersTime)
        {
            int min = Int32.MaxValue;
            int toret = 0;
            foreach (var i in workersTime)
            {
                if (i.Value < min)
                {
                    min = i.Value;
                    toret = i.Key;
                }
                    

            }
            return toret;
        }
    }
}