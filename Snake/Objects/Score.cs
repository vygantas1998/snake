using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects
{
    public class Score
    {
        public int Points { get; set; }

        public Score() { }

        public Score(int id, int points)
        {
            Points = points;
        }
    }
}
