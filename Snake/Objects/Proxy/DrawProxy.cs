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

        public override void DrawSnakes(Graphics canvas, Map map)
        {
            _realSubject.DrawSnakes(canvas, map);
        }
        public override void DrawPowerUps(Graphics canvas, Map map)
        {
            _realSubject.DrawPowerUps(canvas, map);
        }
    }
}
