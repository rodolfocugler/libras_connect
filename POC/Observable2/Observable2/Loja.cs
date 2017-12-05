using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable2
{
    class Loja : Observable.Subject
    {
        public Promocao PromocaoAtual { get; private set; }
        public string Nome { get; }
        public Loja (string nome)
        {
            Nome = nome;
        }
        public void NovaPromocao(Promocao p)
        {
            PromocaoAtual = p;
            Notify();
        }
    }
}
