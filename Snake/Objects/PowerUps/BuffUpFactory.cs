using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public class BuffFactory : AbstractPowerUpFactory
    {
        public override PowerUp getPowerUp(int x, int y, PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.Size:
                    return new SizeUp(x, y);
                case PowerUpType.Speed:
                    return new SpeedDown(x, y);
                default:
                    return null;
            }
        }
    }
}
