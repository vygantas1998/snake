using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Interpreter
{
    abstract class CommandExpression
    {
        public ClientSocket Client;
        public void Interpret(string message, ClientSocket client)
        {
            Client = client;
            if (message.StartsWith(Command()) && Client != null)
            {
                Execute(message.Substring(Command().Length));
            }
        }

        public abstract string Command();
        public abstract void Execute(string data);
    }
}
