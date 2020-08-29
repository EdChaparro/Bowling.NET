using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.Bowling.Tests
{
    [TestClass]
    public class RollTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAcceptPinCountBelowZero()
        {
            new Roll {PinsDownCount = -1};
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAcceptPinCountAboveTen()
        {
            new Roll {PinsDownCount = 11};
        }

        [TestMethod]
        public void ShouldAcceptValidPinCount()
        {
            var roll = new Roll();
            Assert.AreEqual(0, roll.PinsDownCount);

            roll.PinsDownCount = 5;
            Assert.AreEqual(5, roll.PinsDownCount);
        }

        [TestMethod]
        public void ShouldIndicateWhenAllPinsAreDown()
        {
            var roll = new Roll();
            Assert.IsFalse(roll.AreAllPinsDown);

            roll.PinsDownCount = 10;
            Assert.IsTrue(roll.AreAllPinsDown);
        }
    }
}