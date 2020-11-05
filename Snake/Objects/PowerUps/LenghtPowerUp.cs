using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.PowerUps
{
    class LenghtPowerUp : PowerUpDecorator
    {
        public override void Eat(SnakeBody snake)
        {
            base.Eat(snake);
            snake.BodyParts.Add(new BodyPart(snake.BodyParts[0].X, snake.BodyParts[0].Y, snake.BodyColor));
            //bodyParts.Add(new BodyPart(x, y, headColor));
        }
    }
}
