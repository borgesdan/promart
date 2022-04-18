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
using Promart.Models;
using Promart.Codes;
using Promart.Data;
using System.ComponentModel;

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }        

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            var page = new CadastroVoluntarioPage();

            FecharProgramaMenu.Click += (sender, e) => this.Close();

            CadastrarAlunoButton.Click += (object sender, RoutedEventArgs e) => CadastrarAluno();
            CadastrarAlunoMenu.Click += (object sender, RoutedEventArgs e) => CadastrarAluno();
            CadastrarVoluntarioBtn.Click += (object sender, RoutedEventArgs e) => CadastrarVoluntario();
            CadastrarVoluntarioMenu.Click += (object sender, RoutedEventArgs e) => CadastrarVoluntario();            
            ConsultaAvancadaMenu.Click += (object sender, RoutedEventArgs e) => {
                PesquisaAvancadaPage page = new();
                
                Helper.Controles.AbrirNovaAba(TabConteudo, "Pesquisa Avançada", page);
            };
            AbrirTabelaDadosButton.Click += (object sender, RoutedEventArgs e) =>
            {
                TabelaDadosPage page = new();
                Helper.Controles.AbrirNovaAba(TabConteudo, "Relatórios", page);
            };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //var result = MessageBox.Show("O programa será encerrado e toda alteração não salva será perdida. Deseja fechar o programa?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning) ;

            //if (result == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //}

            base.OnClosing(e);
        }

        private void CadastrarAluno()
        {
            CadastroAlunoPage page = new CadastroAlunoPage(new Aluno());
            page.Tab = Helper.Controles.AbrirNovaAba(TabConteudo, "Novo Aluno", page);
        }

        private void CadastrarVoluntario()
        {
            CadastroVoluntarioPage page = new CadastroVoluntarioPage(new Voluntario());
            page.Tab = Helper.Controles.AbrirNovaAba(TabConteudo, "Novo Voluntário", page);
        }

        public void AbrirNovaAba(string nome, Page page)
        {
            Helper.Controles.AbrirNovaAba(TabConteudo, nome, page);
        }
    }
}
