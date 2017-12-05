using libras_connect_domain.DTO;
using libras_connect_domain.Repository.Implements.Mongo;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Services.Implements;
using libras_connect_domain.Services.Interfaces;
using System;
using Unity;
using Unity.Injection;

namespace libras_connect_network_test
{
    class Program
    {
        private static IUnityContainer _container;
        private static Subject _subject;

        static void Main(string[] args)
        {
            _container = new UnityContainer();

            Console.WriteLine("{0}Digite para a conexão com o MongoDB: {0}1 - localhost:27017/libras_connect{0}2 - endereço", Environment.NewLine);
            string parameter = GetParameter(new string[] { "1", "2" });

            MongoConnection mc = null;

            if (parameter == "1")
            {
                mc = new MongoConnection("localhost", 27017, "libras_connect");
            }
            else if (parameter == "2")
            {
                Console.WriteLine("{0}Digite para a conexão com o MongoDB (10.0.0.1:2017/nome_do_banco):", Environment.NewLine);
                mc = new MongoConnection(Console.ReadLine());
            }

            _container.RegisterType<ISignalRepository, SignalRepository>(new InjectionConstructor(mc));
            _container.RegisterType<IWordDNNRepository, WordDNNRepository>(new InjectionConstructor(mc));
            _container.RegisterType<ICntkService, CntkService>();
            _container.RegisterType<IVoiceService, VoiceService>();
            _container.RegisterType<ITest, Test>();

            _subject = new Subject();

            ICntkService cntkService = _container.Resolve<ICntkService>();
            _container.RegisterInstance(cntkService);
            _subject.Attach(cntkService as CntkService);

            ITest test = _container.Resolve<ITest>();

            Console.WriteLine("{0}Digite: {0}1 - BuildFileTrain{0}2 - Compute", Environment.NewLine);
            parameter = GetParameter(new string[] { "1", "2" });

            if (parameter == "1")
            {
                test.BuildFileTrain();
            }
            else if (parameter == "2")
            {
                test.Compute(0, 0.7);
                test.Compute(0.7, 1);
            }

            Console.ReadKey();

            Application_Exit();
        }

        /// <summary>
        /// Read Console and check if return is valid
        /// </summary>
        /// <param name="values">valid returns</param>
        /// <returns>parameter</returns>
        private static string GetParameter(string[] values)
        {
            string parameter = null;
            bool isOk = false;

            do
            {
                parameter = Console.ReadLine();
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] == parameter)
                    {
                        isOk = true;
                    }
                }
            } while (!isOk);

            return parameter;
        }

        /// <summary>
        /// When Application is closing
        /// </summary>
        private static void Application_Exit()
        {
            _subject.Notify();
        }
    }
}
