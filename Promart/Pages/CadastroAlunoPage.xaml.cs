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
using Promart.Windows;
using System.Text.RegularExpressions;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastrarAlunoPage.xaml
    /// </summary>
    public partial class CadastroAlunoPage : Page
    {
        bool dadosAlterados = false;
        public Aluno Aluno { get; private set; }
        public TabItem? Tab { get; set; }

        public CadastroAlunoPage() : this(new Aluno())
        {
        }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();
            Aluno = aluno;

            //Eventos necessários dos controles
            ConfirmarButton.Click += (object sender, RoutedEventArgs e) => ConfirmarPaginaAsync();
            CancelarButton.Click += CancelarButton_Click;
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) =>
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
            };
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                switch (SituacaoProjetoCombo.SelectedIndex)
                {
                    case 0:
                        SituacaoExpLabel.Content = "O aluno está inscrito e solicita aprovação.";
                        break;
                    case 1:
                        SituacaoExpLabel.Content = "O aluno está aprovado para matrícula.";
                        break;
                    case 2:
                        SituacaoExpLabel.Content = "O aluno está na lista de espera.";
                        break;
                    case 3:
                        SituacaoExpLabel.Content = "O aluno está matriculado no projeto.";
                        break;
                    case 4:
                        SituacaoExpLabel.Content = "O aluno foi inscrito mas não aprovado.";
                        break;
                    case 5:
                        SituacaoExpLabel.Content = "O aluno estava matriculado mas desistiu.";
                        break;
                    default:
                        SituacaoExpLabel.Content = "O índice da seleção está fora dos limites.";
                        break;
                }
            };
            TipoTurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => { TipoTurnoProjetoCombo.SelectedIndex = TipoTurnoEscolarCombo.SelectedIndex == 0 ? 1 : 0; };
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => { if (Tab != null) Tab.Header = NomeText.Text; };
            ComposicaoDataGrid.ItemsSource = new List<AlunoVinculo>();

            //TODO: Desabilitado pois a tecla TAB não funciona corretamente
            //CPFText.PreviewKeyDown += (sender, e) => { if (!Helper.Util.VerificarSomenteNumero(e.Key)) e.Handled = true; };
            //CPFText.PreviewTextInput += (sender, e) => Helper.Util.FormatarCPF(CPFText);


            //Eventos para confirmar alterações de dados ao sair da tela
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            RGText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            CPFText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            CertidaoText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            Telefone1Text.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            Telefone2Text.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NomeEscolaText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            RuaText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            BairroText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NumeroText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            ComplementoText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NomeResponsavelText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            ObservacoesText.TextChanged += (object sender, TextChangedEventArgs e) => DefinirAlteracaoDados();
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoVinculoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoMoradiaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoRendaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoTurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoAnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoTurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TipoTurnoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            BeneficiarioCheck.Click += (object sender, RoutedEventArgs e) => DefinirAlteracaoDados();

            //Vai para o evento Page_Loaded.
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Carrega o nome do aluno e coloca o cursor para o fim do nome.
            NomeText.Text = Aluno.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            //Tenta popular a lista de oficinas       
            await Helper.Controles.PopularOficinasListComCheckBoxAsync(TipoOficinasList, (object sender, RoutedEventArgs e) => DefinirAlteracaoDados());
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

        public async void ConfirmarPaginaAsync()
        {
            Aluno.NomeCompleto = NomeText.Text.Trim();

            if (string.IsNullOrWhiteSpace(Aluno.NomeCompleto))
            {
                MessageBox.Show("Digite o nome o aluno antes de confirmar os dados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Aluno.DataNascimento = NascimentoData.SelectedDate;
            Aluno.Sexo = TipoSexoCombo.SelectedIndex;
            Aluno.RG = RGText.Text;
            Aluno.CPF = CPFText.Text;
            Aluno.Certidao = CertidaoText.Text;
            Aluno.VinculoFamiliar = TipoVinculoCombo.SelectedIndex;
            Aluno.NomeResponsavel = NomeResponsavelText.Text;
            Aluno.IsBeneficiario = BeneficiarioCheck.IsChecked ?? false;
            Aluno.TipoCasa = TipoMoradiaCombo.SelectedIndex;
            Aluno.Renda = TipoRendaCombo.SelectedIndex;
            Aluno.Contato1 = Telefone1Text.Text;
            Aluno.Contato2 = Telefone2Text.Text;
            Aluno.EscolaNome = NomeEscolaText.Text;
            Aluno.TurnoEscolar = TipoTurnoEscolarCombo.Text;
            Aluno.AnoEscolar = TipoAnoEscolarCombo.SelectedIndex;
            Aluno.EnderecoRua = RuaText.Text;
            Aluno.EnderecoBairro = BairroText.Text;
            Aluno.EnderecoNumero = NumeroText.Text;
            Aluno.EnderecoComplemento = ComplementoText.Text;
            Aluno.EnderecoCidade = "Ipiaú";
            Aluno.EnderecoEstado = "Bahia";
            Aluno.EnderecoCEP = "45570-000";
            Aluno.SituacaoProjeto = SituacaoProjetoCombo.SelectedIndex;
            Aluno.TurnoProjeto = TipoTurnoEscolarCombo.Text;
            Aluno.Observacoes = ObservacoesText.Text;

            if (Aluno.Id == 0)
            {
                long result = await SqlAccess.InserirAsync(Aluno);

                if (result != -1)
                {
                    await InserirAlunoOficinaAsync();
                    ConfirmarButton.IsEnabled = false;
                    MessageBox.Show("O aluno foi cadastrado com sucesso", "Aluno cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                bool result = await SqlAccess.AtualizarAsync(Aluno);

                if (result)
                {
                    await InserirAlunoOficinaAsync(true);
                    ConfirmarButton.IsEnabled = false;
                    MessageBox.Show("O cadastro do aluno foi atualizado com sucesso", "Cadastro Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                }                    
            }            
        }

        private async Task InserirAlunoOficinaAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.TAlunoOficinas.DeletarAsync(Aluno);

                if (result == null)
                    return;
            }

            foreach (var checkBox in TipoOficinasList.ItemsSource)
            {
                CheckBox? c = checkBox as CheckBox;

                if (c != null
                    && c.IsChecked != null
                    && c.IsChecked.Value)
                {
                    Oficina? oficina = c.Content as Oficina;

                    if (oficina != null)
                    {
                        AlunoOficina alunoOficina = new AlunoOficina();
                        alunoOficina.IdAluno = Aluno.Id;
                        alunoOficina.IdOficina = oficina.Id;

                        await SqlAccess.InserirAsync(alunoOficina);
                    }
                }
            }
        }
    }
}
