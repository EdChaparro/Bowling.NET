using System;
using System.Collections.Generic;
using System.Linq;

namespace intrepidproducts.bowling
{
    public interface IGame
    {
        int Score { get; }
        
        IFrame this[int frameNbr] { get; }

        bool Add(IFrame frame);
        int LastFrameNumberCompleted { get; }
        
        bool IsFinished { get; }
    }

    public class Game : IGame
    {
        private readonly List<IFrame> _frames = new List<IFrame>();

        public int Score
        {
            get
            {
                return _frames.Sum(x => x.Score);
            }
        }
        public IFrame Frame(int frameNbr)
        {
            if ((frameNbr < 0)  || (frameNbr > (_frames.Count - 1)))
            {
                throw new IndexOutOfRangeException();
            }

            return _frames[frameNbr];
        }

        public IFrame this[int frameNbr] => Frame(frameNbr);

        public bool Add(IFrame frame)
        {
            if (_frames.Count == 10)
            {
                return false;
            }

            _frames.Add(frame);
            return true;
        }

        public int LastFrameNumberCompleted
        {
            get
            {
                var frame = _frames.LastOrDefault(x => x.IsComplete);

                if (frame == null)
                {
                    return -1;
                }

                return _frames.LastIndexOf(frame);
            }
        }

        public bool IsFinished
        {
            get
            {
                return _frames.Any() && _frames.All(x => x.IsComplete);
            }
        }
    }
}