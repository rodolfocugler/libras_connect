using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable
{
    class Program
    {
        static void Main(string[] args)
        {
            SubjectManager observable = new SubjectManager(); //Contem o objeto a ser monitorado
            SubjectReader reader = new SubjectReader(1); //Contem o objeto que vai monitorar o objeto de interesse

            reader.Subscribe(observable); //Implementação do Subscribe por fora, não da interface
            //Utiliza internamente o subscribe da interface, mas já deixa pronto o dispose internamente, para não ter
            //Necessidade de guardar o objeto IDisposable para finalizar o objeto

            IObserver<Subject> observer = new SubjectReader(2);
            IDisposable disposable = observable.Subscribe(observer); //Utilização do subscribe da interface, ele
            //Inscreve um observer no observable, e retorna um IDisposable para finalização correta do objeto

            observable.ValorAtual = new Subject(); //Das duas formas, ao atualizar o valor do observable, este valor é
            //Propagado para todos os observers cadastrados (Subject é o objeto de interesse)

            Console.ReadKey();

        }
    }
}
