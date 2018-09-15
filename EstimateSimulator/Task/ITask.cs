

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
        private static int GlobalCounter = 1;
        public int EstimatedTime { get; set; }
        public int RealTime { get; set; }
        public int ElapsedTime { get; private set; }
        private int _id;
        public bool Simulate(int step)
        {
            ElapsedTime += step;
            if (ElapsedTime >= RealTime)
                return true;
            else
                return false;
        }

        public BasicTask()
        {
            _id = GlobalCounter;
            GlobalCounter++;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
