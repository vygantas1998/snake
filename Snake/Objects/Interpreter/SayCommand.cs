using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Interpreter
{
    class SayCommand : CommandExpression
    {
        public override string Command() { return "/say "; }
        public override void Execute(string data)
        {
            string[] dat = data.Split(' ');
            Client.Say(dat[1], dat[0]);
        }
    }
}
