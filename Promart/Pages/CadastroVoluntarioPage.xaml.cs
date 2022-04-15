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
        public TabItem? Tab { get; set; }

        public CadastroVoluntarioPage() : this(new Voluntario())
        {
        }

        public CadastroVoluntarioPage(Voluntario voluntario)
        {
            InitializeComponent();
            Voluntario = voluntario;
            SexoCombo.ItemsSource = ComboBoxTipos.TipoSexoNumerado;

            NascimentoData.SelectedDateChanged += (s, e) => ExibirIdade();
            CancelarButton.Click += CancelarButton_Click;
            ConfirmarButton.Click += async (s, e) => await ConfirmarPagina();            

            //Eventos para confirmar alterações de dados ao sair da tela
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            RGText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            CPFText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            ProfissaoText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            EscolaridadeText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            Telefone1Text.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            Telefone2Text.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            EmailText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            RuaText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            BairroText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NumeroText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            ComplementoText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            CidadeText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            ObservacoesText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            
        }        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NomeText.Text = Voluntario.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            await Helper.Controles.PopularOficinasListComCheckBoxAsync(OficinasList, (object o, RoutedEventArgs a) => DefinirAlteracaoDados());
        }

        private void DefinirAlteracaoDados()
        {
            dadosAlterados = true;
            ConfirmarButton.IsEnabled = true;
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            if (dadosAlterados)
            {
                if (!Helper.Controles.DadosAlteradosAviso())
                    return;
            }

            Helper.Controles.RemoverAba(Tab);
        }

        private async Task ConfirmarPagina()
        {
            Voluntario.NomeCompleto = NomeText.Text.Trim();

            if (string.IsNullOrWhiteSpace(Voluntario.NomeCompleto))
            {
                MessageBox.Show("Digite o nome o voluntário antes de confirmar os dados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Voluntario.DataNascimento = NascimentoData.SelectedDate;
            Voluntario.Sexo = SexoCombo.SelectedIndex;
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
            Voluntario.EnderecoCEP = CEPText.Text;
            Voluntario.Observacoes = ObservacoesText.Text;
            Voluntario.DataCadastro = DateTime.Now;
            Voluntario.Escolaridade = EscolaridadeText.Text;

            if (Voluntario.Id == 0)
            {
                var result = await SqlAccess.InserirAsync(Voluntario);

                if (result != -1)
                {
                    await InserirVoluntarioOficinaAsync();
                    
                    ConfirmarButton.IsEnabled = false;
                    dadosAlterados = false;
                    MessageBox.Show("O cadastro do voluntário foi realizado.", "Voluntário Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                bool result = await SqlAccess.AtualizarAsync(Voluntario);

                if (result)
                {
                    await InserirVoluntarioOficinaAsync(true);
                    ConfirmarButton.IsEnabled = false;
                    dadosAlterados = false;
                    MessageBox.Show("O cadastro do voluntário foi atualizado.", "Voluntário Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                }                
            }

            TituloPaginaLabel.Focus();
        }

        private async Task<bool> InserirVoluntarioOficinaAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.TVoluntarioOficinas.DeletarAsync(Voluntario);

                if (result == null)
                {
                    MessageBox.Show("Infelizmente ocorreu um erro ao atualizar as oficinas do voluntpario.", "Erro em oficinas", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                    
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

                        var result = await SqlAccess.InserirAsync(alunoOficina);

                        if (result == -1)
                        {
                            MessageBox.Show("Infelizmente ocorreu um erro ao inserir as oficinas do voluntário.", "Erro em oficinas", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void ExibirIdade()
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
    }
}
