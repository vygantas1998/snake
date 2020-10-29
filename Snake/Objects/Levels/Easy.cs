using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects.Levels
{
    public class Easy : Level
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
        public override PowerUp generatePowerUp(int mapHeight, int mapWidth)
        {
            Random rnd = new Random();
            return new SizeUp(rnd.Next(8, mapHeight - 8), rnd.Next(8, mapWidth - 8));
        }
    }
}
