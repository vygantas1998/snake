using System;
using Xunit;
using Snake.Objects.Levels;

namespace XUnitTest
{
    public class LevelTests
    {
        [Fact]
        public void TestEasy()
        {
            var easy = new Easy();
            Level level;
            level = LevelFactory.CreateLevel("Easy");
            Assert.Equal(easy.levelType, level.levelType);
        }

        [Fact]
        public void TestMedium()
        {
            var medium = new Medium();
            Level level1;
            level1 = LevelFactory.CreateLevel("Medium");
            Assert.Equal(medium.levelType, level1.levelType);
        }

        [Fact]
        public void TestHard()
        {
            var hard = new Hard();
            Level level2;
            level2 = LevelFactory.CreateLevel("Hard");
            Assert.Equal(hard.levelType, level2.levelType);
        }
    }
}
