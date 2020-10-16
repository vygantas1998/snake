using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.Levels
{
     class LevelFactory
    {
        public static Level CreateLevel(string level="")
        {
            switch (level)
            {
                case "Easy":
                    return new Easy();
                case "Medium":
                    return new Medium();
                case "Hard":
                    return new Hard();
                default:
                    return new Easy();                
            }
        }
    }
}
