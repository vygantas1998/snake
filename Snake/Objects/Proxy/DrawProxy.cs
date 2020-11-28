using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Proxy
{
    class DrawProxy : Subject
    {
        private DrawSubject _realSubject = new DrawSubject();

        public override void DrawBodyPart(BodyPart part, Graphics canvas)
        {
            _realSubject.DrawBodyPart(part, canvas);
        }
        public override void DrawPowerUp(PowerUp part, Graphics canvas)
        {
            _realSubject.DrawPowerUp(part, canvas);
        }
    }
}
