using Promart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Promart.Codes
{
    public class RelatorioAluno
    {
        IEnumerable<Aluno> alunos;

        public List<string> TiposFiltro { get; } = new List<string>()
        {
            //Texto
            "Nome",
            "Idade",
            "Responsável",
            "Escola",
            "Rua",
            "Bairro",
            
            //Combobox
            "Sexo",
            "Vínculo",
            "Beneficiário",
            "Renda",
            "Ano Escolar",
            "Turno Escolar",
            "Situação Projeto",
            "Turno Projeto",
            "Moradia",
        };

        public RelatorioAluno(IEnumerable<Aluno> alunos)
        {
            this.alunos = alunos;
        }

        public IEnumerable<Aluno> Filtrar(string nomeFiltro, string valor)
        {
            switch (nomeFiltro)
            {
                case "Nome":
                    return alunos.Where(a => a.NomeCompleto != null
                        && a.NomeCompleto.Contains(valor));                
                case "Responsável":
                    return alunos.Where(a => a.NomeResponsavel != null
                        && a.NomeResponsavel.Contains(valor));
                case "Escola":
                    return alunos.Where(a => a.EscolaNome != null
                     && a.EscolaNome.Contains(valor));
                case "Rua":
                    return alunos.Where(a => a.EnderecoRua != null
                    && a.EnderecoRua.Contains(valor));
                case "Bairro":
                    return alunos.Where(a => a.EnderecoBairro != null
                    && a.EnderecoBairro.Contains(valor));
                case "Idade":
                    int idade;
                    if (int.TryParse(valor, out idade))
                    {
                        return alunos.Where(a => a.IdadeValue == idade);
                    }
                    break;
            }
            
            return new List<Aluno>();
        }

        public IEnumerable<Aluno> Filtrar(string nomeFiltro, int valor)
        {
            switch (nomeFiltro)
            {
                case "Sexo":
                    return alunos.Where(a => a.Sexo == valor);
                case "Vínculo":
                    return alunos.Where(a => a.VinculoFamiliar == valor);
                case "Beneficiário":
                    return alunos.Where(a => a.IsBeneficiario == (valor == 0));
                case "Renda":
                    return alunos.Where(a => a.Renda == valor);
                case "Ano Escolar":
                    return alunos.Where(a => a.AnoEscolar == valor);
                case "Turno Escolar":
                    return alunos.Where(a => a.TurnoEscolar == valor);
                case "Situação Projeto":
                    return alunos.Where(a => a.SituacaoProjeto == valor);
                case "Turno no Projeto":
                    return alunos.Where(a => a.TurnoProjeto == valor);
                case "Moradia":
                    return alunos.Where(a => a.TipoMoradia == valor);                
                default:
                    return new List<Aluno>();
            }
        }

        public FiltroControleType VerificarTipoFiltro(string nomeFiltro)
        {
            switch (nomeFiltro)
            {
                case "Nome":
                case "Idade":
                case "Responsável":
                case "Escola":
                case "Rua":
                case "Bairro":
                    return FiltroControleType.Texto;

                case "Sexo":
                case "Vínculo":                
                case "Beneficiário":
                case "Renda":
                case "Ano Escolar":
                case "Turno Escolar":
                case "Situação Projeto":
                case "Turno Projeto":
                case "Moradia":
                    return FiltroControleType.ComboBox;

                default:
                    return FiltroControleType.Texto;
            }
        }

        public void PopularComboValor(string nomeFiltro, ComboBox comboValor)
        {
            switch (nomeFiltro)
            {
                case "Sexo":
                    comboValor.ItemsSource = ComboBoxTipos.TipoSexoNaoNumerado;
                    break;
                case "Vínculo":
                    comboValor.ItemsSource = ComboBoxTipos.TipoVinculoFamiliarNaoNumerado;
                    break;
                case "Beneficiário":
                    comboValor.ItemsSource = ComboBoxTipos.TipoBeneficiarioNaoNumerado;
                    break;
                case "Renda":
                    comboValor.ItemsSource = ComboBoxTipos.TipoRendaNaoNumerado;
                    break;
                case "Ano Escolar":
                    comboValor.ItemsSource = ComboBoxTipos.TipoAnoEscolarNaoNumerado;
                    break;
                case "Turno Escolar":
                    comboValor.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNaoNumerado;
                    break;
                case "Situação Projeto":
                    comboValor.ItemsSource = ComboBoxTipos.TipoAlunoSituacaoNaoNumerado;
                    break;
                case "Turno Projeto":
                    comboValor.ItemsSource = ComboBoxTipos.TipoTurnoEscolarNaoNumerado;
                    break;
                case "Moradia":
                    comboValor.ItemsSource = ComboBoxTipos.TipoMoradiaNaoNumerado;
                    break;
            }
        }
        
        public void PopularColunasDataGrid(DataGrid dataGrid)
        {
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Nome",
                Binding = new Binding("NomeCompleto")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Data de Nascimento",
                Binding = new Binding("DataNascimentoValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Idade",
                Binding = new Binding("IdadeValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Sexo",
                Binding = new Binding("SexoValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "RG",
                Binding = new Binding("RG")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "CPF",
                Binding = new Binding("CPF")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Certidão",
                Binding = new Binding("Certidao")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Nome do Responsável",
                Binding = new Binding("NomeResponsavel")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Vínculo Familiar",
                Binding = new Binding("VinculoFamiliarValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Contato 1",
                Binding = new Binding("Contato1")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Contato 2",
                Binding = new Binding("Contato2")
            });
            dataGrid.Columns.Add(new DataGridCheckBoxColumn()
            {
                Header = "Beneficiário",
                Binding = new Binding("IsBeneficiario")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Tipo de Moradia",
                Binding = new Binding("TipoCasaValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Renda",
                Binding = new Binding("RendaValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Escola",
                Binding = new Binding("EscolaNome")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ano Escolar",
                Binding = new Binding("AnoEscolarValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Turno Escolar",
                Binding = new Binding("TurnoEscolar")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Rua",
                Binding = new Binding("EnderecoRua")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Bairro",
                Binding = new Binding("EnderecoBairro")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Número",
                Binding = new Binding("EnderecoNumero")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Complemento",
                Binding = new Binding("EnderecoComplemento")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Cidade",
                Binding = new Binding("EnderecoCidade")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Estado",
                Binding = new Binding("EnderecoEstado")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "CEP",
                Binding = new Binding("EnderecoCEP")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Matrícula",
                Binding = new Binding("Matricula")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Data Matrícula",
                Binding = new Binding("DataMatriculaValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Situação do Aluno",
                Binding = new Binding("SituacaoProjetoValue")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Turno no Projeto",
                Binding = new Binding("TurnoProjeto")
            });
        }
    }
}
