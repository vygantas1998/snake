using Snake.Objects.Memento;
using System;
using System.Collections.Generic;

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
                                part.X += Speed;
                                break;
                            case Direction.Left:
                                part.X -= Speed;
                                break;
                            case Direction.Up:
                                part.Y -= Speed;
                                break;
                            case Direction.Down:
                                part.Y += Speed;
                                break;
                        }
                        CheckForDeath(part, map);
                    }
                    else
                    {
                        part.X = BodyParts[i - 1].X;// + (Direction == Direction.Right ? -1 * (16 - Speed) : (Direction == Direction.Left ? (16 - Speed) : 0));
                        part.Y = BodyParts[i - 1].Y;// + (Direction == Direction.Up ? (16 - Speed) : (Direction == Direction.Down ? -1 * (16 - Speed) : 0));
                    }
                }
            }
        }
        public void Accept(IPowerUp visitor)
        {
            visitor.Eat(this);
        }
        public void CheckForDeath(BodyPart part, Map map)
        {
            //Get maximum X and Y Pos
            int maxXPos = map.Width;
            int maxYPos = map.Height;
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
        public SnakeMemento SaveMemento()
        {
            return new SnakeMemento(Speed, isDead, BodyParts, Direction, HeadColor, BodyColor);
        }
        public void RestoreMemento(SnakeMemento memento)
        {
            this.Speed = memento.Speed;
            this.isDead = memento.IsDead;
            this.BodyParts = memento.BodyParts;
            this.Direction = memento.Direction;
            this.HeadColor = memento.HeadColor;
            this.BodyColor = memento.BodyColor;
        }
    }
}
