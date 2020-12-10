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
        public abstract void DrawSnakes(Graphics canvas, Map map);
        public abstract void DrawPowerUps(Graphics canvas, Map map);
    }
}
