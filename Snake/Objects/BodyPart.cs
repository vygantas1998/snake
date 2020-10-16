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

        public void Draw(Graphics canvas, Map map)
        {
            canvas.FillEllipse(getColor(),
                          new Rectangle(X * map.Width,
                                         Y * map.Height,
                                         map.Width, map.Height));
        }

        public Brush getColor()
        {
            switch (Color)
            {
                case "Black":
                    return Brushes.Black;
                case "Green":
                    return Brushes.Green;
                case "Red":
                    return Brushes.Red;
                default:
                    return Brushes.Black;
            }
        }
    }
}
