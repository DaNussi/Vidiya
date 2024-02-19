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
using Vidiya.Content.YouTube;

namespace Vidiya.Elements.Content.YouTube
{
    /// <summary>
    /// Interaktionslogik für YouTubeSingleContentDisplay.xaml
    /// </summary>
    public partial class YouTubeSingleContentDisplay : UserControl
    {

        public YouTubeSingleContentDisplay(YouTubeSingleContent content)
        {
            InitializeComponent();

            this.TitleTextBlock.Text = content.title;
            this.DiscriptionTextBlock.Text = content.description;
            this.URITextBlock.Text = content.uri.ToString();
        }
    }
}
