

namespace EstimateSimulator.Task
{
    public interface ITask
    {
        int EstimatedTime { get; }
        int RealTime { get; }
        int ElapsedTime { get; }
        bool Simulate(int step);
    }

    public class BasicTask : ITask
    {
        public int EstimatedTime { get; set; }
        public int RealTime { get; set; }
        public int ElapsedTime { get; private set; }
        public bool Simulate(int step)
        {
            ElapsedTime += step;
            if (ElapsedTime >= RealTime)
                return true;
            else
                return false;
        }
    }
}
