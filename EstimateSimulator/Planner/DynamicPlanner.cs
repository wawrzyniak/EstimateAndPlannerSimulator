using System;
using System.Collections.Generic;
using System.Linq;
using EstimateSimulator.Functions;
using EstimateSimulator.Task;
using EstimateSimulator.Workers;

namespace EstimateSimulator.Planner
{
    public class DynamicPlanner : IPlanner
    {
        private LinkedList<ITask> _taskList;

        public DynamicPlanner(IEstimator estimator)
        {
            _taskList = new LinkedList<ITask>();
        }
        public IPlan PreparePlan(WorkerInfo workerInfo, IEnumerable<ITask> taskList)
        {
            foreach (var task in taskList)
            {
                _taskList.AddLast(task);
            }
            BasicPlan plan = new BasicPlan();

            int currentWorker = workerInfo.GetMinWorker();
            
            ITask result = null;
            int min = Int32.MaxValue;
            foreach (var task in _taskList)
            {
                int singlefitnessval = FitnessFunction(workerInfo, currentWorker, task);
                if (singlefitnessval < min)
                {
                    min = singlefitnessval;
                    result = task;
                }

            }

            if (result != null)
            {
                Console.WriteLine("Winner: " + result.ToString() + " MIN: " + min);
                plan.Plan.Add(result, currentWorker);
                _taskList.Remove(result);
            }
            
        







            return plan;

        }

        public IPlan RecaulculatePlan(WorkerInfo workerInfo)
        {
            BasicPlan plan = new BasicPlan();

            int currentWorker = workerInfo.GetMinWorker();
            ITask result = null;
            int min = Int32.MaxValue;
            foreach (var task in _taskList)
            {
                int singlefitnessval = FitnessFunction(workerInfo, currentWorker, task);
                if (singlefitnessval < min)
                {
                    min = singlefitnessval;
                    result = task;
                }

            }

            if (result != null)
            {
                Console.WriteLine("Winner: " + result.ToString() + " MIN: " + min);
                plan.Plan.Add(result, currentWorker);
                _taskList.Remove(result);
            }








            return plan;
        }

        private int FitnessFunction(WorkerInfo workerInfo, int currentIndex, ITask toAdd)
        {
            var tempInfo =  new WorkerInfo(workerInfo);

            tempInfo.WorkersTimeToEnd[currentIndex] += toAdd.EstimatedTime;
            int max = tempInfo.WorkersTimeToEnd.Max(x => x.Value);
            int sum = 0;
            foreach (var val in tempInfo.WorkersTimeToEnd.Values)
            {
                sum = sum + (max - val);
            }


            return sum;
        }
    }
}