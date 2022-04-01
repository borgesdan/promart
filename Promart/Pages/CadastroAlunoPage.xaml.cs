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
        public TabItem? AlunoTab { get; set; }

        public CadastroAlunoPage() : this(new Aluno())
        {
        }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();
            Aluno = aluno;
            ConfirmarButton.Click += (object sender, RoutedEventArgs e) => ConfirmarPagina();
            CancelarButton.Click += CancelarButton_Click;
            NascimentoData.SelectedDateChanged += NascimentoData_SelectedDateChanged;
            SituacaoProjetoCombo.SelectionChanged += SituacaoProjetoCombo_SelectionChanged;

            //Eventos para confirmar alterações de dados ao sair da tela
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            RGText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            CPFText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            CertidaoText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            Telefone1Text.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            Telefone2Text.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NomeEscolaText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            RuaText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            BairroText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NumeroText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            ComplementoText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NomeResponsavelText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            ObservacoesText.TextChanged += (object sender, TextChangedEventArgs e) => dadosAlterados = true;
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoVinculoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoMoradiaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoRendaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoTurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoAnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            TipoTurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => dadosAlterados = true;
            BeneficiarioCheck.Click += (object sender, RoutedEventArgs e) => dadosAlterados = true;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Carrega o nome do aluno
            NomeText.Text = Aluno.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            //Popula a lista de oficinas
            var oficinas = SqlAccess.GetDados<Oficina>();
            List<CheckBox> checkBoxes = new();

            foreach (var o in oficinas)
            {
                CheckBox checkBox = new();
                checkBox.Content = o;
                checkBox.Click += (object sender, RoutedEventArgs e) => dadosAlterados = true;
                checkBoxes.Add(checkBox);
            }

            TipoOficinasList.ItemsSource = checkBoxes;
        }

        private void NomeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            dadosAlterados = true;
        }

        private void NascimentoData_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DatePicker date = ((DatePicker)sender);

                if (date.SelectedDate != null && date.SelectedDate.HasValue)
                {
                    DateTime nascimento = date.SelectedDate.Value;
                    IdadeLabel.Content = string.Concat(Helper.Util.ObterIdade(nascimento), " anos");

                    IdadeLabel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                IdadeLabel.Visibility = Visibility.Hidden;
            }
        }

        private void SituacaoProjetoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? combo = sender as ComboBox;

            if (combo != null)
            {
                switch (combo.SelectedIndex)
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
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlunoTab != null && AlunoTab.Parent is TabControl)
            {
                if (dadosAlterados)
                {
                    MainWindow? window = Window.GetWindow(this) as MainWindow;
                    window?.TrocarVisibilidadeTelaPreta();
                    
                    MessageBoxResult result = MessageBox.Show("Alguns dados foram alterados, deseja sair sem confirma-los?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    window?.TrocarVisibilidadeTelaPreta();

                    if (result == MessageBoxResult.No)
                    {                        
                        return;
                    }
                }

                TabControl? tabControl = AlunoTab.Parent as TabControl;
                tabControl?.Items.Remove(AlunoTab);
            }
        }


        public void ConfirmarPagina()
        {
            Aluno.NomeCompleto = NomeText.Text.Trim();
            Aluno.DataNascimento = NascimentoData.SelectedDate;
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
            Aluno.EnderecoCEP = 45570000;
            Aluno.SituacaoProjeto = SituacaoProjetoCombo.SelectedIndex;
            Aluno.TurnoProjeto = TipoTurnoEscolarCombo.Text;
            Aluno.Observacoes = ObservacoesText.Text;

            if (Aluno.Id == 0)
            {
                SqlAccess.Inserir(Aluno);
                InserirAlunoOficina();
            }
            else
            {
                SqlAccess.Atualizar(Aluno);
                InserirAlunoOficina(true);
            }

            ConfirmarButton.IsEnabled = false;
        }

        private void InserirAlunoOficina(bool atualizar = false)
        {
            if (atualizar)
            {
                SqlAccess.TAlunoOficinas.Deletar(Aluno);
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

                        SqlAccess.Inserir(alunoOficina);
                    }
                }
            }
        }
    }
}
