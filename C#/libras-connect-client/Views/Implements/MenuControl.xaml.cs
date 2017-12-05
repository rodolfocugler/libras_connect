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
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            App.SetContent<ISettingPage>();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            App.SetContent<IHomePage>();
        }
    }
}
