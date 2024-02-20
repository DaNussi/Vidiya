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
using Vidiya.Content;

namespace Vidiya.Elements.Content
{
    /// <summary>
    /// Interaktionslogik für GenericPlaylistContentSourceDisplay.xaml
    /// </summary>
    public partial class GenericPlaylistContentSourceDisplay : UserControl
    {
        public GenericPlaylistContentSourceDisplay()
        {
            InitializeComponent();
        }

        public void SetContent(ContentSource source)
        {
            ContentView.Children.Clear();
            foreach(var content in source.content)
            {
                ContentView.Children.Add(content.GetUserControl());
            }
        }

        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
