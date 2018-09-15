using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstimateSimulator.Data;
using EstimateSimulator.Planner;
using EstimateSimulator.Task;
using EstimateSimulator.Workers;

namespace EstimateSimulator.Manager
{
    public class Manager
    {
        Dictionary<int, IWorker> _workers;
        IPlanner _planner;
        ITaskCreator _taskCreator;
        private IManagerConfig _config;
        private IEnumerable<IData> _data;
        public Manager(IManagerConfig config)
        {
            _config = config;
            FillData();
            PrepareWorkers();
            PrepareTaskCreator();
            PreparePlanner();
        }

        public void RunSimulation()
        {
            bool isthereAnyTask = true;
            var tasks = _taskCreator.GetTasks(150);
            TaskCreatorOnNewTasks(tasks);
            foreach (var worker in _workers)
            {
                if (!worker.Value.IsWorking)
                {
                    WorkerEmpty();
                }
            }

            foreach (var worker in _workers)
            {
                Console.WriteLine(worker.Value.ToString());
            }

            Console.Read();
            while (isthereAnyTask)
            {
               // Console.Clear();
              
                
             

                foreach (var worker in _workers)
                {
                    worker.Value.DoWork();
                }

                isthereAnyTask = false;
                foreach (var worker in _workers)
                {
                    if (worker.Value.IsWorking)
                    {
                        isthereAnyTask = true;
                    }
                    else
                        WorkerEmpty();
                }
              
                
            }
            Console.WriteLine("Koniec");
            foreach (var worker in _workers)
            {
                Console.WriteLine("Worker numer: " + worker.Value.WorkerId);
                Console.WriteLine(worker.Value.Stats.ToString());
                Console.WriteLine(worker.Value.Stats.ErrorList.Average());
                Console.WriteLine("***************");
            }
            Console.ReadKey();


        }

        void PreparePlanner()
        {
            _planner = new DynamicPlanner();
        }
        private void FillData()
        {
            var data = new List<IData>();
            int i = 1;
            foreach (var dataSource in _config.DataSources)
            {
                var temp = new DataFromCsv();
                temp.DataId = i;
                i++;
                temp.ParseCSVData(dataSource);
                temp.ParseCSVCoeffData(_config.EstimateDataSource);
                data.Add(temp);
            }
            _data = data;
        }

        private void PrepareWorkers()
        {
            _workers = new Dictionary<int, IWorker>();
            for (int i = 0; i < _config.NumberOfWorkers; i++)
            {
                _workers.Add(i,new BasicWorker(i,_config.SimulationInterval));
            }
        }
        private void PrepareTaskCreator()
        {
            _taskCreator = new BaseTaskCreator(_data);
    
        }

        private void DoPlan(IPlan plan)
        {
            foreach (var t in plan.Plan)
            {
              _workers[t.Value].PushTask(t.Key);
            }
        }

        private void TaskCreatorOnNewTasks(IEnumerable<ITask> taskList)
        {
            var info = new WorkerInfo();
            foreach (var worker in _workers)
            {
                info.WorkersTimeToEnd.Add(worker.Key,worker.Value.GetTimeToEnd());
            }
            info.NumberOfWorker = _workers.Count;

           var plan =  _planner.PreparePlan(info, taskList);
            DoPlan(plan);

     

        }
        private void WorkerEmpty()
        {
            var info = new WorkerInfo();
            foreach (var worker in _workers)
            {
                int val = worker.Value.GetTimeToEnd();
                if (val < 0)
                    val = -1 * val;
                info.WorkersTimeToEnd.Add(worker.Key, val);
            }
            info.NumberOfWorker = _workers.Count;

            var plan = _planner.RecaulculatePlan(info);
            DoPlan(plan);



        }
    }
}

