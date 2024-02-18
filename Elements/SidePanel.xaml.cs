using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Vidiya.Managers;

namespace Vidiya.Elements
{
    /// <summary>
    /// Interaktionslogik für SidePanel.xaml
    /// </summary>
    public partial class SidePanel : UserControl
    {
        public SidePanel()
        {
            InitializeComponent();
        }

        private void MenuGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MenuGrid_SetUserControl(VidiyaManager.instance.MenuManager.currentMenu);
            VidiyaManager.instance.MenuManager.MenuChanged += MenuGrid_MenuChanged;
        }

        private void MenuGrid_MenuChanged(object sender, UserControl userControl)
        {
            MenuGrid_SetUserControl(userControl);
        }

        private void MenuGrid_SetUserControl(UserControl userControl)
        {
            MenuGrid.Children.Clear();
            MenuGrid.Children.Add(userControl);
        }

    }
}
