﻿using System;
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
        string? nomeAlunoPesquisa;

        RelatorioAluno relatorioAluno = new RelatorioAluno(new List<Aluno>());
        RelatorioVoluntario relatorioVoluntario = new RelatorioVoluntario(new List<Voluntario>());

        public TabelaDadosPage() : this(null) { }

        public TabelaDadosPage(string? nomeAluno)
        {
            InitializeComponent();
            DesabilitarControles();
            this.nomeAlunoPesquisa = nomeAluno;

            FiltrarButton.Click += (object sender, RoutedEventArgs e) => Filtrar();
            ExportarCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => Exportar();
            SelecionarButton.Click += async (object sender, RoutedEventArgs e) => await DefinirEstadoRelatorio();
            FiltroSelecaoCombo.SelectionChanged += (object sender, SelectionChangedEventArgs e) => DefinirControleValor();
            DadosDataGrid.PreviewKeyDown += (object sender, KeyEventArgs e) => RemoverDados(e);
            DadosDataGrid.PreviewKeyDown += (object sender, KeyEventArgs e) => AbrirItem(e, null);
            DadosDataGrid.PreviewMouseDoubleClick += (object sender, MouseButtonEventArgs e) => AbrirItem(null, e);            
        }

        private async void AbrirItem(KeyEventArgs? ek, MouseButtonEventArgs? em)
        {
            if((ek != null && ek.Key == Key.Enter) || (em != null && em.LeftButton == MouseButtonState.Pressed))
            {                
                switch(selectedIndex)
                {
                    case 0:
                        var alunoSelecionado = (Aluno)DadosDataGrid.SelectedItem;
                        var alunoAtualizado = await SqlAccess.GetDadoAsync<Aluno>(alunoSelecionado.Id);

                        if (alunoAtualizado != null)
                        {
                            MainWindow.Instance?.AbrirNovaAba(alunoAtualizado.NomeCompleto ?? "Aluno Selecionado", new CadastroAlunoPage(alunoAtualizado));
                        }
                        break;
                    case 1:
                        var voluntarioSelecionado = (Voluntario)DadosDataGrid.SelectedItem;
                        var voluntarioAtualizado = await SqlAccess.GetDadoAsync<Voluntario>(voluntarioSelecionado.Id);

                        if (voluntarioAtualizado != null)
                        {
                            MainWindow.Instance?.AbrirNovaAba(voluntarioAtualizado.NomeCompleto ?? "Aluno Selecionado", new CadastroVoluntarioPage(voluntarioAtualizado));
                        }
                        break;
                }
            }            
        }

        private void RemoverDados(KeyEventArgs e)
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
            selectedIndex = TipoTabelaCombo.SelectedIndex;

            switch (selectedIndex)
            {
                case 0:                    
                    var alunos = await SqlAccess.GetDadosAsync<Aluno>();

                    if (alunos != null)
                    {
                        relatorioAluno = new RelatorioAluno(alunos);
                        DefinirRelatorio(alunos);                        
                        HabilitarControles();
                    }
                    break;
                case 1:
                    var voluntarios = await SqlAccess.GetDadosAsync<Voluntario>();

                    if (voluntarios != null)
                    {
                        relatorioVoluntario = new RelatorioVoluntario(voluntarios);
                        DefinirRelatorio(voluntarios);                        
                        HabilitarControles();
                    }
                    break;
            }

            SelecionarButton.IsEnabled = true;
            SelecionarButton.IsEnabled = true;
        }

        private void DefinirRelatorio(IEnumerable<Aluno> alunos)
        {
            PopularDataGrid(alunos);
            FiltroSelecaoCombo.ItemsSource = relatorioAluno.TiposFiltro;
            FiltroSelecaoCombo.SelectedIndex = 0;
        }

        private void DefinirRelatorio(IEnumerable<Voluntario> voluntarios)
        {
            PopularDataGrid(voluntarios);
            FiltroSelecaoCombo.ItemsSource = relatorioVoluntario.TiposFiltro;
            FiltroSelecaoCombo.SelectedIndex = 0;
        }

        private void PopularDataGrid(IEnumerable<Aluno> alunos)
        {            
            DadosDataGrid.Items.Clear();
            foreach(var a in alunos)
            {
                DadosDataGrid.Items.Add(a);
            }
            relatorioAluno.PopularColunasDataGrid(DadosDataGrid);
        }

        private void PopularDataGrid(IEnumerable<Voluntario> voluntarios)
        {
            DadosDataGrid.Items.Clear();
            foreach (var v in voluntarios)
            {
                DadosDataGrid.Items.Add(v);
            }

            relatorioVoluntario.PopularColunasDataGrid(DadosDataGrid);
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
                        PopularComboValor(nomeFiltro);
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

        private void PopularComboValor(string nomeFiltro)
        {
            switch (selectedIndex)
            {
                case 0:
                    relatorioAluno.PopularComboValor(nomeFiltro, ComboValor);
                    break;
                case 1:
                    relatorioAluno.PopularComboValor(nomeFiltro, ComboValor);
                    break;
            }
        }

        private void Filtrar()
        {
            FiltrarButton.IsEnabled = false;

            switch (selectedIndex)
            {
                case 0:
                    IEnumerable<Aluno> resultadoAluno = new List<Aluno>();

                    if (TextoValor.Visibility == Visibility.Visible)
                    {
                        resultadoAluno = relatorioAluno.Filtrar((string)FiltroSelecaoCombo.SelectedValue, TextoValor.Text.Trim());
                    }
                    else if (ComboValor.Visibility == Visibility.Visible)
                    {
                        resultadoAluno = relatorioAluno.Filtrar((string)FiltroSelecaoCombo.SelectedValue, ComboValor.SelectedIndex);
                    }

                    PopularDataGrid(resultadoAluno);
                    break;
                case 1:
                    IEnumerable<Voluntario> resultadoVoluntario = new List<Voluntario>();

                    if (TextoValor.Visibility == Visibility.Visible)
                    {
                        resultadoVoluntario = relatorioVoluntario.Filtrar((string)FiltroSelecaoCombo.SelectedValue, TextoValor.Text.Trim());
                    }
                    else if (ComboValor.Visibility == Visibility.Visible)
                    {
                        resultadoVoluntario = relatorioVoluntario.Filtrar((string)FiltroSelecaoCombo.SelectedValue, ComboValor.SelectedIndex);
                    }

                    PopularDataGrid(resultadoVoluntario);
                    break;
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(nomeAlunoPesquisa != null)
            {
                selectedIndex = 0;
                await DefinirEstadoRelatorio();
                FiltroSelecaoCombo.SelectedIndex = 0;
                TextoValor.Text = nomeAlunoPesquisa;
                Filtrar();
            }
        }
    }
}
