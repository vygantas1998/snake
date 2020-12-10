using Snake.Objects.TemplateM;
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
        public override void DrawSnakes(Graphics canvas, Map map)
        {
            foreach (SnakeBody snake in map.Snakes)
            {
                foreach (BodyPart part in snake.BodyParts)
                {
                   DrawBodyPart(part, canvas);
                }
            }
        }
        public override void DrawPowerUps(Graphics canvas, Map map)
        {
            foreach (PowerUp powerUp in map.PowerUps)
            {
               DrawPowerUp(powerUp, canvas);
            }
        }
        public void DrawBodyPart(BodyPart part, Graphics canvas)
        {
            DrawDAO bodyDao = new BodyTemp(part);
            bodyDao.Run(canvas);
        }

        public void DrawPowerUp(PowerUp part, Graphics canvas)
        {
            DrawDAO partDao = new PowerUpTemp(part);
            partDao.Run(canvas);
        }
    }
}
