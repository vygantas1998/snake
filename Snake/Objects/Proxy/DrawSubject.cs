using Snake.Objects.Iterator;
using Snake.Objects.TemplateM;
using System.Drawing;

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
            PowerUpIterator i = map.PowerUps.CreateIterator();
            PowerUp powerUp = i.First();
            while (powerUp != null)
            {
                DrawPowerUp(powerUp, canvas);
                powerUp = i.Next();
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
