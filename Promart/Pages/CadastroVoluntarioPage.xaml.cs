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
using System.IO;
using Promart.Controls;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastroVoluntarioPage.xaml
    /// </summary>
    public partial class CadastroVoluntarioPage : Page
    {
        bool dadosCarregados = false;
        bool paginaCarregada = false;
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

            //Eventos necessários dos controles
            NascimentoData.SelectedDateChanged += (s, e) => ExibirIdade();
            CancelarButton.Click += CancelarButton_Click;
            ConfirmarButton.Click += async (s, e) => await ConfirmarPagina();
            AbrirImagemButton.Click += (object sender, RoutedEventArgs e) => AbrirNovaImagem();
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => AlterarHeaderAba();
            ExcluirButton.Click += async (object sender, RoutedEventArgs e) => await ExcluirCadastro();

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
            FotoImage.Changed += (object? sender, EventArgs e) => DefinirAlteracaoDados();

            NomeText.Text = Voluntario.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Helper.Controles.PopularOficinasListComCheckBoxAsync(OficinasList, (object o, RoutedEventArgs a) => DefinirAlteracaoDados());

            if (!dadosCarregados && Voluntario.Id != 0)
            {
                await PreencherDados();
                dadosCarregados = true;
            }

            if (paginaCarregada)
            {
                await ObterOficinas();
            }

            paginaCarregada = true;
        }

        private async Task PreencherDados()
        {
            NomeText.Text = Voluntario.NomeCompleto;
            NascimentoData.SelectedDate = Voluntario.DataNascimento;
            SexoCombo.SelectedIndex = Voluntario.Sexo;
            RGText.Text = Voluntario.RG;
            CPFText.Text = Voluntario.CPF;
            ProfissaoText.Text = Voluntario.Profissao;
            EscolaridadeText.Text = Voluntario.Escolaridade;
            Telefone1Text.Text = Voluntario.Contato1;
            Telefone2Text.Text = Voluntario.Contato2;
            EmailText.Text = Voluntario.Email;
            RuaText.Text = Voluntario.EnderecoRua;
            BairroText.Text = Voluntario.EnderecoBairro;
            NumeroText.Text = Voluntario.EnderecoNumero;
            ComplementoText.Text = Voluntario.EnderecoComplemento;
            CidadeText.Text = Voluntario.EnderecoCidade;
            CEPText.Text = Voluntario.EnderecoCEP;            

            CarregarFoto();

            await ObterOficinas();

            ConfirmarButton.IsEnabled = false;
            ExcluirButton.IsEnabled = true;
            dadosAlterados = false;

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

            MainWindow.Instance?.FecharAbaAtual();
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
            Voluntario.Profissao = ProfissaoText.Text.Trim();
            Voluntario.RG = RGText.Text.Trim();
            Voluntario.CPF = CPFText.Text.Trim();
            Voluntario.Contato1 = Telefone1Text.Text.Trim();
            Voluntario.Contato2 = Telefone2Text.Text.Trim();
            Voluntario.Email = EmailText.Text.Trim();
            Voluntario.EnderecoRua = RuaText.Text.Trim();
            Voluntario.EnderecoBairro = BairroText.Text.Trim();
            Voluntario.EnderecoNumero = NumeroText.Text.Trim();
            Voluntario.EnderecoComplemento = ComplementoText.Text.Trim();
            Voluntario.EnderecoCidade = CidadeText.Text.Trim();
            Voluntario.EnderecoEstado = "Bahia";
            Voluntario.EnderecoCEP = CEPText.Text.Trim();
            Voluntario.Observacoes = ObservacoesText.Text.Trim();            
            Voluntario.Escolaridade = EscolaridadeText.Text.Trim();

            if (Voluntario.Id == 0)
            {
                var result = await SqlAccess.InserirAsync(Voluntario);

                if (result != -1)
                {
                    await InserirVoluntarioOficinaAsync();
                    
                    ConfirmarButton.IsEnabled = false;
                    ExcluirButton.IsEnabled = true;
                    Voluntario.DataCadastro = DateTime.Now;
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
                    ExcluirButton.IsEnabled = true;
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

        private void AlterarHeaderAba()
        {
            if (Tab != null)
            {
                if (Tab.Header is string)
                {
                    Tab.Header = NomeText.Text;
                }
                else if (Tab.Header is TabHeaderContentControl)
                {
                    var header = Tab.Header as TabHeaderContentControl;

                    if (header != null)
                    {
                        header.HeaderText = NomeText.Text;
                    }
                }
            }
        }

        private void AbrirNovaImagem()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var result = Helper.Util.AbrirSalvarImagem(Helper.Diretorios.FOTOS_VOLUNTARIOS, guid.ToString());

                if (result != null)
                {
                    Voluntario.FotoUrl = result;
                    CarregarFoto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregar a imagem.\n\nErro: {ex.Message}", "Erro de carregamento", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CarregarFoto()
        {
            if (Voluntario.FotoUrl != null)
            {
                string arquivo = $"{Helper.Diretorios.FOTOS_VOLUNTARIOS}/{Voluntario.FotoUrl}";

                if (File.Exists(arquivo))
                {
                    FotoImage.ImageSource = new BitmapImage(new Uri(arquivo));
                }
            }
        }

        private async Task ObterOficinas()
        {
            var resultado = await SqlAccess.TVoluntarioOficinas.GetAsync(Voluntario);

            if (resultado != null)
            {
                foreach (var voluntarioOficina in resultado)
                {
                    foreach (var boxes in OficinasList.ItemsSource)
                    {
                        CheckBox checkBox = (CheckBox)boxes;
                        Oficina oficina = (Oficina)checkBox.Content;

                        if (oficina.Id == voluntarioOficina.IdOficina)
                        {
                            checkBox.IsChecked = true;
                        }
                    }
                }
            }
        }

        private async Task ExcluirCadastro()
        {
            var result = MessageBox.Show("Deseja realmente excluir esse cadastro? Essa alteração não pode ser desfeita.", "Excluir Cadastro", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await SqlAccess.TVoluntarioOficinas.DeletarAsync(Voluntario);
                await SqlAccess.DeletarAsync(Voluntario);

                MainWindow.Instance?.FecharAbaAtual();
            }
        }
    }
}
