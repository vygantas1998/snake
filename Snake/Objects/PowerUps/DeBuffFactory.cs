using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class DeBuffFactory: AbstractPowerUpFactory
    {
        public override PowerUp getPowerUp(int x, int y, PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.Size:
                    return new SizeDown(x, y);
                case PowerUpType.Speed:
                    return new SpeedDown(x, y);
                default:
                    return null;
            }
        }
    }
}
