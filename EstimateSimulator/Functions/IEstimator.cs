using System.Collections.Generic;
using EstimateSimulator.Data;

namespace EstimateSimulator.Functions
{
   public interface IEstimator
   {
       double EstimateValue(DataEntry metrics);
       void SetCoeffs(Dictionary<string, double> coeffData);
       int EstimateOverTime(int estimatedtime);
       void SetStd(double std);
   }
}
