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

namespace Promart.Controls
{
    /// <summary>
    /// Interaction logic for TabHeaderContentControl.xaml
    /// </summary>
    public partial class TabHeaderContentControl : UserControl
    {
        public string HeaderText
        {
            get { return (string)TitleLabel.Content; }
            set { TitleLabel.Content = value; }
        }

        public TabHeaderContentControl(string titulo, double largura, RoutedEventHandler closeAction)
        {
            InitializeComponent();
            TitleLabel.Content = titulo;

            this.Width = largura;
            CloseButton.Click += closeAction;
        }
    }
}
