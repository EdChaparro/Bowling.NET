using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.Bowling.Tests
{
    [TestClass]
    public class FrameTest
    {
        IFrame _frame = new Frame();

        [TestInitialize]
        public void Initialize()
        {
            _frame = new Frame();
        }

        [TestMethod]
        public void ShouldIdentifyStrikes()
        {
            Assert.IsFalse(_frame.IsStrike);
            _frame.AddRoll(new Roll {PinsDownCount = 10});
            Assert.IsTrue(_frame.IsStrike);
            Assert.IsFalse(_frame.IsSpare);
        }

        [TestMethod]
        public void ShouldIdentifySpares()
        {
            Assert.IsFalse(_frame.IsSpare);
            _frame.AddRoll(new Roll { PinsDownCount = 9 });
            Assert.IsFalse(_frame.IsSpare);

            _frame.AddRoll(new Roll { PinsDownCount = 1 });
            Assert.IsTrue(_frame.IsSpare);
            Assert.IsFalse(_frame.IsStrike);
        }

        [TestMethod]
        public void ShouldIdentifyCompletedStrike()
        {
            Assert.IsFalse(_frame.IsComplete);

            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            Assert.IsFalse(_frame.IsComplete);
            Assert.IsTrue(_frame.IsStrike);

            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            Assert.IsFalse(_frame.IsComplete);
            Assert.IsTrue(_frame.IsStrike);

            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            Assert.IsTrue(_frame.IsComplete);
            Assert.IsTrue(_frame.IsStrike);
        }

        [TestMethod]
        public void ShouldIdentifyCompletedSpare()
        {
            Assert.IsFalse(_frame.IsComplete);

            _frame.AddRoll(new Roll { PinsDownCount = 9 });
            Assert.IsFalse(_frame.IsComplete);
            Assert.IsFalse(_frame.IsSpare);

            _frame.AddRoll(new Roll { PinsDownCount = 1 });
            Assert.IsFalse(_frame.IsComplete);
            Assert.IsTrue(_frame.IsSpare);

            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            Assert.IsTrue(_frame.IsComplete);
            Assert.IsTrue(_frame.IsSpare);
        }

        [TestMethod]
        public void ShouldCalculateStrikeScore()
        {
            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            _frame.AddRoll(new Roll { PinsDownCount = 10 });
            _frame.AddRoll(new Roll { PinsDownCount = 10 });

            Assert.AreEqual(30, _frame.Score);
        }

        [TestMethod]
        public void ShouldCalculateSpareScore()
        {
            _frame.AddRoll(new Roll { PinsDownCount = 8 });
            _frame.AddRoll(new Roll { PinsDownCount = 2 });
            _frame.AddRoll(new Roll { PinsDownCount = 5 });

            Assert.AreEqual(15, _frame.Score);
        }

        [TestMethod]
        public void ShouldCalculateRegularScore()
        {
            _frame.AddRoll(new Roll { PinsDownCount = 7 });
            _frame.AddRoll(new Roll { PinsDownCount = 2 });

            Assert.AreEqual(9, _frame.Score);
        }

        [TestMethod]
        public void ShouldRejectInvalidPinRolls()
        {
            Assert.IsTrue(_frame.AddRoll(new Roll {PinsDownCount = 7}));
            Assert.IsFalse(_frame.AddRoll(new Roll {PinsDownCount = 4}));
            Assert.IsTrue(_frame.AddRoll(new Roll { PinsDownCount = 1 }));
            Assert.IsTrue(_frame.IsComplete);
        }

        [TestMethod]
        public void ShouldOnlyPermitTwoRollsWhenNotStrikeOrSpare()
        {
            Assert.IsTrue(_frame.AddRoll(new Roll { PinsDownCount = 2 }));
            Assert.IsTrue(_frame.AddRoll(new Roll { PinsDownCount = 3}));
            Assert.IsFalse(_frame.AddRoll(new Roll { PinsDownCount = 4 }));
        }
    }
}