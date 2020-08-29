using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.Bowling.Tests
{
    [TestClass]
    public class GameTest
    {
        private IGame _game;
        private IList<Frame> _frames;

        [TestInitialize]
        public void Initialize()
        {
            _game = new Game();
            _frames = new List<Frame>();

            for (int i = 0; i < 10; i++)
            {
                _frames.Add(new Frame());
            }
        }

        [TestMethod]
        public void ShouldOnlyAllowTenFrames()
        {
            foreach (var frame in _frames)
            {
                Assert.IsTrue(_game.Add(frame));
            }

            Assert.IsFalse(_game.Add(new Frame()));
        }

        [TestMethod]
        public void ShouldScorePerfectGame()
        {
            Assert.IsFalse(_game.IsFinished);

            var expectedGameScore = 0;

            foreach (var frame in _frames)
            {
                frame.AddRoll(new Roll {PinsDownCount = 10});
                frame.AddRoll(new Roll {PinsDownCount = 10});
                frame.AddRoll(new Roll {PinsDownCount = 10});

                _game.Add(frame);

                expectedGameScore += 30;
                Assert.AreEqual(expectedGameScore, _game.Score);
            }

            Assert.IsTrue(_game.IsFinished);
        }

        [TestMethod]
        public void ShouldScoreAsEachFrameIsCompleted()
        {
            Assert.IsFalse(_game.IsFinished);

            var frame1 = new Frame();
            _game.Add(frame1);

            frame1.AddRoll(new Roll {PinsDownCount = 5});
            Assert.AreEqual(0, _game.Score);

            frame1.AddRoll(new Roll { PinsDownCount = 0 });
            Assert.AreEqual(5, _game.Score);
        }

        [TestMethod]
        public void ShouldRetrieveFrameByIndex()
        {
            var frame1 = new Frame();
            _game.Add(frame1);

            frame1.AddRoll(new Roll { PinsDownCount = 5 });

            Assert.AreEqual(frame1, _game[0]);
        }

        [TestMethod]
        public void ShouldKnowLastCompletedFrame()
        {
            Assert.AreEqual(-1, _game.LastFrameNumberCompleted);

            var frame1 = new Frame();
            _game.Add(frame1);
            Assert.AreEqual(-1, _game.LastFrameNumberCompleted);

            frame1.AddRoll(new Roll { PinsDownCount = 5 });
            Assert.AreEqual(-1, _game.LastFrameNumberCompleted);

            frame1.AddRoll(new Roll { PinsDownCount = 0 });
            Assert.AreEqual(0, _game.LastFrameNumberCompleted);
        }
    }
}