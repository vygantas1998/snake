using Snake.Objects.PowerUps;
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
        public override PowerUp generatePowerUp(int mapHeight, int mapWidth)
        {
            Random rnd = new Random();
            AbstractPowerUpFactory powerUpFactory = PowerUpFactoryProducer.getFactory(rnd.Next(100) > 30 ? true : false);
            PowerUp powerUp = powerUpFactory.getPowerUp(rnd.Next(8, mapHeight - 8), rnd.Next(8, mapWidth - 8), rnd.Next(2) == 1 ? PowerUpType.Size : PowerUpType.Speed);
            return powerUp;
        }
    }
}
