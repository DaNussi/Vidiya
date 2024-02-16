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
            if(IsMuted)
                UnMute();
            else 
                Mute();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetVolume(e.NewValue);
        }

        public void Mute ()
        {
            if (IsMuted) return;
            IsMuted = true;
            VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeMute;
        }

        public void UnMute()
        {
            if (!IsMuted) return;
            IsMuted = false;

            if(VolumeIcon.Kind == Material.Icons.MaterialIconKind.VolumeMute) SetVolume(VolumeSlider.Value);
        }

        public void SetVolume (double value)
        {

            if(value == 0)
            {
                Mute();
                return;
            } 

            UnMute();
            if (value > 0.5)
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeHigh;
            else
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeMedium;
        }
    }
}
