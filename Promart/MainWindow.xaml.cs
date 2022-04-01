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

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void TrocarVisibilidadeTelaPreta()
        {
            if (BlackScreen.Visibility == Visibility.Visible)
                BlackScreen.Visibility = Visibility.Hidden;
            else
                BlackScreen.Visibility = Visibility.Visible;
        }

        public MainWindow()
        {
            InitializeComponent();

            //var page = new CadastroAlunoPage();
            //var page = new ControlePresencaAlunoPage();
            //var page = new CadastroOficinaPage();
            var page = new CadastroVoluntarioPage();

            CadastrarAlunoBtn.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Aluno", "Digite o nome do aluno"), CadastrarNovoAluno);
            CadastrarAlunoMenu.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Aluno", "Digite o nome do aluno"), CadastrarNovoAluno);
            CadastrarVoluntarioBtn.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Voluntário", "Digite o nome do voluntário"), CadastrarNovoVoluntario);
            CadastrarVoluntarioMenu.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Voluntário", "Digite o nome do voluntário"), CadastrarNovoVoluntario);
            CadastrarOficinaBtn.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Oficina", "Digite o nome da Oficina"), CadastrarNovaOficina);
            CadastrarOficinaMenu.Click += (object sender, RoutedEventArgs e) => InicializarCadastro(new NovoCadastroWindow("Cadastrar Oficina", "Digite o nome da Oficina"), CadastrarNovaOficina);
            ConsultaAvancadaMenu.Click += (object sender, RoutedEventArgs e) => {
                PesquisaAvancadaPage page = new();
                
                Helper.Controles.AbrirNovaAba(TabConteudo, "Pesquisa Avançada", page);
            };

            //var alunos = SqliteDataAccess.GetAlunos();
            //MessageBox.Show(alunos.Count.ToString());
        }        

        private void CadastrarNovoAluno(string tabHeader)
        {
            Aluno aluno = new();
            aluno.NomeCompleto = tabHeader;
            CadastroAlunoPage page = new(aluno);

            page.AlunoTab = Helper.Controles.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void CadastrarNovoVoluntario(string tabHeader)
        {
            Voluntario voluntario = new();
            voluntario.NomeCompleto = tabHeader;
            CadastroVoluntarioPage page = new(voluntario);

            Helper.Controles.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void CadastrarNovaOficina(string tabHeader)
        {
            Oficina oficina = new();
            oficina.Nome = tabHeader;            
            CadastroOficinaPage page = new(oficina);

            Helper.Controles.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void InicializarCadastro(NovoCadastroWindow dialogWindow, Action<string> cadastrarAction)
        {
            BlackScreen.Visibility = Visibility.Visible;
            bool? result = dialogWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                cadastrarAction(dialogWindow.NomeRecebido);
            }

            BlackScreen.Visibility = Visibility.Hidden;
        }
    }
}
