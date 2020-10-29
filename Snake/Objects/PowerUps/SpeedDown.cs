﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Objects.PowerUps
{
   public class SpeedDown: Speed
    {
        public SpeedDown(int x, int y)
        {
            X = x;
            Y = y;
            Points = 100;
        }
        public override void Eat(SnakeBody snake)
        {
            snake.Speed -= 8;
        }
    }
}
