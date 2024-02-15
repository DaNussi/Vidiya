using System.Windows;
using System.Windows.Controls;

namespace Vidiya.Elements
{
    /// <summary>
    /// Interaktionslogik für SecondaryMediaControl.xaml
    /// </summary>
    public partial class SecondaryMediaControl : UserControl
    {
        public SecondaryMediaControl()
        {
            InitializeComponent();
        }

        bool IsMuted = false;
        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            IsMuted = !IsMuted;
            if(IsMuted)
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeMute;
            else 
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeHigh;
        }
    }
}
