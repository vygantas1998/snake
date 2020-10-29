using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Snake;
using Snake.Objects;
using System.Windows.Forms;

namespace XUnitTest
{
    public class SnakeTest
    {
        [Fact]
        public void TestDead()
        {
            var snake = new SnakeBody();
            var m = new Map();
            snake.isDead = true;
            try
            {
                snake.MoveSnake(m);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

    }
}
