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

namespace Vidiya.Elements.Generics
{
    /// <summary>
    /// Interaktionslogik für VidiyaRect.xaml
    /// </summary>
    public partial class VidiyaRect : UserControl
    {
        public VidiyaRect()
        {
            InitializeComponent();
        }


        private void MainGrid_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            FullGrid.Visibility = System.Windows.Visibility.Hidden;
            XGrid.Visibility = System.Windows.Visibility.Hidden;
            YGrid.Visibility = System.Windows.Visibility.Hidden;
            MiniGrid.Visibility = System.Windows.Visibility.Hidden;

            if (MainGrid.ActualWidth <= 48.6 && MainGrid.ActualHeight <= 48.6)
            {
                MiniGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MainGrid.ActualHeight <= 48.6)
            {
                XGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else if (MainGrid.ActualWidth <= 48.6)
            {
                YGrid.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                FullGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
