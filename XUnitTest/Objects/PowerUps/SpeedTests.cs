using Moq;
using Snake;
using Snake.Objects;
using Snake.Objects.PowerUps;
using System;
using Xunit;

namespace XUnitTest.Objects.PowerUps
{
    public class SpeedTests
    {
        private MockRepository mockRepository;



        public SpeedTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Speed CreateSpeed()
        {
            return new Speed();
        }

        [Fact]
        public void Eat_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var speed = this.CreateSpeed();
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed1 = 0;
            // Act

            var snake = new SnakeBody(
                X,
                Y,
                speed1,
                Direction.Down,
                headColor,
                bodyColor);

            // Act

            try
            {
                speed.Eat(snake);
                Assert.True(true);
            }
            catch
            {


                Assert.True(false);
            }
            this.mockRepository.VerifyAll();
        }
    }
}
