using libras_connect_client.Services.Implements;
using libras_connect_client.Services.Interfaces;
using libras_connect_client.Views;
using libras_connect_client.Views.Implements;
using libras_connect_domain.DTO;
using libras_connect_domain.Models;
using libras_connect_domain.Repository.Implements.Mongo;
using libras_connect_domain.Repository.Implements.SQLite;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Services.Implements;
using libras_connect_domain.Services.Implements.Net;
using libras_connect_domain.Services.Interfaces;
using libras_connect_infrastructure.Config;
using System.Collections.Generic;
using System.Windows;
using Unity;
using Unity.Injection;

namespace libras_connect_client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer _container;
        private static Subject _subject;
        private static ICollection<SocketServer> _socketServers;
        private static MainWindow _mainWindow;

        /// <summary>
        /// When Application is opening
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">StartupEventArgs</param>
        protected void Application_Startup(object sender, StartupEventArgs e)
        {
            _container = new UnityContainer();

            #region SERVICES
            _container.RegisterType<IControlService, ControlService>();
            _container.RegisterType<IBitmapService, BitmapService>();
            _container.RegisterType<ISettingService, SettingService>();
            _container.RegisterType<ICntkService, CntkService>();
            _container.RegisterType<IVoiceService, VoiceService>();
            #endregion

            #region REPOSITORY
            MongoConnection mc = new MongoConnection(ConfigValues.Mongo_IP, 27017, ConfigValues.Mongo_Database);

            _container.RegisterType<ISignalRepository, SignalRepository>(new InjectionConstructor(mc));
            _container.RegisterType<IWordDNNRepository, WordDNNRepository>(new InjectionConstructor(mc));
            _container.RegisterType<ISettingRepository, SettingRepository>();
            #endregion

            #region PAGE
            _container.RegisterType<IHomePage, HomePage>();
            _container.RegisterType<ISettingPage, SettingPage>();
            #endregion

            #region SUBJECT
            _subject = new Subject();

            ICntkService cntkService = _container.Resolve<ICntkService>();
            _container.RegisterInstance(cntkService);
            _subject.Attach(cntkService as CntkService);
            #endregion

            #region SOCKET
            IControlService controlService = _container.Resolve<IControlService>();
            _container.RegisterInstance(controlService);
            _subject.Attach(controlService as IObserver);
            RefreshSockets();
            #endregion

            #region MAIN_WINDOW
            _mainWindow = new MainWindow();
            SetContent<IHomePage>();
            _mainWindow.Show();
            #endregion
        }

        #region SOCKET
        public static void RefreshSockets()
        {
            if (_socketServers != null)
            {
                foreach (SocketServer socketServer in _socketServers)
                {
                    _subject.Dettach(socketServer);
                }
            }

            _socketServers = new List<SocketServer>();
            ISettingService settingService = _container.Resolve<ISettingService>();
            ISocketCallback callback = _container.Resolve<IControlService>() as ISocketCallback;

            foreach (Setting setting in settingService.Get())
            {
                SocketServer socketServer = new SocketServer(callback, setting.IP, setting.Port, setting.Camera);
                _socketServers.Add(socketServer);
                _subject.Attach(socketServer);
            }
        }
        #endregion

        /// <summary>
        /// Set Main Window Content
        /// </summary>
        public static void SetContent<T>()
        {
            T t = _container.Resolve<T>();
            _mainWindow.Content = t;

            if (t is IBindPage)
            {
                (t as IBindPage).Bind();
            }
        }

        /// <summary>
        /// When Application is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Exit(object sender, ExitEventArgs e)
        {
            _subject.Notify();
        }
    }
}
