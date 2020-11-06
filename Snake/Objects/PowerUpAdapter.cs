using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects
{
    class PowerUpAdapter : PowerUp
    {
        private AdapteePowerUp _adaptee = new AdapteePowerUp();

        public override void Eat(SnakeBody snake)
        {
            _adaptee.AddSpeed(snake, 5);
            _adaptee.AddLength(snake);
            _adaptee.ChangeColor(snake, "Teal");
        }
    }
}
