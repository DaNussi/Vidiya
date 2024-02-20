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
    /// Interaktionslogik für GenericSingleContentSourceDisplay.xaml
    /// </summary>
    public partial class GenericSingleContentSourceDisplay : UserControl
    {

        public GenericSingleContentSourceDisplay()
        {
            InitializeComponent();
        }

        public void SetContent(ContentSource source)
        {
            if (source.content.Count != 1) return;
            ContentElement.SetContent(source.content[0]);
        }
    }
}
