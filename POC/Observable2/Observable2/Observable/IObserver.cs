using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable2.Observable
{
    abstract class IObserver
    {
        public abstract void Notify(Object o);
    }
}
