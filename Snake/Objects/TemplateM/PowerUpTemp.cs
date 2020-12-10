using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.TemplateM
{
    class PowerUpTemp : DrawDAO
    {
        private PowerUp Part;
        public PowerUpTemp(PowerUp part)
        {
            Part = part;
        }
        public override void SetColor()
        {
            Color = Part.Color;
        }
        public override void SetCoords()
        {
            X = Part.X;
            Y = Part.Y;
        }
    }
}
