using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using Microsoft.Win32;
using Promart.Codes;
using Promart.Data;
using Promart.Windows;
using Promart.Models;
using System.IO;

namespace Promart.Pages
{


    /// <summary>
    /// Interaction logic for TabelaDadosPage.xaml
    /// </summary>
    public partial class TabelaDadosPage : Page
    {
        int selectedIndex = -1;
        IEnumerable<Voluntario>? voluntarios = new List<Voluntario>();
        RelatorioAluno relatorioAluno = new RelatorioAluno(new List<Aluno>());

        public TabelaDadosPage()
        {
            InitializeComponent();
            DesabilitarControles();

            FiltrarButton.Click += (object sender, RoutedEventArgs e) => Filtrar();
            ExportarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => Exportar();
            SelecionarButton.Click += async (object sender, RoutedEventArgs e) => await DefinirEstadoRelatorio();
            FiltroSelecaoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirControleValor();
            DadosDataGrid.PreviewKeyDown += (object sender, KeyEventArgs e) => RemoverColuna(e);
            DadosDataGrid.PreviewKeyDown += (object sender, KeyEventArgs e) => AbrirItem(e);
        }

        private async void AbrirItem(KeyEventArgs e)
        {
            if(selectedIndex == 0 && e.Key == Key.Enter)
            {
                var aluno =  (Aluno)DadosDataGrid.SelectedItem;
                var resultado = await SqlAccess.GetDadoAsync<Aluno>(aluno.Id);

                if(resultado != null)
                {
                    MainWindow.Instance?.AbrirNovaAba(aluno.NomeCompleto ?? "Aluno Selecionado", new CadastroAlunoPage(resultado));
                }                
            }
        }

        private void RemoverColuna(KeyEventArgs e)
        {
            if((e.KeyboardDevice.IsKeyDown(Key.LeftShift) || e.KeyboardDevice.IsKeyDown(Key.Right))
                && e.KeyboardDevice.IsKeyDown(Key.Delete))
            {
                DadosDataGrid.Columns.Remove(DadosDataGrid.CurrentColumn);
            }
            else if (e.Key == Key.Delete)
            {
                DadosDataGrid.Items.Remove(DadosDataGrid.SelectedItem);                
            }
        }

        private void DesabilitarControles()
        {
            FiltrarButton.IsEnabled = false;
            FiltroSelecaoCombo.IsEnabled = false;
            ComboValor.IsEnabled = false;
            TextoValor.IsEnabled = false;
            DataValor.IsEnabled = false;
            ExportarCombo.IsEnabled = false;
        }

        private void HabilitarControles()
        {
            FiltrarButton.IsEnabled = true;
            FiltroSelecaoCombo.IsEnabled = true;
            ComboValor.IsEnabled = true;
            TextoValor.IsEnabled = true;
            DataValor.IsEnabled = true;
            ExportarCombo.IsEnabled = true;
        }

        private async Task DefinirEstadoRelatorio()
        {
            SelecionarButton.IsEnabled = false;

            switch (TipoTabelaCombo.SelectedIndex)
            {
                case 0:
                    selectedIndex = TipoTabelaCombo.SelectedIndex;
                    var alunos = await SqlAccess.GetDadosAsync<Aluno>();

                    if (alunos != null)
                    {
                        relatorioAluno = new RelatorioAluno(alunos);
                        DefinirRelatorio(alunos);
                        SelecionarButton.IsEnabled = true;
                        HabilitarControles();
                    }
                    break;
                case 1:
                    selectedIndex = TipoTabelaCombo.SelectedIndex;
                    var voluntarios = await SqlAccess.GetDadosAsync<Aluno>();

                    if (voluntarios != null)
                    {
                        //PopularDataGrid(alunos);
                    }
                    break;
            }
        }

        private void DefinirRelatorio(IEnumerable<Aluno> alunos)
        {
            PopularDataGrid(alunos);
            FiltroSelecaoCombo.ItemsSource = RelatorioAluno.TiposFiltro;
            FiltroSelecaoCombo.SelectedIndex = 0;
        }

        private void PopularDataGrid(IEnumerable<Aluno> alunos)
        {
            //DadosDataGrid.ItemsSource = alunos;
            
            DadosDataGrid.Items.Clear();
            foreach(var aluno in alunos)
            {
                DadosDataGrid.Items.Add(aluno);
            }
            relatorioAluno.PopularColunasDataGrid(DadosDataGrid);
        }

        private void DefinirControleValor()
        {
            if (FiltroSelecaoCombo.SelectedValue != null && FiltroSelecaoCombo.SelectedValue is string nomeFiltro)
            {
                FiltroControleType tipo = relatorioAluno.VerificarTipoFiltro(nomeFiltro);

                switch (tipo)
                {
                    case FiltroControleType.Texto:
                        AlterarVisibilidadeFiltroValor(TextoValor);
                        break;
                    case FiltroControleType.Data:
                        AlterarVisibilidadeFiltroValor(DataValor);
                        break;
                    case FiltroControleType.ComboBox:
                        AlterarVisibilidadeFiltroValor(ComboValor);
                        relatorioAluno.PopularComboValor(nomeFiltro, ComboValor);
                        ComboValor.SelectedIndex = 0;
                        break;
                }
            }
        }

        void AlterarVisibilidadeFiltroValor(Control controle)
        {
            ComboValor.Visibility = Visibility.Collapsed;
            DataValor.Visibility = Visibility.Collapsed;
            TextoValor.Visibility = Visibility.Collapsed;

            controle.Visibility = Visibility.Visible;
        }

        private void Filtrar()
        {
            FiltrarButton.IsEnabled = false;

            if (selectedIndex == 0)
            {
                IEnumerable<Aluno> resultado = new List<Aluno>();

                if (TextoValor.Visibility == Visibility.Visible)
                {
                    resultado = relatorioAluno.Filtrar((string)FiltroSelecaoCombo.SelectedValue, TextoValor.Text.Trim());
                }
                else if (ComboValor.Visibility == Visibility.Visible)
                {
                    resultado = relatorioAluno.Filtrar((string)FiltroSelecaoCombo.SelectedValue, ComboValor.SelectedIndex);
                }

                if (resultado.Any())
                {
                    PopularDataGrid(resultado);
                }
            }

            FiltrarButton.IsEnabled = true;
        }

        private void Exportar()
        {
            //https://stackoverflow.com/questions/34704314/wpf-datagrid-to-csv-exporting-only-the-visible-values-in-the-grid

            ExportarCombo.IsEnabled = false;

            try
            {
                if (ExportarCombo.SelectedIndex == 1)
                {
                    DadosDataGrid.SelectAllCells();
                    DadosDataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

                    ApplicationCommands.Copy.Execute(null, DadosDataGrid);
                    DadosDataGrid.UnselectAllCells();

                    string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Arquivo CSV (*.csv)|*.csv";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        File.AppendAllText(saveFileDialog.FileName, result, UnicodeEncoding.UTF8);
                        MessageBox.Show($"Os dados foram exportados com sucesso", "Dados exportados", MessageBoxButton.OK, MessageBoxImage.Information);
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar exportar os dados.\n\n Erro: {ex.Message}", "Erro ao Exportar", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            ExportarCombo.IsEnabled = true;
            ExportarCombo.SelectedIndex = 0;
        }
    }
}
