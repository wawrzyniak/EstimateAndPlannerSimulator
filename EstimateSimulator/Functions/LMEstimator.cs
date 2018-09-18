using System;
using System.Collections.Generic;
using EstimateSimulator.Data;
using MathNet.Numerics.Distributions;

namespace EstimateSimulator.Functions
{
    public class LMEstimator : IEstimator
    {
        private double _std;

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

        public void SetStd(double std)
        {
            _std = std;
        }

        public int EstimateOverTime(int estimatedtime)
        {
            double mean = estimatedtime;
            MathNet.Numerics.Distributions.Normal normalDist = new Normal(mean, _std);
            double d = mean + 1.0;
            double sum = 0;
            while(true)
            {
                double density =  normalDist.Density(d);
                if (density < 1e-10)
                {
                    break;
                    
                }

                sum += d * density;
                d = d + 1.0;
            }

            sum = Math.Round(sum, 0);
            return (int) sum;
        }
    }
}