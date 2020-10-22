using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
   public class PowerUpFactoryProducer
    {
        public static AbstractPowerUpFactory getFactory(bool buff)
        {
            if (buff)
            {
                return new BuffFactory();
            }
            else
            {
                return new DeBuffFactory();
            }
        }
    }
}
