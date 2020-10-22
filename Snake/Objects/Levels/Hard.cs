using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.Levels
{
    public class Hard: Level
    {
        private readonly string _levelType;

        public Hard()
        {
            _levelType = "Hard";
        }
        public override string levelType
        {
            get { return _levelType; }
        }
    }
}
