using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class SizeUp : Size
    {
        public SizeUp(int x, int y, int random)
        {
            X = x;
            Y = y;
            Points = 100;
            Random = random;
        }
        public override void Eat(SnakeBody snake)
        {
            BodyPart lastPart = snake.BodyParts.Last();
            BodyPart newPart = new BodyPart(lastPart.X, lastPart.Y, snake.BodyColor);
            snake.BodyParts.Add(newPart);
        }
    }
}
