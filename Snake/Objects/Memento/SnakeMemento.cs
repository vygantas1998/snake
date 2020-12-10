using System.Collections.Generic;

namespace Snake.Objects.Memento
{
    public class SnakeMemento
    {
        private int _speed { get; set; }
        private bool _isDead { get; set; }
        private List<BodyPart> _bodyParts { get; set; }
        private Direction _direction { get; set; }
        private string _headColor { get; set; }
        private string _bodyColor { get; set; }

        public SnakeMemento(int speed, bool isDead, List<BodyPart> bodyParts, Direction direction, string headColor, string bodyColor)
        {
            this._speed = speed;
            this._isDead = isDead;
            this._direction = direction;
            this._headColor = headColor;
            this._bodyColor = bodyColor;

            _bodyParts = new List<BodyPart>();
            foreach(BodyPart b in bodyParts)
            {
                _bodyParts.Add(new BodyPart(b.X, b.Y, b.Color));
            }
        }

        public int Speed
        {
            get { return _speed; }
        }

        public bool IsDead
        {
            get { return _isDead; }
        }

        public List<BodyPart> BodyParts
        {
            get { return _bodyParts; }
        }

        public Direction Direction
        {
            get { return _direction; }
        }
        public string HeadColor
        {
            get { return _headColor; }
        }
        public string BodyColor
        {
            get { return _bodyColor; }
        }
    }
}
