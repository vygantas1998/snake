using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    //An generic interface for Observers specifying how they should be updated
    public interface IObserver<T>
    {
        void Update(T data);
    }

    public class Observable<T>
    {
        private List<IObserver<T>> observers = new List<IObserver<T>>();
        private T subject;

        public T Subject
        {
            get => subject;
            set
            {
                subject = value;
                Notify();
            }
        }

        public void Register(IObserver<T> observer)
        {
            observers.Add(observer);
        }

        public void Unregister(IObserver<T> observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(subject);
            }
        }
    }
}
