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
using Promart.Models;
using Promart.Data;
using Promart.Codes;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastroVoluntarioPage.xaml
    /// </summary>
    public partial class CadastroVoluntarioPage : Page
    {
        bool dadosAlterados = false;
        public Voluntario Voluntario { get; private set; }
        public MainWindow? MainWindow { get; set; }

        public CadastroVoluntarioPage() : this(new Voluntario())
        {            
        }

        public CadastroVoluntarioPage(Voluntario voluntario)
        {
            InitializeComponent();

            Voluntario = voluntario;
            MainWindow = Window.GetWindow(this) as MainWindow;
            
            NascimentoData.SelectedDateChanged += NascimentoData_SelectedDateChanged;

            //Eventos para confirmar alterações de dados ao sair da tela
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            RGText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            CPFText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            ProfissaoText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            Telefone1Text.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            Telefone2Text.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;            
            EmailText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;            
            RuaText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            BairroText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NumeroText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            ComplementoText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            CidadeText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;            
            ObservacoesText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => dadosAlterados = true;            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NomeText.Text = Voluntario.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            Helper.Controles.PopularOficinasList(OficinasList, (object o, RoutedEventArgs e) => dadosAlterados = true);
        }

        private void NascimentoData_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {

            if (NascimentoData.SelectedDate.HasValue)
            {
                DateTime nascimento = NascimentoData.SelectedDate.Value;
                IdadeLabel.Content = string.Concat(Helper.Util.ObterIdade(nascimento), " anos");
                IdadeLabel.Visibility = Visibility.Visible;
            }
            else
            {
                IdadeLabel.Visibility = Visibility.Hidden;
            }
        }

        public void ConfirmarPagina()
        {
            Voluntario.NomeCompleto = NomeText.Text.Trim();
            Voluntario.DataNascimento = NascimentoData.SelectedDate;
            Voluntario.Profissao = ProfissaoText.Text;
            Voluntario.RG = RGText.Text;
            Voluntario.CPF = CPFText.Text;
            Voluntario.Contato1 = Telefone1Text.Text;
            Voluntario.Contato2 = Telefone2Text.Text;
            Voluntario.Email = EmailText.Text;
            Voluntario.EnderecoRua = RuaText.Text;
            Voluntario.EnderecoBairro = BairroText.Text;
            Voluntario.EnderecoNumero = NumeroText.Text;
            Voluntario.EnderecoComplemento = ComplementoText.Text;
            Voluntario.EnderecoCidade = CidadeText.Text;
            Voluntario.EnderecoEstado = "Bahia";
            Voluntario.EnderecoCEP = 45570000;
            Voluntario.Observacoes = ObservacoesText.Text;

            if (Voluntario.Id == 0)
            {
                SqlAccess.Inserir(Voluntario);
                InserirVoluntarioOficina();
            }
            else
            {
                SqlAccess.Atualizar(Voluntario);
                InserirVoluntarioOficina(true);
            }

            ConfirmarButton.IsEnabled = false;
        }

        private void InserirVoluntarioOficina(bool atualizar = false)
        {
            if (atualizar)
            {
                SqlAccess.TVoluntarioOficinas.Deletar(Voluntario);
            }

            foreach (var checkBox in OficinasList.ItemsSource)
            {
                CheckBox? c = checkBox as CheckBox;

                if (c != null
                    && c.IsChecked != null
                    && c.IsChecked.Value)
                {
                    Oficina? oficina = c.Content as Oficina;

                    if (oficina != null)
                    {
                        VoluntarioOficina alunoOficina = new VoluntarioOficina();
                        alunoOficina.IdVoluntario = Voluntario.Id;
                        alunoOficina.IdOficina = oficina.Id;

                        SqlAccess.Inserir(alunoOficina);
                    }
                }
            }
        }

    }
}
