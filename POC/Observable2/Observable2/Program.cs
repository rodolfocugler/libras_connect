using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable2
{
    class Program
    {
        static void Main(string[] args)
        {
            Loja l1 = new Loja("Americanas");
            Loja l2 = new Loja("Extra");

            Cliente c1 = new Cliente("c1");
            Cliente c2 = new Cliente("c2");
            Cliente c3 = new Cliente("c3");
            Cliente c4 = new Cliente("c4");
            Cliente c5 = new Cliente("c5");

            l1.Attach(c1);
            l1.Attach(c2);
            l1.Attach(c3);
            l1.Attach(c4);

            l2.Attach(c4);
            l2.Attach(c5);

            l1.NovaPromocao(new Promocao("Celular 15% desconto"));
            l2.NovaPromocao(new Promocao("Frete Gratis Toda Loja"));

            Console.ReadKey();

        }
    }
}
