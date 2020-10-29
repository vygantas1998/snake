using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public abstract class Size : PowerUp
    {
        public Size()
        {
            Color = Brushes.Violet;
        }
    }
}
