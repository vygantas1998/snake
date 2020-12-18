using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Iterator
{
    interface IAbstractCollection
    {
        PowerUpIterator CreateIterator();
    }

    public class PowerUpCollection : IAbstractCollection
    {
        private ArrayList _items = new ArrayList();

        public PowerUpIterator CreateIterator()
        {
            return new PowerUpIterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public void Add(PowerUp powerUp)
        {
            _items.Add(powerUp);
        }

        public void Remove(PowerUp powerUp)
        {
            _items.Remove(powerUp);
        }


        public PowerUp this[int index]
        {
            get { return _items[index] as PowerUp; }
        }
    }
}
