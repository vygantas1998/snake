using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects
{
    abstract class Command
    {
        protected Map receiver;

        public Command(Map receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
        public abstract void UnExecute();
    }
}
