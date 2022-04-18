using Promart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace Promart.Codes
{
    public class RelatorioVoluntario
    {
        IEnumerable<Voluntario> voluntarios;

        public List<string> TiposFiltro { get; } = new List<string>()
        {
             //Texto
            "Nome",
            "Idade",
            "Profissão",
            "Escolaridade",
            "Rua",
            "Bairro",
            //Combobox
            "Sexo",
        };

        public RelatorioVoluntario(IEnumerable<Voluntario> voluntarios)
        {
            this.voluntarios = voluntarios;
        }

        public IEnumerable<Voluntario> Filtrar(string nomeFiltro, string valor)
        {
            switch (nomeFiltro)
            {
                case "Nome":
                    return voluntarios.Where(a => a.NomeCompleto != null
                        && a.NomeCompleto.Contains(valor));
                case "Profissão":
                    return voluntarios.Where(a => a.Profissao != null
                        && a.Profissao.Contains(valor));
                case "Escolaridade":
                    return voluntarios.Where(a => a.Escolaridade != null
                     && a.Escolaridade.Contains(valor));
                case "Rua":
                    return voluntarios.Where(a => a.EnderecoRua != null
                    && a.EnderecoRua.Contains(valor));
                case "Bairro":
                    return voluntarios.Where(a => a.EnderecoBairro != null
                    && a.EnderecoBairro.Contains(valor));
                case "Idade":
                    int idade;
                    if (int.TryParse(valor, out idade))
                    {
                        return voluntarios.Where(a => a.IdadeValue == idade);
                    }
                    break;
            }

            return new List<Voluntario>();
        }

        public IEnumerable<Voluntario> Filtrar(string nomeFiltro, int valor)
        {
            switch (nomeFiltro)
            {
                case "Sexo":
                    return voluntarios.Where(a => a.Sexo == valor);                
                default:
                    return new List<Voluntario>();
            }
        }

        public FiltroControleType VerificarTipoFiltro(string nomeFiltro)
        {
            switch (nomeFiltro)
            {
                case "Nome":
                case "Idade":
                case "Profissão":
                case "Escolaridade":
                case "Rua":
                case "Bairro":
                    return FiltroControleType.Texto;

                case "Sexo":
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
                Header = "Profissão",
                Binding = new Binding("Profissao")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Escolaridade",
                Binding = new Binding("Escolaridade")
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
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Email",
                Binding = new Binding("Email")
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
        }
    }
}
