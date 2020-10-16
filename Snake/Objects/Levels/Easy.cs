using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects.Levels
{
    class Easy : Level
    {
        private readonly string _levelType;
        public Easy()
        {
            _levelType = "Easy";
        }
        public override string levelType
        {
            get { return _levelType; }
        }
    }
}
