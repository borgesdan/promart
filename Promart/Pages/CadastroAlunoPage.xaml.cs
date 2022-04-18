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
using Promart.Controls;
using Microsoft.Win32;
using System.IO;

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
        List<AlunoVinculo> vinculos = new List<AlunoVinculo>();

        public CadastroAlunoPage() : this(new Aluno())
        {
        }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();

            Aluno = aluno;
            MatriculaPanel.Visibility = Visibility.Hidden;
            ComposicaoDataGrid.ItemsSource = vinculos;
            PreencherComboBoxes();

            //Eventos necessários dos controles
            ConfirmarButton.Click += async (object sender, RoutedEventArgs e) => await ConfirmarPaginaAsync();
            CancelarButton.Click += (object sender, RoutedEventArgs e) => CancelarPagina();
            NascimentoData.SelectedDateChanged += (object? sender, SelectionChangedEventArgs e) => ExibirIdade();
            SituacaoProjetoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => ExibirInfoSituacao();
            TurnoEscolarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => { TurnoProjetoCombo.SelectedIndex = TurnoEscolarCombo.SelectedIndex == 0 ? 1 : 0; };
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => AlterarHeaderAba();
            AbrirImagemButton.Click += (object sender, RoutedEventArgs e) => AbrirNovaImagem();
            AddMembroButton.Click += (object sender, RoutedEventArgs e) => AbrirCadastroVinculo();
            EditarMembroButton.Click += (object sender, RoutedEventArgs e) => EditarCadastroVinculo();
            ExcluirMembroButton.Click += (object sender, RoutedEventArgs e) => ExcluirCadastroVinculo();

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
            
            //Vai para o evento Page_Loaded.
        }       
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Carrega o nome do aluno e coloca o cursor para o fim do nome.
            NomeText.Text = Aluno.NomeCompleto;
            NomeText.Focus();
            NomeText.CaretIndex = NomeText.Text != null ? NomeText.Text.Length : 0;

            await PopularOficinas();
            
            if(Aluno.Id != 0)
            {
                await PreencherDados();
            }
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

            ConfirmarButton.IsEnabled = false;
            dadosAlterados = false;
        }

        private void CancelarPagina()
        {
            if (dadosAlterados)
            {
                if (!Helper.Controles.DadosAlteradosAviso())
                    return;
            }

            Helper.Controles.RemoverAba(Tab);
        }

        private async Task ConfirmarPaginaAsync()
        {
            Aluno.NomeCompleto = NomeText.Text.Trim();

            if (string.IsNullOrWhiteSpace(Aluno.NomeCompleto))
            {
                MessageBox.Show("Digite o nome o aluno antes de confirmar os dados.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

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

            if (Aluno.Id == 0)
            {
                long result = await SqlAccess.InserirAsync(Aluno);

                if (result != -1)
                {
                    await InserirVinculosAsync();
                    await InserirAlunoOficinaAsync();

                    Aluno.Matricula = GeradorMatricula.Get(Aluno.NomeCompleto);
                    Aluno.DataMatricula = DateTime.Now;
                    ConfirmarButton.IsEnabled = false;
                    dadosAlterados = false;
                    MessageBox.Show("O cadastro do aluno foi realizado e um número de matrícula foi gerado.", "Aluno cadastrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    MatriculaText.Text = Aluno.Matricula;
                    DataMatriculaText.Text = Aluno.DataMatriculaValue.ToString();
                    MatriculaPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                bool result = await SqlAccess.AtualizarAsync(Aluno);

                if (result)
                {
                    await InserirVinculosAsync(true);
                    await InserirAlunoOficinaAsync(true);
                    ConfirmarButton.IsEnabled = false;
                    dadosAlterados = false;
                    MessageBox.Show("O cadastro do aluno foi atualizado com sucesso", "Cadastro Atualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            MatriculaText.Focus();
        }

        private async Task<bool> InserirAlunoOficinaAsync(bool atualizar = false)
        {
            if (atualizar)
            {
                var result = await SqlAccess.TAlunoOficinas.DeletarAsync(Aluno);

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

                        var result = await SqlAccess.InserirAsync(alunoOficina);

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
                var result = await SqlAccess.TAlunoVinculos.DeletarAsync(Aluno);

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

                var result = await SqlAccess.InserirAsync(alunoVinculo);

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
            dadosAlterados = true;
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
                IdadeLabel.Content = string.Concat(Helper.Util.ObterIdade(nascimento), " anos");
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
                    SituacaoExpLabel.Content = "O aluno se encontra em outra situação.";
                    break;
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
            if(Aluno.FotoUrl != null)
            {
                string arquivo = $"{Helper.Diretorios.FOTOS_ALUNOS}/{Aluno.FotoUrl}";

                if (File.Exists(arquivo))
                {
                    FotoImage.ImageSource = new BitmapImage(new Uri(arquivo));
                }                
            }
        }

        private void AbrirCadastroVinculo()
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

        private void EditarCadastroVinculo()
        {
            if (ComposicaoDataGrid.SelectedIndex != -1)
            {
                AlunoVinculo vinculo = vinculos[ComposicaoDataGrid.SelectedIndex];

                if (vinculo != null)
                {
                    NovoMembroComposicaoWindow novoMembro = new NovoMembroComposicaoWindow(vinculo);
                    novoMembro.ShowDialog();

                    ComposicaoDataGrid.ItemsSource = null;
                    ComposicaoDataGrid.ItemsSource = vinculos;
                    DefinirAlteracaoDados();
                }
            }
        }

        private void ExcluirCadastroVinculo()
        {
            if (ComposicaoDataGrid.SelectedIndex != -1)
            {
                vinculos.RemoveAt(ComposicaoDataGrid.SelectedIndex);
                ComposicaoDataGrid.ItemsSource = null;
                ComposicaoDataGrid.ItemsSource = vinculos;
                DefinirAlteracaoDados();
            }
        }

        private async Task ObterVinculos()
        {
            var resultado = await SqlAccess.TAlunoVinculos.GetAsync(Aluno);

            if(resultado != null)
            {
                vinculos = new List<AlunoVinculo>(resultado);
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
            var resultado = await SqlAccess.TAlunoOficinas.GetAsync(Aluno);

            if(resultado != null)
            {
                foreach(var alunoOficina in resultado)
                {
                    foreach (var boxes in OficinasList.ItemsSource)
                    {
                        CheckBox checkBox = (CheckBox)boxes;
                        Oficina oficina = (Oficina)checkBox.Content;

                        if(oficina.Id == alunoOficina.IdOficina)
                        {
                            checkBox.IsChecked = true;
                        }
                    }
                }
            }
        }
    }
}
