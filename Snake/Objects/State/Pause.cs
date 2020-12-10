﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.State
{
    public class Pause : GameState
    {
        public override void Handle(Map map)
        {
            map.gameState = new Started();
        }
    }
}

