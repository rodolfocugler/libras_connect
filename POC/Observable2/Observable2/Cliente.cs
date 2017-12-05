using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable2
{
    class Cliente : Observable.IObserver
    {
        public string nome;
        public Cliente(string nome)
        {
            this.nome = nome;
        }
        public override void Notify(object o)
        {
            if(typeof(Loja).Equals(o.GetType()))
            {
                Loja l = (Loja)o;
                Console.WriteLine("Cliente {0}: Loja {1} esta com a seguinte promocao: {2}", nome, l.Nome, l.PromocaoAtual.descricao);
            }
        }
    }
}
