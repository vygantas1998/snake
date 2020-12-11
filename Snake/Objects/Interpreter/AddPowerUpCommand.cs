using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Interpreter
{
    class AddPowerUpCommand : CommandExpression
    {
        public override string Command() { return "/addpow"; }
        public override void Execute(string data)
        {
            Client.AddPowerUp();
        }
    }
}
