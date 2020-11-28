using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Proxy
{
    abstract class Subject
    {
        public abstract void DrawBodyPart(BodyPart part, Graphics canvas);
        public abstract void DrawPowerUp(PowerUp part, Graphics canvas);
    }
}
