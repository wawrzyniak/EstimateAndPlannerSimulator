using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using EstimateSimulator.Data;
using EstimateSimulator.Functions;

namespace EstimateSimulator.Task
{
    /// <summary>
    /// implementacje mają za zadanie z przygotowanych i wczytanych danych w formie listy typu IData wygenerować taski dla całej symulacji
    /// 1. dla każdego IData i zawartych tam danych i coeffs zrobić ITaska czyli wyestymować TIME
    /// 2. zgodnie z ustawienia dawać taski przez event w jakiejś formie, 
    /// </summary>
   public interface ITaskCreator
    {
        IEnumerable<ITask> GetTasks(int howMany);
        bool IsEmpty();
        //  event NewTasks NewTasks;
    }

   // public delegate void NewTasks(IEnumerable<ITask> taskList);

    public class BaseTaskCreator : ITaskCreator
    {
        private int index = 0;
        private IEstimator _estimator;
        public IEnumerable<ITask> GetTasks(int howMany)
        {
           
            if (howMany >= _allTasks.Count)
            {
                List<ITask> toret = new List<ITask>();
                while (_allTasks.Count > 0)
                {
                    toret.Add(_allTasks.Dequeue());
                }
                return toret;
            }

            else
            {
                List<ITask> toret = new List<ITask>();
                for (int i = 0; i < howMany; i++)
                {
                    toret.Add(_allTasks.Dequeue());
                }
                return toret;
            }
        }

        public bool IsEmpty()
        {
            return _allTasks.Count <= 0;
        }

        //  public event NewTasks NewTasks;
        private IEnumerable<IData> _data;
        private Queue<ITask> _allTasks;
        public BaseTaskCreator(IEnumerable<IData> data, IEstimator estimator)
        {
            _data = data;
            _estimator = estimator;
            PrepareTasks();
        }


        private void PrepareTasks()
        {
            _allTasks = new Queue<ITask>();
            foreach (var data in _data)
            {
                _estimator.SetCoeffs(data.GetCoeffsData());

                foreach (var datanerty in data.GetData())
                {
                    var task = new BasicTask();
                    task.RealTime = (int) Math.Round(datanerty.GetValue("TIME"));
                    task.EstimatedTime = (int) Math.Round(_estimator.EstimateValue(datanerty));
                
                    _allTasks.Enqueue(task);
                }
            }
        }


    }
}
