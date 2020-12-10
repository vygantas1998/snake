using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.TemplateM
{
    abstract class DrawDAO
    {
        protected Brush Color;
        protected int X;
        protected int Y;

        public abstract void SetColor();
        public abstract void SetCoords();

        public virtual void Draw(Graphics canvas)
        {
            canvas.FillEllipse(Color, new Rectangle(X,Y,16, 16));
        }


        public void Run(Graphics canvas)
        {
            SetColor();
            SetCoords();
            Draw(canvas);
        }
    }
}
