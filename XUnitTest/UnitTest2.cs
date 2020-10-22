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
    }
}
