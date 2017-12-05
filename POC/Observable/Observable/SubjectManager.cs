using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable
{
    //Subject Manager possui o objeto de interesse, e implementa o IObservable, esta implementaçãpo poderia ser feita
    //No proprio Subjet, e ao invez de retornar o objeto inteiro, retornar apenas a parte de interesse a ser propagada
    class SubjectManager : IObservable<Subject>, IDisposable
    {
        List<IObserver<Subject>> observers;
        private Subject valorAtual;
        public Subject ValorAtual {
           get
            {
                return valorAtual;
            }
            set
            {
                valorAtual = value;
                Notify(value);//Sempre que o objeto de interesse é atualizado, ele notifica todos os observers cadastrados
            }
        }
        public SubjectManager()
        {
            observers = new List<IObserver<Subject>>();
        }
        public IDisposable Subscribe(IObserver<Subject> observer) //Metodo do IObservable, cadastra um observer
        {//e retorna o objeto para sua finalização (IDisposable)
            observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public void Notify(Subject t)
        {
            foreach (IObserver<Subject> o in observers)
            {
                o.OnNext(t);//OnNext notifica os observers
            }
        }

        public void Dispose() //Finaliza o objeto, removendo todos os observers (OnCompleted notifica o encerramento
        {// do subject)
           
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();
            observers.Clear();
        }

        private class Unsubscriber : IDisposable //Finalização do observer
        {
            private List<IObserver<Subject>> _observers;
            private IObserver<Subject> _observer;

            public Unsubscriber(List<IObserver<Subject>> observers, IObserver<Subject> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }

    
}
