using System;
using System.Windows;
using System.Windows.Controls;
using Vidiya.Managers;
using Vidiya.Windows;

namespace Vidiya.Elements
{
    /// <summary>
    /// Interaktionslogik für SecondaryMediaControl.xaml
    /// </summary>
    public partial class SecondaryMediaControl : UserControl
    {
        LogManager logger = VidiyaManager.instance.LogManager;

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

            VidiyaManager.instance.MediaManager.Volume(0);
            logger.log(LogType.Info, "Muted");
        }

        public void UnMute()
        {
            if (!IsMuted) return;
            IsMuted = false;

            if(VolumeIcon.Kind == Material.Icons.MaterialIconKind.VolumeMute) SetVolume(VolumeSlider.Value);
            logger.log(LogType.Info, "Unmuted");
        }

        public void SetVolume (double volume)
        {

            if(volume == 0)
            {
                Mute();
                return;
            } 

            UnMute();
            if (volume > 0.5)
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeHigh;
            else
                VolumeIcon.Kind = Material.Icons.MaterialIconKind.VolumeMedium;

            VidiyaManager.instance.MediaManager.Volume(volume);
        }

        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            MediaWindow window = new MediaWindow();
            window.Show();
        }

        private void QrCodeButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
