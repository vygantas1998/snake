using Moq;
using Snake.Objects;
using System;
using Xunit;
using System.Windows.Forms;
using Snake;
using System.Drawing;
using MiNET.Plugins;

namespace XUnitTest.Objects
{
    public class SnakeBodyTests
    {
        private MockRepository mockRepository;



        public SnakeBodyTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private SnakeBody CreateSnakeBody()
        {
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed = 0;
            // Act

            var snake1 = new SnakeBody(
                X,
                Y,
                speed,
                Direction.Down,
                headColor,
                bodyColor);
            return snake1;
        }


        [Fact]
        public void MoveSnake_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var snakeBody = this.CreateSnakeBody();
            Map map = new Map();

            // Act
            try
            {
                snakeBody.MoveSnake(map);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }

            // Assert
            this.mockRepository.VerifyAll();
        }

    }
}
