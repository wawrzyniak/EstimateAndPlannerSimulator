using System.Collections.Generic;

namespace EstimateSimulator.Data
{
    public interface IData
    {
        int DataId { get; }
        IEnumerable<DataEntry> GetData();
        Dictionary<string, double> GetCoeffsData();
    }
}