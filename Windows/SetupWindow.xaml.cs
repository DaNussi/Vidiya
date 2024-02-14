using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vidiya.Managers;
using YoutubeDLSharp;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        public SetupWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log(LogMessageType.Info, "Starting software...");

            DataManager.Init(log);
            ContentManager.Init(log);
            AutoPlayManager.Init(log);

            MainWindow mainWindow = new MainWindow();

            log(LogMessageType.Info, "Finished setup");
            log(LogMessageType.Info, "Opening main window");

            this.Hide();
            mainWindow.Show();
        }

        public void log(LogMessageType type,String text)
        {
            logTextBlock.Text += type + " | " + text + "\n";
        }
    }

    public enum LogMessageType
    {
        Info,
        Warning,
        Error,
    }
}
