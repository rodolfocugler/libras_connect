using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable
{
    class SubjectReader : IObserver<Subject>
    {
        private IDisposable unsubscriber;
        private int id = 0;
        public SubjectReader(int id)
        {
            this.id = id;
        }
        public void OnCompleted() //Notificação recebida do observable (subject), indicando sua finalização
        {
            unsubscriber.Dispose();
        }
        public virtual void Subscribe(IObservable<Subject> provider)
        {
            if (provider != null)//Recebe um observable, realiza o cadastro no mesmo, e armazena o objeto de finalização
                unsubscriber = provider.Subscribe(this);
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Um Erro Ocorreu: {0}", error.ToString());
        }

        public void OnNext(Subject value)//Metodo chamado pelo Observable, para notificar este observer de uma atualização
        {
            Console.Write("Subject reader {0}: ", id);
            value.acao();
        }
    }
}
