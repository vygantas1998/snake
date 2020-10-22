using Moq;
using Snake.Objects.Levels;
using System;
using Xunit;

namespace XUnitTest.Objects.Levels
{
    public class LevelFactoryTests
    {
        private MockRepository mockRepository;



        public LevelFactoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private LevelFactory CreateFactory()
        {
            return new LevelFactory();
        }

        [Fact]
        public void CreateLevel_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var factory = this.CreateFactory();
            string level = "Easy";

            // Act
            Level result = LevelFactory.CreateLevel(level);

            // Assert
            Assert.True(result.levelType == level);
            this.mockRepository.VerifyAll();
        }
    }
}
