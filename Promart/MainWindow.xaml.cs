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
        }

        private void CadastrarNovoAluno(string tabHeader)
        {
            Aluno aluno = new();
            aluno.NomeCompleto = tabHeader;
            CadastroAlunoPage page = new(aluno);

            Helper.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void CadastrarNovoVoluntario(string tabHeader)
        {
            Voluntario voluntario = new();
            voluntario.NomeCompleto = tabHeader;
            CadastroVoluntarioPage page = new(voluntario);

            Helper.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void CadastrarNovaOficina(string tabHeader)
        {
            Oficina oficina = new();
            oficina.Nome = tabHeader;            
            CadastroOficinaPage page = new(oficina);

            Helper.AbrirNovaAba(TabConteudo, tabHeader, page);
        }

        private void InicializarCadastro(NovoCadastroWindow dialogWindow, Action<string> cadastrarAction)
        {
            PopupBackRectangle.Visibility = Visibility.Visible;
            bool? result = dialogWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                cadastrarAction(dialogWindow.NomeRecebido);
            }

            PopupBackRectangle.Visibility = Visibility.Hidden;
        }
    }
}
