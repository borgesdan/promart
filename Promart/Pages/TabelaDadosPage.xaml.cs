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
using Promart.Codes;
using Promart.Data;
using Promart.Windows;
using Promart.Models;
using Promart.Codes;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for TabelaDadosPage.xaml
    /// </summary>
    public partial class TabelaDadosPage : Page
    {
        int selectedIndex = -1;
        IEnumerable<Aluno>? alunos;
        IEnumerable<Voluntario>? voluntarios;

        public TabelaDadosPage()
        {
            InitializeComponent();

            SelecionarButton.Click += async (object sender, RoutedEventArgs e) =>
            {
                SelecionarButton.IsEnabled = false;

                switch (TipoTabelaCombo.SelectedIndex)
                {
                    case 0:
                        selectedIndex = TipoTabelaCombo.SelectedIndex;
                        PopularSelecaoComboPorAluno();
                        alunos = await SqlAccess.GetDadosAsync<Aluno>();

                        if (alunos != null)
                        {
                            PopularDataGrid(alunos);
                        }

                        break;
                    case 1:
                        //var voluntarios = await SqlAccess.GetDadosAsync<Voluntario>();

                        //DadosDataGrid.ItemsSource = null;
                        //DadosDataGrid.ItemsSource = voluntarios;
                        //colCount = DadosDataGrid.Columns.Count;
                        //DadosDataGrid.Columns.RemoveAt(colCount - 1); //FotoUrl
                        break;
                }

                FiltroSelecaoCombo.SelectedIndex = 0;
                SelecionarButton.IsEnabled = true;
            };

            FiltrarButton.Click += FiltrarButton_Click;
            FiltroSelecaoCombo.SelectionChanged += FiltroSelecaoCombo_SelectionChanged;
        }

        private void FiltroSelecaoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (FiltroSelecaoCombo.SelectedValue)
            {
                case "Nome":
                case "Idade":
                case "Responsável":
                case "Escola":
                case "Rua":
                case "Bairro":
                    AlterarVisibilidadeFiltroValor(TextoValor);
                    break;

                case "Nascimento":
                    AlterarVisibilidadeFiltroValor(DataValor);
                    break;

                case "Sexo":
                case "Vínculo":
                case "Beneficiário":
                case "Moradia":
                case "Renda":
                case "Ano Escolar":
                case "Turno Escolar":
                case "Situação":
                case "Turno no Projeto":
                    AlterarVisibilidadeFiltroValor(ComboValor);
                    PopularComboValor(FiltroSelecaoCombo.SelectedValue);
                    break;
            }
        }

        void PopularComboValor(object caso)
        {
            switch(caso)
            {
                case "Sexo":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoSexoNaoNumerado;
                    break;
                case "Vínculo":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoVinculoFamiliarNaoNumerado;
                    break;
                case "Beneficiário":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoBeneficiarioNaoNumerado;
                    break;
                case "Moradia":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoMoradiaNaoNumerado;
                    break;
                case "Renda":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoRendaNaoNumerado;
                    break;
                case "Ano Escolar":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoAnoEscolarNaoNumerado;
                    break;
                case "Turno Escolar":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNaoNumerado;
                    break;
                case "Situação":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoAlunoSituacaoNaoNumerado;
                    break;
                case "Turno no Projeto":
                    ComboValor.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNaoNumerado;
                    break;
            }
        }

        void AlterarVisibilidadeFiltroValor(Control controle)
        {
            ComboValor.Visibility = Visibility.Collapsed;
            DataValor.Visibility = Visibility.Collapsed;
            TextoValor.Visibility = Visibility.Collapsed;

            controle.Visibility = Visibility.Visible;
        }

        void PopularSelecaoComboPorAluno()
        {
            FiltroSelecaoCombo.ItemsSource = new List<string>
            {
                "Nome",
                "Nascimento",
                "Idade",
                "Sexo",
                "Responsável",
                "Vínculo",
                "Beneficiário",
                "Moradia",
                "Renda",
                "Escola",
                "Ano Escolar",
                "Turno Escolar",
                "Rua",
                "Bairro",
                "Situação",
                "Turno no Projeto",
            };
        }

        private void FiltrarButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedIndex == 0 && alunos != null)
            {
                IEnumerable<Aluno> resultado = new List<Aluno>();

                if (FiltroSelecaoCombo.SelectedValue.Equals("Nome"))
                {
                    resultado = alunos.Where(a => a.NomeCompleto != null
                        && a.NomeCompleto.Contains(TextoValor.Text)
                    );
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Idade"))
                {
                    int idade;

                    if (int.TryParse(TextoValor.Text, out idade))
                    {
                        resultado = alunos.Where(a => a.IdadeValue == idade);
                    }
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Responsável"))
                {
                    resultado = alunos.Where(a => a.NomeResponsavel != null
                        && a.NomeResponsavel.Contains(TextoValor.Text)
                    );
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Escola"))
                {
                    resultado = alunos.Where(a => a.EscolaNome != null
                        && a.EscolaNome.Contains(TextoValor.Text)
                    );
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Rua"))
                {
                    resultado = alunos.Where(a => a.EnderecoRua != null
                        && a.EnderecoRua.Contains(TextoValor.Text)
                    );
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Bairro"))
                {
                    resultado = alunos.Where(a => a.EnderecoRua != null
                        && a.EnderecoRua.Contains(TextoValor.Text)
                    );
                }
                else if (FiltroSelecaoCombo.SelectedValue.Equals("Data"))
                {
                    if (DataValor.SelectedDate != null)
                    {
                        resultado = alunos.Where(a => a.DataNascimento != null
                            && a.DataNascimento == DataValor.SelectedDate.Value
                        );
                    }
                }

                if (resultado.Any())
                {
                    PopularDataGrid(resultado);
                }
            }
        }

        private void PopularDataGrid(IEnumerable<Aluno> alunos)
        {
            DadosDataGrid.ItemsSource = alunos;
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Nome",
                Binding = new Binding("NomeCompleto")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Data de Nascimento",
                Binding = new Binding("DataNascimentoValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Idade",
                Binding = new Binding("IdadeValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Sexo",
                Binding = new Binding("SexoValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "RG",
                Binding = new Binding("RG")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "CPF",
                Binding = new Binding("CPF")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Certidão",
                Binding = new Binding("Certidao")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Nome do Responsável",
                Binding = new Binding("NomeResponsavel")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Vínculo Familiar",
                Binding = new Binding("VinculoFamiliarValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Contato 1",
                Binding = new Binding("Contato1")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Contato 2",
                Binding = new Binding("Contato2")
            });
            DadosDataGrid.Columns.Add(new DataGridCheckBoxColumn()
            {
                Header = "Beneficiário",
                Binding = new Binding("IsBeneficiario")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Tipo de Moradia",
                Binding = new Binding("TipoCasaValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Renda",
                Binding = new Binding("RendaValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Escola",
                Binding = new Binding("EscolaNome")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ano Escolar",
                Binding = new Binding("AnoEscolarValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Turno Escolar",
                Binding = new Binding("TurnoEscolar")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Rua",
                Binding = new Binding("EnderecoRua")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Bairro",
                Binding = new Binding("EnderecoBairro")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Número",
                Binding = new Binding("EnderecoNumero")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Complemento",
                Binding = new Binding("EnderecoComplemento")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Cidade",
                Binding = new Binding("EnderecoCidade")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Estado",
                Binding = new Binding("EnderecoEstado")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "CEP",
                Binding = new Binding("EnderecoCEP")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Situação do Aluno",
                Binding = new Binding("SituacaoProjetoValue")
            });
            DadosDataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Turno no Projeto",
                Binding = new Binding("TurnoProjeto")
            });
        }
    }
}
