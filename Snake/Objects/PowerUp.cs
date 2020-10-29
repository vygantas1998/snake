using Newtonsoft.Json.Linq;
using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Objects
{
    public abstract class PowerUp
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Points { get; set; }
        public Brush Color { get; set; }
        public PowerUp() { }
        public abstract void Eat(SnakeBody snake);
        public JObject toJSON()
        {
            JObject powerUpObj = new JObject();
            JObject addPowerUp = new JObject();
            addPowerUp["x"] = X;
            addPowerUp["y"] = Y;
            addPowerUp["isBuff"] = (this is SpeedUp || this is SizeUp) ? true : false;
            addPowerUp["powerUpType"] = this is Snake.Objects.PowerUps.Size ? 0 : 1;
            powerUpObj["powerUp"] = addPowerUp;
            return powerUpObj;
        }
    }
}
