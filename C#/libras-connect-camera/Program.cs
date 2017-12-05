using libras_connect_domain.DTO;
using libras_connect_domain.Enums;
using libras_connect_domain.Services.Implements;
using libras_connect_domain.Services.Implements.Net;
using libras_connect_domain.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Unity;

namespace libras_connect_camera
{
    class Program
    {
        private static IUnityContainer _container;
        private static SocketClient _socketClient;
        private static Subject _subject;

        static void Main(string[] args)
        {
            _container = new UnityContainer();

            Console.WriteLine("Digite: {0}1 - Dados{0}2 - Imagem{0}3 - Teste", Environment.NewLine);

            string parameter = GetParameter(new string[] { "1", "2", "3" });
            RealsenseTypeEnum realsenseType = (RealsenseTypeEnum)Convert.ToInt32(parameter);
            
            _container.RegisterType<IFingerDataService, FingerDataService>();
            _container.RegisterType<IJointDataService, JointDataService>();
            _container.RegisterType<IHandDataService, HandDataService>();
            _container.RegisterType<IDataService, DataService>();

            if (realsenseType == RealsenseTypeEnum.HAND_DATA)
            {
                _container.RegisterType<IRealsenseService, RealsenseDataService>();
            }
            else if (realsenseType == RealsenseTypeEnum.IMAGE_DATA)
            {
                _container.RegisterType<IRealsenseService, RealsenseImageService>();
            }
            else if (realsenseType == RealsenseTypeEnum.TEST)
            {
                _container.RegisterType<IRealsenseService, RealsenseTestService>();
            }

            _container.RegisterType<IRealsenseAlertService, RealsenseAlertService>();

            Console.WriteLine("{0}Digite: {0}1 - localhost{0}2 - digitar o endereço", Environment.NewLine);
            parameter = GetParameter(new string[] { "1", "2" });

            string address = null;

            if (parameter == "1")
            {
                Console.WriteLine("{0}Digite: {0}1 - 16503{0}2 - 16504", Environment.NewLine);
                parameter = GetParameter(new string[] { "1", "2" });

                if (parameter == "1")
                {
                    address = "127.0.0.1:16503";
                }
                else if (parameter == "2")
                {
                    address = "127.0.0.1:16504";
                }
            }
            else if (parameter == "2")
            {
                Console.WriteLine("{0}Digite o endereço do WPF: ", Environment.NewLine);
                address = Console.ReadLine();
            }

            _socketClient = SocketClient.GetInstance(address);

            IRealsenseService realsenseService = _container.Resolve<IRealsenseService>();

            _subject = new Subject();
            _subject.Attach(realsenseService as IObserver);

            Task.Run(() =>
            {
                realsenseService.Start();
            });

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
