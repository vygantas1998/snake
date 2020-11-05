using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
    public enum PowerUpType
    {
        Size,
        Speed
    }
    public abstract class AbstractPowerUpFactory
    {
        public abstract PowerUp getPowerUp(int x, int y, PowerUpType powerUpType, int random);
    }
}
