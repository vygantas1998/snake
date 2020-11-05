using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class SpeedUp : Speed
    {
        public SpeedUp(int x, int y, int random)
        {
            X = x;
            Y = y;
            Points = 100;
            Random = random;
        }
        public override void Eat(SnakeBody snake)
        {
            snake.Speed += 8;
        }
    }
}
