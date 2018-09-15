using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EstimateSimulator.Task;

namespace EstimateSimulator.Workers
{
    public interface IWorker
    {
        bool IsWorking { get; }
        WorkerStats Stats { get; }
        int WorkerId { get; }
        void PushTask(ITask task);
        void DoWork();
        ITask GetTaskInProgress();
        
        int GetTimeToEnd();
    }

    public class WorkerStats
    {
        public int IdleTime { get; set; }
        public int TotalWorkTime { get; set; }
        public List<int> ErrorList { get; set; }

        public WorkerStats()
        {
            ErrorList = new List<int>();
        }
        public override string ToString()
        {
            string temp = String.Format("IdleTime: {0} TotalWorkTime: {1}", IdleTime, TotalWorkTime);
            return temp;
        }
    }

    public class BasicWorker : IWorker
    {
        private int _workValue;
        private Queue<ITask> _queue;
        public bool IsWorking { get; private set; }
        public WorkerStats Stats { get; set; }
        public int WorkerId { get; private set; }
        public void PushTask(ITask task)
        {
            _queue.Enqueue(task);
        }

        public void DoWork()
        {
            if (_queue.Count == 0)
            {
                Stats.IdleTime += _workValue;
                IsWorking = false;
            }
            else
            {
                IsWorking = true;
                Stats.TotalWorkTime += _workValue;
                if (_queue.Peek().Simulate(_workValue))
                {
                    var t = _queue.Dequeue();
                    int error = t.RealTime - t.EstimatedTime;
                    Stats.ErrorList.Add(error);
                }
            }
    
        }

        public ITask GetTaskInProgress()
        {
            if (_queue.Count == 0)
            {
                return null;
            }
            else return _queue.Peek();
        }

        public int GetTimeToEnd()
        {
            int sum = 0;
            if (_queue.Count == 0)
            {
               return sum;
            }
            foreach (var task in _queue)
            {
                sum += (task.EstimatedTime - task.ElapsedTime);
            }
            return sum;
        }

        public BasicWorker(int id, int amountOfWork)
        {
            WorkerId = id;
            _queue = new Queue<ITask>();
            Stats = new WorkerStats();
            _workValue = amountOfWork;
        }
    }
}
