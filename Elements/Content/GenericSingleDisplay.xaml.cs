using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Vidiya.Managers;

namespace Vidiya.Elements.Content
{
    /// <summary>
    /// Interaktionslogik für GenericSingleContentSourceDisplay.xaml
    /// </summary>
    public partial class GenericSingleDisplay : UserControl
    {
        public LogManager logger = VidiyaManager.instance.LogManager;

        public GenericSingleDisplay()
        {
            InitializeComponent();
        }

        public void SetContent(ContentSource source)
        {
            if (source.content.Count != 1) return;
            ContentElement.SetContent(source.content[0]);
            StateIcon.Kind = source.stateIcon;
            StateIcon.ToolTip = source.stateMessage;
            source.StateChanged += SourceStateChanged;
        }

        private void SourceStateChanged(object? sender, ContentSource source)
        {
            StateIcon.Kind = source.stateIcon;
            StateIcon.ToolTip = source.stateMessage;
        }
    }
}
