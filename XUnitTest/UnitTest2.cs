using System;
using System.Collections.Generic;
using System.Text;
using Snake.Objects.PowerUps;
using Xunit;

namespace XUnitTest
{
    public class UnitTest2
    {
        [Fact]
        public void TestSizeUp()
        {
            var buff = new BuffFactory();
            var y = buff.getPowerUp(2, 2, PowerUpType.Size);
            var x = new SizeUp(2,2);

            Assert.True(y.GetType() == x.GetType() && y.X == x.X && y.Y == x.Y);

        }

        [Fact]
        public void TestSpeedUp()
        {
            var buff = new BuffFactory();
            var y = buff.getPowerUp(2, 2, PowerUpType.Speed);
            var x = new SpeedUp(2, 2);

            Assert.True(y.GetType() == x.GetType() && y.X == x.X && y.Y == x.Y);

        }

        [Fact]
        public void TestSpeedDownn()
        {
            var buff = new DeBuffFactory();
            var y = buff.getPowerUp(2, 2, PowerUpType.Speed);
            var x = new SpeedDown(2, 2);

            Assert.True(y.GetType() == x.GetType() && y.X == x.X && y.Y == x.Y);

        }

        [Fact]
        public void TestSizeDown()
        {
            var buff = new DeBuffFactory();
            var y = buff.getPowerUp(2, 2, PowerUpType.Size);
            var x = new SizeDown(2, 2);

            Assert.True(y.GetType() == x.GetType() && y.X == x.X && y.Y == x.Y);

        }
    }
}
