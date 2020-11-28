using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Proxy
{
    class DrawSubject : Subject
    {
        public override void DrawBodyPart(BodyPart part, Graphics canvas)
        {
            canvas.FillEllipse(part.getColor(),
                          new Rectangle(part.X,
                                         part.Y,
                                         16, 16));
        }

        public override void DrawPowerUp(PowerUp part, Graphics canvas)
        {
            canvas.FillEllipse(part.Color,
                          new Rectangle(part.X,
                                         part.Y,
                                         16, 16));
        }
    }
}
