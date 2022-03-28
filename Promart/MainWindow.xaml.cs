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
using Promart.Pages;
using Promart.Windows;

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //NavegadorConteudo.Navigate(new Uri("Pages/CadastrarAlunoPage.xaml", UriKind.Relative) );
            
            //var page = new CadastroAlunoPage();
            //var page = new ControlePresencaAlunoPage();
            //var page = new CadastroOficinaPage();
            var page = new CadastroVoluntarioPage();

            ConteudoFrame.NavigationService.Navigate(page);

            CadastrarAlunoBtn.Click += (object sender, RoutedEventArgs e) => { 
                NovoAlunoWindow novoAlunoWindow = new NovoAlunoWindow();

                PopupBackRectangle.Visibility = Visibility.Visible;
                novoAlunoWindow.ShowDialog();
                PopupBackRectangle.Visibility = Visibility.Hidden;
                CadastrarAlunoBtn_Click(sender, e, string.Empty); 
            };
        }

        private void CadastrarAlunoBtn_Click(object sender, RoutedEventArgs e, string tabHeader)
        {
            TabItem tabItem = new TabItem();
            ScrollViewer scrollViewer = new ScrollViewer();
            Frame frame = new Frame();
            CadastroAlunoPage page = new CadastroAlunoPage();

            frame.Content = page;
            scrollViewer.Content = frame;
            tabItem.Content = scrollViewer;

            TabConteudo.Items.Add(tabItem);
        }
    }
}
