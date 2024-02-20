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
    /// Interaktionslogik für GenericPlaylistContentDisplay.xaml
    /// </summary>
    public partial class GenericContentDisplay : UserControl
    {
        public GenericContentDisplay()
        {
            InitializeComponent();
        }

        public void SetContent(ContentResource content)
        {
            TitleText.Text = content.title;
            DescriptionText.Text = content.description;
        }
    }
}
