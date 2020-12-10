using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.TemplateM
{
    class BodyTemp: DrawDAO
    {
        private BodyPart Body;
        public BodyTemp(BodyPart body)
        {
            Body = body;
        }
        public override void SetColor()
        {
            Color = Body.getColor();
        }
        public override void SetCoords()
        {
            X = Body.X;
            Y = Body.Y;
        }
    }
}
