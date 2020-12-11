using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Objects.Iterator
{
    interface IAbstractIterator

    {
        PowerUp First();
        PowerUp Next();
        bool IsDone { get; }
        PowerUp CurrentItem { get; }
    }
    public class PowerUpIterator : IAbstractIterator
    {
        private PowerUpCollection _collection;
        private int _current = 0;
        private int _step = 1;

        public PowerUpIterator(PowerUpCollection collection)
        {
            this._collection = collection;
        }

        public PowerUp First()
        {
            _current = 0;
            return _collection[_current] as PowerUp;
        }

        public PowerUp Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as PowerUp;
            else
                return null;
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public PowerUp CurrentItem
        {
            get { return _collection[_current] as PowerUp; }
        }

        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}
