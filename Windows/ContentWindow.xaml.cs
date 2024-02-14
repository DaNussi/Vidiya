using YoutubeDLSharp;
using System;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Vidiya.Managers;
using Vidiya.Elements;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

                ContentState state = new ContentState(ContentStatus.Unknown, ContentType.Unknown, ContentUse.music, null, null, AddContentTextBox.Text);
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

        private void AddFolderContentButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select Folder";
            dialog.ShowDialog();

            if (dialog.SelectedPath == null || dialog.SelectedPath == "") return;


            ContentState state = new ContentState(ContentStatus.Unknown, ContentType.Unknown, ContentUse.ad, null, null, "file://"+ dialog.SelectedPath);
            ContentListBoxItem item = new ContentListBoxItem(state);

            item.Width = ContentListBox.ActualWidth - 10;
            ContentManager.add_content(item);
            ContentListBox.ItemsSource = ContentManager.contentCollection;

        }

        ObservableCollection<ContentQueueItem> contentQueueItems = new ObservableCollection<ContentQueueItem>();
        private void ContentQueueListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ContentQueueListBox.ItemsSource = contentQueueItems;
            ContentQueueListBox_Update();

            AutoPlayManager.queueListeners.Add(ContentQueueListBox_Update);
        }

        private void ContentQueueListBox_Update()
        {
            contentQueueItems.Clear();
            foreach (QueueItem uri in AutoPlayManager.queue)
            {
                ContentQueueItem item = new ContentQueueItem(uri);
                item.Width = ContentListBox.ActualWidth - 10;
                contentQueueItems.Add(item);
            }
            SizeQueueTextBlock.Text = "Size: "+contentQueueItems.Count + " | Ads: " + AutoPlayManager.allAds.Count + " | Music: " + AutoPlayManager.allMusic.Count;
        }

        private void ContentQueueListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var item in contentQueueItems)
            {
                item.Width = ContentQueueListBox.ActualWidth - 10;
            }
        }

        private void RescanQueueButton_Click(object sender, RoutedEventArgs e)
        {
            AutoPlayManager.Scan();
        }

        private void ExpandQueueButton_Click(object sender, RoutedEventArgs e)
        {
            AutoPlayManager.ExpandQueue();
        }

        private void AdFrequenzy_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool AdFrequenzy_Initzialiced = false;
        private void AdFrequenzy_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!AdFrequenzy_Initzialiced) return;
            try {
                AutoPlayManager.AdFrequency = int.Parse(AdFrequenzy.Text);
                AutoPlayManager.Scan();
            } catch { }
        }

        private void AdFrequenzy_Loaded(object sender, RoutedEventArgs e)
        {
            AdFrequenzy.Text = AutoPlayManager.AdFrequency.ToString();
            AdFrequenzy_Initzialiced = true;
        }
    }
}
