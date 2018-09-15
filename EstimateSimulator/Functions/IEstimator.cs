using System.Collections.Generic;
using EstimateSimulator.Data;

namespace EstimateSimulator.Functions
{
   public interface IEstimator
   {
       double EstimateValue(DataEntry metrics);
       void SetCoeffs(Dictionary<string, double> coeffData);
   }

    public class LMEstimator : IEstimator
    {
        private Dictionary<string, double> _coeffData;
        public double EstimateValue(DataEntry metrics)
        {
            double estimate = _coeffData["(Intercept)"];
            foreach (var d in _coeffData)
            {
                if (d.Key != "(Intercept)")
                {
                    estimate += d.Value*metrics.GetValue(d.Key);
                }
            }
            return estimate;

        }

        public void SetCoeffs(Dictionary<string, double> coeffData)
        {
            _coeffData = coeffData;
        }
    }
}
