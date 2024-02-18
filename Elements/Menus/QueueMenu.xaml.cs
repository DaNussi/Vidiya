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
using Vidiya.Managers;

namespace Vidiya.Elements.Menus
{
    /// <summary>
    /// Interaktionslogik für QueueMenu.xaml
    /// </summary>
    public partial class QueueMenu : UserControl
    {
        public QueueMenu()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MenuManager.returnToPrevious();
        }
    }
}
