using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.Bowling
{
    public interface IFrame
    {
        bool IsStrike { get; }
        bool IsSpare { get; }
        bool IsComplete { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zero until Frame is complete</returns>
        int Score { get; }

        bool AddRoll(IRoll roll);
    }

    public class Frame : IFrame
    {
        public Frame()
        {
            _rolls = new List<IRoll>();
        }

        private readonly List<IRoll> _rolls;

        public bool IsStrike => (_rolls.Any() && _rolls.First().AreAllPinsDown);

        public bool IsSpare
        {
            get
            {
                if (_rolls.Count < 2)
                {
                    return false;
                }

                return _rolls[0].PinsDownCount + _rolls[1].PinsDownCount == 10;
            }
        }

        public bool IsComplete =>
            (IsStrike || IsSpare)
                ? NumberOfRolls == 3    //Strikes & Spares require 3 rolls to complete
                : NumberOfRolls == 2;   //  Otherwise, standard frame has two rolls

        public int Score =>
            IsComplete
                ? _rolls.Sum(x => x.PinsDownCount)
                : 0;

        public int NumberOfRolls => _rolls.Count;

        public bool AddRoll(IRoll roll)
        {
            if (IsComplete)
            {
                return false;
            }

            if (!(IsStrike))
            {
                if (_rolls.Count == 1)
                {
                    if (_rolls[0].PinsDownCount + roll.PinsDownCount > 10)
                    {
                        return false;
                    }
                }
            }

            _rolls.Add(roll);
            return true;
        }
    }
}