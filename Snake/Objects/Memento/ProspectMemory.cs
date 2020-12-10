using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Memento
{
    public class ProspectMemory
    {
        private SnakeMemento _memento;

        public SnakeMemento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}
