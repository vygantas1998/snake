using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects
{
    public abstract class PowerUp
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Points { get; set; }
        public Brush Color { get; set; }
        public PowerUp() { }
        public abstract void Eat(SnakeBody snake);
    }
}
