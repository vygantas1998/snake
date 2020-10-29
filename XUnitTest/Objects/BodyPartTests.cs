using LibNoise.Renderer;
using Moq;
using Snake.Objects;
using System;
using System.Drawing;
using Xunit;

namespace XUnitTest.Objects
{
    public class BodyPartTests
    {
        private MockRepository mockRepository;



        public BodyPartTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private BodyPart CreateBodyPart(string color)
        {
            var x = 5;
            var y = 5;
            return new BodyPart(x, y, color);
        }

        [Fact]
        public void getColor_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var bodyPart = this.CreateBodyPart("Red");

            // Act
            var result = bodyPart.getColor();
            // Assert
            Assert.Equal(result, Brushes.Red);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void getColor_Green()
        {
            // Arrange
            var bodyPart = this.CreateBodyPart("Green");

            // Act
            var result = bodyPart.getColor();
            // Assert
            Assert.Equal(result, Brushes.Green);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void getColor_Black()
        {
            // Arrange
            var bodyPart = this.CreateBodyPart("Black");

            // Act
            var result = bodyPart.getColor();
            // Assert
            Assert.Equal(result, Brushes.Black);
            this.mockRepository.VerifyAll();
        }
        [Fact]
        public void getColor_Default()
        {
            // Arrange
            var bodyPart = new BodyPart();

            // Act
            var result = bodyPart.getColor();
            // Assert
            Assert.Equal(result, Brushes.Black);
            this.mockRepository.VerifyAll();
        }
    }
}
