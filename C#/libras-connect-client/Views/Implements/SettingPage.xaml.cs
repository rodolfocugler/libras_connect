using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using libras_connect_domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace libras_connect_client.Views.Implements
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page, ISettingPage, IBindPage
    {
        private ISettingService _settingService;

        public SettingPage(ISettingService settingService)
        {
            _settingService = settingService;

            InitializeComponent();
        }

        public void Bind()
        {
            try
            {
                ICollection<Setting> list = _settingService.Get();

                if(list != null && list.Count > 1)
                {
                    tbx_server_ip.Text = list.ElementAt(0).IP;
                    tbx_server_1_port.Text = list.Where(s => s.Camera == CameraEnum.CAMERA_1).SingleOrDefault().Port.ToString();
                    tbx_server_2_port.Text = list.Where(s => s.Camera == CameraEnum.CAMERA_2).SingleOrDefault().Port.ToString();
                }                
            }
            catch
            {

            }
        }

        private void save_socket_setting(object sender, RoutedEventArgs e)
        {
            try
            {
                _settingService.Delete();

                Setting setting = new Setting();
                setting.IP = tbx_server_ip.Text;
                setting.Port = Convert.ToInt32(tbx_server_1_port.Text);
                setting.Camera = CameraEnum.CAMERA_1;

                _settingService.Create(setting);

                setting = new Setting();
                setting.IP = tbx_server_ip.Text;
                setting.Port = Convert.ToInt32(tbx_server_2_port.Text);
                setting.Camera = CameraEnum.CAMERA_2;

                _settingService.Create(setting);

                App.RefreshSockets();
                MessageBox.Show("Salvo com sucesso", "Sucesso", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Erro ao salvar", "Erro", MessageBoxButton.OK);
            }            
        }
    }
}
