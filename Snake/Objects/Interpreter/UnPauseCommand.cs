﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Interpreter
{
    class UnPauseCommand : CommandExpression
    {
        public override string Command() { return "/u"; }
        public override void Execute(string data)
        {
            Client.PauseGame();
        }
    }
}
