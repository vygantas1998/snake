using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.Levels
{
    public class Medium: Level
    {
        private readonly string _levelType;

        public Medium()
        {
            _levelType = "Medium";
        }
        public override string levelType
        {
            get { return _levelType; }
        }
    }
}
