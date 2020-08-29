using System;

namespace intrepidproducts.bowling
{
    public interface IRoll
    {
        short PinsDownCount { get; set; }
        bool AreAllPinsDown { get; }
    }

    public class Roll : IRoll
    {
        private short _pinsDownCount;

        public short PinsDownCount
        {
            get => _pinsDownCount;

            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException();
                }

                _pinsDownCount = value;
            }
        }

        public bool AreAllPinsDown => PinsDownCount == 10;
    }
}