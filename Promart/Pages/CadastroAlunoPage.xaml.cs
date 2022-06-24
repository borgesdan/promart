using Promart.Codes;
using Promart.Controls;
using Promart.Data;
using Promart.Models;
using Promart.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Text;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastrarAlunoPage.xaml
    /// </summary>
    public partial class CadastroAlunoPage : Page, IMainWindowPage
    {
        bool dadosCarregados = false;
        bool paginaCarregada = false;
        int idade = 0;
        public Aluno Aluno { get; private set; }

        public string TitleHeader { get; set; } = "Cadastro de Aluno";
        public bool CanClose { get; private set; } = true;
        public string CloseWarging => "Há dados alterados que não foram salvos. Deseja realmente fechar a página?";

        List<AlunoVinculo> vinculos = new();

        public CadastroAlunoPage() : this(new Aluno()) { }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();

            Aluno = aluno;
            MatriculaPanel.Visibility = Visibility.Hidden;
            ComposicaoDataGrid.ItemsSource = vinculos;
            PreencherComboBoxes();

            NomeText.Text = Aluno.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            ExibirInfoSituacao();
            DefinirEventos();
        }

        private void DefinirEventos()
        {
            //Eventos necessários dos controles
            ConfirmarButton.Click += async (object sender, RoutedEventArgs e) => await ConfirmarPaginaAsync();
            CancelarButton.Click += (object sender, RoutedEventArgs e) => MainWindow.CloseTab();
            AbrirImagemButton.Click += (object sender, RoutedEventArgs e) => AbrirNovaImagem();
            AddMembroButton.Click += (object sender, RoutedEventArgs e) => AbrirVinculo();
            EditarMembroButton.Click += (object sender, RoutedEventArgs e) => EditarVinculo();
            ExcluirMembroButton.Click += (object sender, RoutedEventArgs e) => ExcluirVinculo();
            ExcluirButton.Click += async (object sender, RoutedEventArgs e) => await ExcluirCadastro();
            ImprimirButton.Click += (object sender, RoutedEventArgs e) => Imprimir();
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => ValidarData();
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => ExibirIdade();
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => ExibirInfoSituacao();
            TurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => TurnoProjetoCombo.SelectedIndex = TurnoEscolarCombo.SelectedIndex == 0 ? 1 : 0;
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => MainWindow.SetHeaderTab(NomeText.Text.Trim());
            ComposicaoDataGrid.SelectionChanged += (object sender, SelectionChangedEventArgs e) => HabilitarEdicaoComposicao();

            //Eventos para confirmar alterações de dados ao sair da tela
            FotoImage.Changed += (object? sender, EventArgs e) => DefinirAlteracaoDados();
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
            VinculoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            MoradiaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            RendaCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            AnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            TurnoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirAlteracaoDados();
            BeneficiarioCheck.Click += (object sender, RoutedEventArgs e) => DefinirAlteracaoDados();
        }

        private void HabilitarEdicaoComposicao()
        {
            EditarMembroButton.IsEnabled = ComposicaoDataGrid.SelectedItem != null;
            ExcluirMembroButton.IsEnabled = ComposicaoDataGrid.SelectedItem != null;
        }
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PopularOficinas();

            if (!dadosCarregados && Aluno.Id != 0)
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
            NomeText.Text = Aluno.NomeCompleto;
            NascimentoData.SelectedDate = Aluno.DataNascimento;
            SexoCombo.SelectedIndex = Aluno.Sexo;
            RGText.Text = Aluno.RG;
            CPFText.Text = Aluno.CPF;
            CertidaoText.Text = Aluno.Certidao;

            NomeResponsavelText.Text = Aluno.NomeResponsavel;
            VinculoCombo.SelectedIndex = Aluno.VinculoFamiliar;
            MoradiaCombo.SelectedIndex = Aluno.TipoMoradia;
            RendaCombo.SelectedIndex = Aluno.Renda;

            BeneficiarioCheck.IsChecked = Aluno.IsBeneficiario;
            Telefone1Text.Text = Aluno.Contato1;
            Telefone2Text.Text = Aluno.Contato2;

            NomeEscolaText.Text = Aluno.EscolaNome;
            TurnoEscolarCombo.SelectedIndex = Aluno.TurnoEscolar;
            AnoEscolarCombo.SelectedIndex = Aluno.AnoEscolar;
            RuaText.Text = Aluno.EnderecoRua;
            BairroText.Text = Aluno.EnderecoBairro;
            NumeroText.Text = Aluno.EnderecoNumero;
            ComplementoText.Text = Aluno.EnderecoComplemento;

            SituacaoProjetoCombo.SelectedIndex = Aluno.SituacaoProjeto;
            TurnoProjetoCombo.SelectedIndex = Aluno.TurnoProjeto;
            ObservacoesText.Text = Aluno.Observacoes;

            MatriculaText.Text = Aluno.Matricula;
            DataMatriculaText.Text = Aluno.DataMatriculaValue.ToString();
            MatriculaPanel.Visibility = Visibility.Visible;
            CarregarFoto();

            await ObterVinculos();
            await ObterOficinas();

            ExibirInfoSituacao();
            ConfirmarButton.IsEnabled = false;
            ExcluirButton.IsEnabled = true;
            CanClose = true;
        }

        private bool ValidarDados()
        {
            NomeText.Text = NomeText.Text.Trim();
            RGText.Text = RGText.Text.Trim();
            CPFText.Text = CPFText.Text.Trim();
            CertidaoText.Text = CertidaoText.Text.Trim();
            NomeResponsavelText.Text = NomeResponsavelText.Text.Trim();
            Telefone1Text.Text = Telefone1Text.Text.Trim();
            Telefone2Text.Text = Telefone2Text.Text.Trim();
            NomeEscolaText.Text = NomeEscolaText.Text.Trim();
            RuaText.Text = RuaText.Text.Trim();
            BairroText.Text = BairroText.Text.Trim();
            NumeroText.Text = NumeroText.Text.Trim();
            ComplementoText.Text = ComplementoText.Text.Trim();
            ObservacoesText.Text = ObservacoesText.Text.Trim();

            if (string.IsNullOrWhiteSpace(NomeText.Text))
            {
                MessageBox.Show("Digite o nome o aluno antes de confirmar os dados.",
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return false;
            }
            
            StringBuilder avisos = new StringBuilder();
            int count = 1;

            if (idade < Aluno.FaixaEtariaMin)
            {
                avisos.AppendLine($"{count}- O aluno tem uma idade de {idade} anos que é menor do que a faixa etária do projeto: {Aluno.FaixaEtariaMin} anos.");
                avisos.AppendLine();
                count++;
            }
            else if (idade > Aluno.FaixaEtariaMax)
            {
                avisos.AppendLine($"{count} - O aluno tem uma idade {idade} anos que é maior do que a faixa etária do projeto: {Aluno.FaixaEtariaMax} anos.");
                avisos.AppendLine();
                count++;
            }           

            if (SituacaoProjetoCombo.SelectedIndex != (int)SituacaoAlunoTipo.Matriculado)
            {
                foreach (var checkBox in OficinasList.ItemsSource)
                {
                    CheckBox? c = checkBox as CheckBox;

                    if (c != null
                        && c.IsChecked != null
                        && c.IsChecked.Value)
                    {
                        avisos.AppendLine($"{count} - O aluno foi marcado nas oficinas mas ainda não está matriculado.");
                        avisos.AppendLine();
                        count++;
                        break;
                    }
                }                        
            }

            string relatorio = $"Foram verificadas algumas inconsistências:\n\n {avisos}";
            var result = MessageBox.Show($"{relatorio}\n Deseja continuar o cadastro?",
                "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.No)
                return false;

            return true;
        }

        private void AtribuirDados()
        {
            Aluno.NomeCompleto = NomeText.Text.Trim();
            Aluno.DataNascimento = NascimentoData.SelectedDate;
            Aluno.Sexo = SexoCombo.SelectedIndex;
            Aluno.RG = RGText.Text;
            Aluno.CPF = CPFText.Text;
            Aluno.Certidao = CertidaoText.Text;
            Aluno.VinculoFamiliar = VinculoCombo.SelectedIndex;
            Aluno.NomeResponsavel = NomeResponsavelText.Text;
            Aluno.IsBeneficiario = BeneficiarioCheck.IsChecked ?? false;
            Aluno.TipoMoradia = MoradiaCombo.SelectedIndex;
            Aluno.Renda = RendaCombo.SelectedIndex;
            Aluno.Contato1 = Telefone1Text.Text;
            Aluno.Contato2 = Telefone2Text.Text;
            Aluno.EscolaNome = NomeEscolaText.Text;
            Aluno.TurnoEscolar = TurnoEscolarCombo.SelectedIndex;
            Aluno.AnoEscolar = AnoEscolarCombo.SelectedIndex;
            Aluno.EnderecoRua = RuaText.Text;
            Aluno.EnderecoBairro = BairroText.Text;
            Aluno.EnderecoNumero = NumeroText.Text;
            Aluno.EnderecoComplemento = ComplementoText.Text;
            Aluno.EnderecoCidade = "Ipiaú";
            Aluno.EnderecoEstado = "Bahia";
            Aluno.EnderecoCEP = "45570-000";
            Aluno.SituacaoProjeto = SituacaoProjetoCombo.SelectedIndex;
            Aluno.TurnoProjeto = TurnoProjetoCombo.SelectedIndex;
            Aluno.Observacoes = ObservacoesText.Text;
        }

        private async Task ConfirmarPaginaAsync()
        {
            if (!ValidarDados())
                return;

            AtribuirDados();

            if (Aluno.Id == 0)
            {
                Aluno.Matricula = GeradorMatricula.Get(Aluno.NomeCompleto);
                Aluno.DataMatricula = DateTime.Now;

                long result = await SqlAccess.InsertAsync(Aluno);

                if (result != -1)
                {
                    await InserirVinculosAsync();
                    await InserirAlunoOficinaAsync();
                    ConfirmarButton.IsEnabled = false;
                    ExcluirButton.IsEnabled = true;
                    CanClose = true;
                    MessageBox.Show("O cadastro do aluno foi realizado e um número de matrícula foi gerado.", "Aluno cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    MatriculaText.Text = Aluno.Matricula;
                    DataMatriculaText.Text = Aluno.DataMatriculaValue.ToString();
                    MatriculaPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                bool result = await SqlAccess.UpdateAsync(Aluno);

                if (result)
                {
                    await InserirVinculosAsync(true);
                    await InserirAlunoOficinaAsync(true);
                    ConfirmarButton.IsEnabled = false;
                    ExcluirButton.IsEnabled = true;
                    CanClose = true;
                    MessageBox.Show("O cadastro do aluno foi atualizado com sucesso", "Cadastro Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            MatriculaText.Focus();
        }

        private async Task<bool> InserirAlunoOficinaAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.DeleteAllAsync<AlunoOficina, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");

                if (result == null)
                {
                    MessageBox.Show("Infelizmente ocorreu um erro ao atualizar as oficinas do aluno.", "Erro em oficinas", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        AlunoOficina alunoOficina = new AlunoOficina();
                        alunoOficina.IdAluno = Aluno.Id;
                        alunoOficina.IdOficina = oficina.Id;

                        var result = await SqlAccess.InsertAsync(alunoOficina);

                        if (result == -1)
                        {
                            MessageBox.Show("Infelizmente ocorreu um erro ao inserir as oficinas do aluno.", "Erro em oficinas", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private async Task<bool> InserirVinculosAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.DeleteAllAsync<AlunoVinculo, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");

                if (result == null)
                {
                    MessageBox.Show("Infelizmente ocorreu um erro ao atualizar os vínculos familiares do aluno.", "Erro em vínculos familiares", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            for (int i = 0; i < vinculos.Count; i++)
            {
                AlunoVinculo alunoVinculo = vinculos[i];
                alunoVinculo.IdAluno = Aluno.Id;

                var result = await SqlAccess.InsertAsync(alunoVinculo);

                if (result == -1)
                {
                    MessageBox.Show("Infelizmente ocorreu um erro ao inserir os vínculos familiares do aluno.", "Erro em vínculos familiares", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        private void DefinirAlteracaoDados()
        {
            CanClose = false;
            ConfirmarButton.IsEnabled = true;
        }

        private void PreencherComboBoxes()
        {
            SexoCombo.ItemsSource = ComboBoxTipos.TipoSexoNumerado;
            VinculoCombo.ItemsSource = ComboBoxTipos.TipoVinculoFamiliarNumerado;
            MoradiaCombo.ItemsSource = ComboBoxTipos.TipoMoradiaNumerado;
            RendaCombo.ItemsSource = ComboBoxTipos.TipoRendaNumerado;
            AnoEscolarCombo.ItemsSource = ComboBoxTipos.TipoAnoEscolarNumerado;
            TurnoEscolarCombo.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNumerado;
            SituacaoProjetoCombo.ItemsSource = ComboBoxTipos.TipoAlunoSituacaoNumerado;
            TurnoProjetoCombo.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNumerado;
        }

        private void ExibirIdade()
        {
            if (NascimentoData.SelectedDate != null && NascimentoData.SelectedDate.HasValue)
            {
                DateTime nascimento = NascimentoData.SelectedDate.Value;
                idade = Helper.Util.ObterIdade(nascimento);
                IdadeLabel.Content = string.Concat(idade, " anos");
                IdadeLabel.Visibility = Visibility.Visible;
            }
            else
            {
                IdadeLabel.Visibility = Visibility.Hidden;
            }
        }

        private void ExibirInfoSituacao()
        {
            switch (SituacaoProjetoCombo.SelectedIndex)
            {
                case (int)SituacaoAlunoTipo.Inscrito:
                    SituacaoExpText.Text = "O aluno está inscrito no projeto e espera o processo de aprovação.";
                    break;
                case (int)SituacaoAlunoTipo.Aprovado:
                    SituacaoExpText.Text = "O aluno está inscrito e aprovado para ser matriculado nas oficinas.";
                    break;
                case (int)SituacaoAlunoTipo.EmEspera:
                    SituacaoExpText.Text = "O aluno está inscrito mas em situação de espera para uma possível oportunidade de matrícula.";
                    break;
                case (int)SituacaoAlunoTipo.Matriculado:
                    SituacaoExpText.Text = "O aluno está matriculado e apto a participar das oficinas.";
                    break;
                case (int)SituacaoAlunoTipo.NaoAprovado:
                    SituacaoExpText.Text = "O aluno foi inscrito mas não foi aprovado.";
                    break;
                case (int)SituacaoAlunoTipo.Desistente:
                    SituacaoExpText.Text = "O aluno estava matriculado, mas não frequentando e foi considerado desistente";
                    break;
                case (int)SituacaoAlunoTipo.Exaluno:
                    SituacaoExpText.Text = "O aluno é um ex-participante do projeto.";
                    break;
                case (int)SituacaoAlunoTipo.NaoEspecificado:
                    SituacaoExpText.Text = "O aluno se encontra em uma situação excepcional e não especificada.";
                    break;
            }
        }        

        private void AbrirNovaImagem()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var result = Helper.Util.AbrirSalvarImagem(Helper.Diretorios.FOTOS_ALUNOS, guid.ToString());

                if (result != null)
                {
                    Aluno.FotoUrl = result;
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
            if (Aluno.FotoUrl != null)
            {
                string arquivo = $"{Helper.Diretorios.FOTOS_ALUNOS}/{Aluno.FotoUrl}";

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

        private void AbrirVinculo()
        {
            NovoMembroComposicaoWindow novoMembro = new NovoMembroComposicaoWindow();
            var result = novoMembro.ShowDialog();

            if (result == true && novoMembro.Vinculo != null)
            {
                vinculos.Add(novoMembro.Vinculo);
                ComposicaoDataGrid.ItemsSource = null;
                ComposicaoDataGrid.ItemsSource = vinculos;
                DefinirAlteracaoDados();
            }
        }

        private void EditarVinculo()
        {
            if (ComposicaoDataGrid.SelectedItem != null)
            {
                AlunoVinculo vinculo = vinculos[ComposicaoDataGrid.SelectedIndex];

                if (vinculo != null)
                {
                    NovoMembroComposicaoWindow novoMembro = new(vinculo);
                    novoMembro.ShowDialog();

                    ComposicaoDataGrid.ItemsSource = null;
                    ComposicaoDataGrid.ItemsSource = vinculos;
                    DefinirAlteracaoDados();
                }
            }
        }

        private void ExcluirVinculo()
        {
            if (ComposicaoDataGrid.SelectedItem != null)
            {
                AlunoVinculo vinculo = vinculos[ComposicaoDataGrid.SelectedIndex];

                var result = MessageBox.Show($"Deseja realmente excluir {vinculo.NomeFamiliar}?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;

                vinculos.RemoveAt(ComposicaoDataGrid.SelectedIndex);
                ComposicaoDataGrid.ItemsSource = null;
                ComposicaoDataGrid.ItemsSource = vinculos;
                DefinirAlteracaoDados();
            }
        }

        private void Imprimir()
        {
            PrintDialog printDialog = new PrintDialog();

            AlunoDadosPrintPage cadastroAlunoPage = new AlunoDadosPrintPage(this);

            bool? result = printDialog.ShowDialog();

            if (result == true)
            {
                printDialog.PrintVisual(cadastroAlunoPage, "Cadastro do Aluno");
            }
        }

        private void ValidarData()
        {
            if(NascimentoData.SelectedDate != null && NascimentoData.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("A data selecionada é maior do que a data de hoje.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Question);
                NascimentoData.SelectedDate = null;
            }
        }

        private async Task ObterVinculos()
        {
            //var resultado = await SqlAccess.TAlunoVinculos.GetAsync(Aluno);
            var resultado = await SqlAccess.GetAllAsync<AlunoVinculo, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");

            if (resultado != null)
            {
                vinculos = new List<AlunoVinculo>();

                foreach (var item in resultado)
                {
                    vinculos.Add(AlunoVinculo.DynamicConverter(item));
                }

                ComposicaoDataGrid.ItemsSource = vinculos;
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao receber os vínculos do aluno.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task PopularOficinas()
        {
            await Helper.Controles.PopularOficinasListComCheckBoxAsync(OficinasList, (object sender, RoutedEventArgs e) => DefinirAlteracaoDados());
        }

        private async Task ObterOficinas()
        {
            //var resultado = await SqlAccess.TAlunoOficinas.GetAsync(Aluno);
            var resultado = await SqlAccess.GetAllAsync<AlunoOficina, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");

            if (resultado != null)
            {
                foreach (var item in resultado)
                {
                    AlunoOficina alunoOficina = AlunoOficina.DynamicConvert(item);

                    foreach (var boxes in OficinasList.ItemsSource)
                    {
                        CheckBox checkBox = (CheckBox)boxes;
                        Oficina oficina = (Oficina)checkBox.Content;

                        if (oficina.Id == alunoOficina.IdOficina)
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
                await SqlAccess.DeleteAllAsync<AlunoOficina, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");
                await SqlAccess.DeleteAllAsync<AlunoVinculo, Aluno>(Aluno, nameof(Aluno.Id), "IdAluno");
                await SqlAccess.DeleteAsync(Aluno);

                MainWindow.Instance?.CloseCurrentTab();
            }
        }


    }
}
