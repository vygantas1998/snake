using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects
{
    class AdapteePowerUp
    {
        public SnakeBody AddSpeed(SnakeBody snake, int speed)
        {
            snake.Speed += speed;
            return snake;
        }
        public SnakeBody AddLength(SnakeBody snake)
        {
            BodyPart lastPart = snake.BodyParts.Last();
            BodyPart newPart = new BodyPart(lastPart.X, lastPart.Y, snake.BodyColor);
            snake.BodyParts.Add(newPart);
            return snake;
        }
        public SnakeBody ChangeColor(SnakeBody snake, string color)
        {
            snake.BodyColor = color;
            return snake;
        }
    }
}
