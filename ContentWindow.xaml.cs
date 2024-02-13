using YoutubeDLSharp;
using System;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace Vidiya
{
    /// <summary>
    /// Interaktionslogik für ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {


        public ContentWindow()
        {
            InitializeComponent();
            ContentListBox.ItemsSource = ContentManager.contentCollection;
        }

        private void AddContentTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter && AddContentTextBox.Text != string.Empty)
            {

                ContentState state = new ContentState(ContentStatus.Unknown, ContentType.Unknown, null, null, AddContentTextBox.Text);
                ContentListBoxItem item = new ContentListBoxItem(state);

                item.Width = ContentListBox.ActualWidth-10;
                ContentManager.add_content(item);
                ContentListBox.ItemsSource = ContentManager.contentCollection;

                AddContentTextBox.Text = string.Empty;
            }

        }

        private void ContentListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach(var item in ContentManager.contentCollection)
            {
                item.Width = ContentListBox.ActualWidth-10;
            }
        }
    }

    public class ContentCollection : ObservableCollection<ContentListBoxItem>
    {

    }
}
