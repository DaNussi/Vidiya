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
using Vidiya.Elements.Menus;
using Vidiya.Managers;

namespace Vidiya.Elements.Menus
{
    /// <summary>
    /// Interaktionslogik für MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MenuManager.changeTo(new QueueMenu());
        }

        private void ContentButton_Click(object sender, RoutedEventArgs e)
        {
            VidiyaManager.instance.MenuManager.changeTo(new ContentMenu());
        }

        private void AutoplayButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
