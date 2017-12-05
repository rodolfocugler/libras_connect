using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable
{
    class Subject
    {
        int id = new Random().Next();
        public void acao()
        {
            Console.WriteLine("Acao Executada pelo {0}", id);
        }
    }
}
