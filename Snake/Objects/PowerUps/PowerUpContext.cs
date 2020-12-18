using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.PowerUps
{
    class PowerUpContext

    {
        private PowerUp _strategy;

        // Constructor

        public PowerUpContext(PowerUp strategy)
        {
            this._strategy = strategy;
        }

        public void Eat(SnakeBody snake)
        {
            snake.Accept(_strategy);
        }
    }
}
