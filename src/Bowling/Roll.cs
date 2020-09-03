using System;

namespace IntrepidProducts.Bowling
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
                    throw new ArgumentException("Pin Count must be between 0 and 10, inclusive");
                }

                _pinsDownCount = value;
            }
        }

        public bool AreAllPinsDown => PinsDownCount == 10;
    }
}