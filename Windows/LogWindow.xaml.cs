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
using System.Windows.Shapes;
using Vidiya.Managers;

namespace Vidiya.Windows
{
    /// <summary>
    /// Interaktionslogik für LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void LogTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            LogTextBlock.Text = string.Empty;
            LogListener logListener = new LogListener(Log_Recived, VidiyaManager.instance.LogManager);
        }

        private void Log_Recived(LogLine logLine, List<LogLine> logs)
        {
            LogTextBlock.Text += logLine.getLine() + "\n";
            LogScrollViewer.ScrollToBottom();
        }
    }
}
