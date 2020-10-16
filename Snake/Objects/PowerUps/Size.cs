using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class Size : PowerUp
    {
        public Size()
        {
            Color = Brushes.Violet;
        }
        public override void Eat(SnakeBody snake) { }
    }
}
