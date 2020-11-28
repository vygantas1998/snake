using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.State
{
    public abstract class GameState
    {
        public abstract void Handle(Map map);
    }
}
