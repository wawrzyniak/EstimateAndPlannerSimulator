using System;
using System.Collections.Generic;

namespace EstimateSimulator.Manager
{
    public interface IManagerConfig
    {
        int NumberOfWorkers { get; }
        int SimulationInterval { get; }

        IEnumerable<Tuple<string,string>> DataSources { get; }
        
    }
}