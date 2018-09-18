namespace EstimateSimulator.Functions
{
    public class testmath
    {
        public void test()
        {
            double mean = 15000;
            double stdDev = 760087;

            var esti = new LMEstimator();
            esti.SetStd(stdDev);
            var vv = esti.EstimateOverTime(15000);
        }

    }
}