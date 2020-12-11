using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Interpreter
{
    class GameStartCommand : CommandExpression
    {
        public override string Command() { return "/startgame "; }
        public override void Execute(string data)
        {
            Client.GameStart(data, 1000, 969);
            Client.AddPowerUp();
        }
    }
}
