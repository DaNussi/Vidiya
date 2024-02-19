using System.Windows.Controls;

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

            if (MainGrid.ActualWidth > 48.6 && MainGrid.ActualHeight > 48.6)
            {
                FullGrid.Visibility = System.Windows.Visibility.Visible;
            } else if(MainGrid.ActualWidth > 48.6)
            {
                XGrid.Visibility = System.Windows.Visibility.Visible;
            } else if(MainGrid.ActualHeight > 48.6)
            {
                YGrid.Visibility = System.Windows.Visibility.Visible;
            } else
            {
                MiniGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
