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
    public partial class CadastroVoluntarioPage : Page, IMainWindowPage
    {
        bool dadosCarregados = false;
        bool paginaCarregada = false;
        public Voluntario Voluntario { get; private set; }

        public string TitleHeader { get; set; } = "Cadastro de Voluntário";
        public bool CanClose { get; private set; } = true;
        public string CloseWarging => "Há dados alterados que não foram salvos. Deseja realmente fechar a página?";

        public CadastroVoluntarioPage() : this(new Voluntario())
        {
        }

        public CadastroVoluntarioPage(Voluntario voluntario)
        {
            InitializeComponent();
            Voluntario = voluntario;
            SexoCombo.ItemsSource = ComboBoxTipos.TipoSexoNumerado;

            NomeText.Text = Voluntario.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            DefinirEventos();
        }

        private void DefinirEventos()
        {
            //Eventos necessários dos controles
            NascimentoData.SelectedDateChanged += (s, e) => ExibirIdade();
            CancelarButton.Click += (object sender, RoutedEventArgs e) => MainWindow.CloseTab();
            ConfirmarButton.Click += async (s, e) => await ConfirmarPagina();
            AbrirImagemButton.Click += (object sender, RoutedEventArgs e) => AbrirNovaImagem();
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => MainWindow.SetHeaderTab(NomeText.Text);
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
            CanClose = true;

        }

        private void DefinirAlteracaoDados()
        {
            CanClose = false;
            ConfirmarButton.IsEnabled = true;
        }
        
        private bool ValidarDados()
        {
            NomeText.Text = NomeText.Text.Trim();
            ProfissaoText.Text = ProfissaoText.Text.Trim();
            RGText.Text = RGText.Text.Trim();
            CPFText.Text = CPFText.Text.Trim();
            Telefone1Text.Text = Telefone1Text.Text.Trim();
            Telefone2Text.Text = Telefone2Text.Text.Trim();
            EmailText.Text = EmailText.Text.Trim();
            RuaText.Text = RuaText.Text.Trim();
            BairroText.Text = BairroText.Text.Trim();
            NumeroText.Text = NumeroText.Text.Trim();
            ComplementoText.Text = ComplementoText.Text.Trim();
            CidadeText.Text = CidadeText.Text.Trim();
            CEPText.Text = CEPText.Text.Trim();
            ObservacoesText.Text = ObservacoesText.Text.Trim();
            EscolaridadeText.Text = EscolaridadeText.Text.Trim();

            if (string.IsNullOrWhiteSpace(NomeText.Text))
            {
                MessageBox.Show("Digite o nome o voluntário antes de confirmar os dados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        private void AtribuirDados()
        {
            Voluntario.NomeCompleto = NomeText.Text;
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
            Voluntario.Escolaridade = EscolaridadeText.Text;
        }

        private async Task ConfirmarPagina()
        {
            if (!ValidarDados())
                return;

            AtribuirDados();

            if (Voluntario.Id == 0)
            {
                var result = await SqlAccess.InsertAsync(Voluntario);

                if (result != -1)
                {
                    await InserirVoluntarioOficinaAsync();
                    
                    ConfirmarButton.IsEnabled = false;
                    ExcluirButton.IsEnabled = true;
                    Voluntario.DataCadastro = DateTime.Now;
                    CanClose = true;
                    MessageBox.Show("O cadastro do voluntário foi realizado.", "Voluntário Cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                bool result = await SqlAccess.UpdateAsync(Voluntario);

                if (result)
                {
                    await InserirVoluntarioOficinaAsync(true);
                    ConfirmarButton.IsEnabled = false;
                    ExcluirButton.IsEnabled = true;
                    CanClose = true;
                    MessageBox.Show("O cadastro do voluntário foi atualizado.", "Voluntário Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                }                
            }

            TituloPaginaLabel.Focus();
        }

        private async Task<bool> InserirVoluntarioOficinaAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.DeleteAllAsync<VoluntarioOficina, Voluntario>(Voluntario, nameof(Voluntario.Id), "IdVoluntario");

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

                        var result = await SqlAccess.InsertAsync(alunoOficina);

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
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(arquivo);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    FotoImage.ImageSource = bitmap;
                }
            }
        }

        private async Task ObterOficinas()
        {            
            var resultado = await SqlAccess.GetAllAsync<VoluntarioOficina, Voluntario>(Voluntario, nameof(Voluntario.Id), "IdVoluntario");

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
                await SqlAccess.DeleteAllAsync<VoluntarioOficina, Voluntario>(Voluntario, nameof(Voluntario.Id), "IdVoluntario");
                await SqlAccess.DeleteAsync(Voluntario);

                MainWindow.Instance?.CloseCurrentTab();
            }
        }
    }
}
