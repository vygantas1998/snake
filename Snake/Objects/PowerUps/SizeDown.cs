using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class SizeDown : Size
    {
        public SizeDown(int x, int y)
        {
            X = x;
            Y = y;
            Points = -50;
        }
        public override void Eat(SnakeBody snake)
        {
            BodyPart lastPart = snake.BodyParts.Last();
            snake.BodyParts.Remove(lastPart);
            if(snake.BodyParts.Count < 1)
            {
                snake.isDead = true;
            }
        }
    }
}
