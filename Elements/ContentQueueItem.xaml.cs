using System;
using System.Collections.Generic;
using System.IO;
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

namespace Vidiya.Elements
{
    /// <summary>
    /// Interaktionslogik für ContentQueueItem.xaml
    /// </summary>
    public partial class ContentQueueItem : UserControl
    {
        private QueueItem data;

        public ContentQueueItem(QueueItem data)
        {
            this.data = data;
            InitializeComponent();
        }

        private void UriTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            UriTextBlock.Text = System.IO.Path.GetFileName(data.uri.LocalPath);
        }

        private void TypeTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            switch(data.originState.use)
            {
                case ContentUse.ad: TypeTextBlock.Text = "⛟"; break;
                case ContentUse.music: TypeTextBlock.Text = "♬"; break;
            }
        }
    }
}
