using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects
{
    public class BodyPart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }

        public BodyPart() { }
        public BodyPart(int x, int y, string color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public virtual Brush getColor()
        {
            Type t = typeof(Brushes);
            Brush b = (Brush)t.GetProperty(Color).GetValue(null, null);
            return b;
        }
    }
}
