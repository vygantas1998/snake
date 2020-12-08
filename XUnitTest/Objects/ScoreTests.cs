using Moq;
using Snake.Objects;
using System;
using Xunit;

namespace XUnitTest.Objects
{
    public class ScoreTests
    {
        private MockRepository mockRepository;



        public ScoreTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Score CreateScore()
        {
            int id = 1;
            int points = 5;

            return new Score(id, points);
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var score = this.CreateScore();


            // Assert
            Assert.True(score.Points == 5);
            this.mockRepository.VerifyAll();
        }
        [Fact]
        public void TestMethod0()
        {
            try
            {
                var score = new Score(5, 2);
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
