using System.Collections.Generic;

namespace libras_connect_domain.DTO
{
    public class Subject
    {
        private readonly List<IObserver> _observers;

        public Subject()
        {
            _observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }                
        }

        public void Dettach(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                observer.Notify();
                _observers.Remove(observer);
            }                
        }

        public void Notify()
        {
            foreach (IObserver o in _observers)
            {
                o.Notify();
            }
        }
    }
}
