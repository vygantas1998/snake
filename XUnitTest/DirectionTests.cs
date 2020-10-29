using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Snake;
using System.Net.WebSockets;

namespace XUnitTest
{
    public class DirectionTests
    {
        [Fact]
        public void TestDirectionDown()
        {            
            Direction direction = Direction.Down;
            var x = Map.getStringDirection(direction);
            Assert.Same(x, "Down");
        }

        [Fact]
        public void TestDirectionUp()
        {
            Direction direction = Direction.Up;
            var x = Map.getStringDirection(direction);
            Assert.Same(x, "Up");
        }

        [Fact]
        public void TestDirectionLeft()
        {
            Direction direction = Direction.Left;
            var x = Map.getStringDirection(direction);
            Assert.Same(x, "Left");
        }

        [Fact]
        public void TestDirectionRight()
        {
            Direction direction = Direction.Right;
            var x = Map.getStringDirection(direction);
            Assert.Same(x, "Right");
        }

        [Fact]
        public void TestUpDirection()
        {
            Direction direction = Direction.Up;
            var x = Map.getDirection("Up");
            Assert.Equal(direction, x);
        }
        [Fact]
        public void TestDownDirection()
        {
            Direction direction = Direction.Down;
            var x = Map.getDirection("Down");
            Assert.Equal(direction, x);
        }
        [Fact]
        public void TestRightDirection()
        {
            Direction direction = Direction.Right;
            var x = Map.getDirection("Right");
            Assert.Equal(direction, x);
        }
        [Fact]
        public void TestLeftDirection()
        {
            Direction direction = Direction.Left;
            var x = Map.getDirection("Left");
            Assert.Equal(direction, x);
        }
    }
}
