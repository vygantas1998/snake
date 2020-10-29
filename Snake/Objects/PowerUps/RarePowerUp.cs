using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.PowerUps
{
    class RarePowerUp: PowerUpDecorator
    {
        public override void Eat(SnakeBody snake)
        {
            base.Eat(snake);
            snake.Speed += 1;
        }
    }
}
