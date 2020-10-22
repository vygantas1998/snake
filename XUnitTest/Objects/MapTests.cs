using Moq;
using Snake;
using System;
using Xunit;
using System.Drawing;
using Snake.Objects;
using System.Windows.Forms;
using Snake.Objects.PowerUps;

namespace XUnitTest.Objects
{
    public class MapTests
    {
        private MockRepository mockRepository;



        public MapTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Map CreateMap()
        {
            return Map.GetInstance;
        }

        [Fact]
        public void addSnake_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed = 0;
            // Act

            var snake = map.addSnake(
                X,
                Y,
                headColor,
                bodyColor,
                speed);

            // Assert
            Assert.True(snake.BodyParts[0].X == X && snake.BodyParts[0].Y == Y && snake.BodyParts[0].Color == headColor && snake.BodyColor == bodyColor && snake.Speed == speed);
            this.mockRepository.VerifyAll();
        }


        [Fact]
        public void eatFood_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            int snake = 0;
            PowerUp powerUp = new SizeUp(5, 5);
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed = 0;
            // Act

            var snake1 = map.addSnake(
                X,
                Y,
                headColor,
                bodyColor,
                speed);

            map.Snakes.Add(snake1);
            int bodyParts = map.Snakes[snake].BodyParts.Count;
            // Act
            map.eatFood(
                snake,
                powerUp);

            // Assert
            Assert.True(bodyParts == map.Snakes[snake].BodyParts.Count-1);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void addScore_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            int snake = 0;
            int points = 5;
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed = 0;
            // Act

            var snake1 = map.addSnake(
                X,
                Y,
                headColor,
                bodyColor,
                speed);
            map.Snakes.Add(snake1);
            // Act
            map.addScore(
                snake,
                points);

            // Assert
            Assert.True(map.Scores[snake].Points == points);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void checkForFood_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            // Act            
            try
            {
                map.checkForFood();
                Assert.True(true);
            }
            catch
            {

                Assert.True(false);
            }
            // Assert
            
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void MoveSnakes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();

            // Act            
            try
            {
                map.MoveSnakes();
                Assert.True(true);
            }
            catch
            {

                Assert.True(false);
            }

            // Assert
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void addFood_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            int x = 0;
            int y = 0;
            bool isBuff = false;
            PowerUpType powerUpType = default(global::Snake.Objects.PowerUps.PowerUpType);
            AbstractPowerUpFactory powerUpFactory = PowerUpFactoryProducer.getFactory(isBuff);
            PowerUp sizePowerUp = powerUpFactory.getPowerUp(x, y, powerUpType);
            // Act
            map.addFood(
                x,
                y,
                isBuff,
                powerUpType);

            // Assert
            Assert.True(map.PowerUps[0].GetType() == sizePowerUp.GetType() && map.PowerUps[0].X == sizePowerUp.X && map.PowerUps[0].Y == sizePowerUp.Y);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void changeSnakeDirection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            int snakeId = 0;
            Direction direction = Direction.Right;
            int X = 0;
            int Y = 0;
            string headColor = null;
            string bodyColor = null;
            int speed = 0;
            // Act

            var snake1 = map.addSnake(
                X,
                Y,
                headColor,
                bodyColor,
                speed);
            map.Snakes.Add(snake1);
            // Act
            map.changeSnakeDirection(
                snakeId,
                direction);

            // Assert
            Assert.True(map.Snakes[0].Direction == direction);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void getDirection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            string direction = "Right";

            // Act
            var result = Map.getDirection(
                direction);

            // Assert
            Assert.True(result == Direction.Right);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void getStringDirection_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();
            Direction direction = Direction.Left;

            // Act
            var result = Map.getStringDirection(
                direction);

            // Assert
            Assert.True(result == "Left");
            this.mockRepository.VerifyAll();
        }


        [Fact]
        public void ClearMap_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var map = this.CreateMap();

            // Act
            try
            {
                map.ClearMap();
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
