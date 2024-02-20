using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class GenericPlaylistDisplay : UserControl
    {
        private ContentSource? source;

        public GenericPlaylistDisplay()
        {
            InitializeComponent();
        }

        public void SetContent(ContentSource source)
        {
            this.source = source;
            Source_Update();
            StateIcon.Kind = source.stateIcon;
            StateIcon.ToolTip = source.stateMessage;

            source.StateChanged += SourceStateChanged;
            source.content.CollectionChanged += Source_ContentChanged;
        }

        private void Source_ContentChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Source_Update();
        }

        private void Source_Update()
        {
            if (source == null) return;

            ContentView.Children.Clear();
            foreach (var content in source.content)
            {
                ContentView.Children.Add(content.GetUserControl());
            }
        }

        private void SourceStateChanged(object? sender, ContentSource source)
        {
            StateIcon.Kind = source.stateIcon;
            StateIcon.ToolTip = source.stateMessage;
        }
        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
