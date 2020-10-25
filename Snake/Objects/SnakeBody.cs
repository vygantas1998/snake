using Newtonsoft.Json.Linq;
using ServerApp.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

namespace Snake.Objects
{
    public class SnakeBody
    {
        public int Speed { get; set; }
        public bool isDead { get; set; }
        public List<BodyPart> BodyParts { get; set; }
        public Direction Direction { get; set; }
        public string HeadColor { get; set; }
        public string BodyColor { get; set; }

        public SnakeBody() { }

        public SnakeBody(int x, int y, int speed, Direction direction, string headColor, string bodyColor)
        {
            Speed = speed;
            List<BodyPart> bodyParts = new List<BodyPart>();
            bodyParts.Add(new BodyPart(x, y, headColor));
            BodyParts = bodyParts;
            Direction = direction;
            isDead = false;
            HeadColor = headColor;
            BodyColor = bodyColor;
        }
        public void MoveSnake(Map map)
        {
            if (!isDead)
            {
                for (int i = BodyParts.Count - 1; i >= 0; i--)
                {
                    BodyPart part = BodyParts[i];
                    //Move head
                    if (i == 0)
                    {
                        switch (Direction)
                        {
                            case Direction.Right:
                                part.X++;
                                break;
                            case Direction.Left:
                                part.X--;
                                break;
                            case Direction.Up:
                                part.Y--;
                                break;
                            case Direction.Down:
                                part.Y++;
                                break;
                        }
                        CheckForDeath(part, map);
                    }
                    else
                    {
                        //Move body
                        part
                            .X = BodyParts[i - 1].X;
                        part.Y = BodyParts[i - 1].Y;
                    }
                }
            }
        }
        public void CheckForDeath(BodyPart part, Map map)
        {
            //Get maximum X and Y Pos
            int maxXPos = map.Width / 16;
            int maxYPos = map.Height / 16;
            //Detect collission with game borders.
            if (part.X < 0 || part.Y < 0
                || part.X >= maxXPos || part.Y >= maxYPos)
            {
                isDead = true;
            }

            //Detect collission with body
            for (int j = 1; j < BodyParts.Count; j++)
            {
                BodyPart otherPart = BodyParts[j];
                if (part.X == otherPart.X &&
                   part.Y == otherPart.Y)
                {
                    isDead = true;
                }
            }
        }
    }
}
