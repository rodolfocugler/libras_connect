using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable2.Observable
{
    class Subject
    {
        List<IObserver> observers;
        public Subject()
        {
            observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }
        public void Dettach(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (IObserver o in observers)
            {
                o.Notify(this);
            }
        }
    }
}
