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
using System.Windows.Shapes;

namespace Promart.Windows
{
    public enum TipoFiltro
    {
        Aluno,
        Voluntario
    }

    /// <summary>
    /// Interaction logic for FiltroTabelaWindow.xaml
    /// </summary>
    public partial class FiltroTabelaWindow : Window
    {
        TipoFiltro tipoFiltro;

        public FiltroTabelaWindow(TipoFiltro tipo)
        {
            tipoFiltro = tipo;
            InitializeComponent();

            switch (tipo)
            {
                case TipoFiltro.Aluno:
                    TipoFiltroCombo.ItemsSource = new List<string>
                    {
                        "Nome", //Texto
                        "RG", //Texto
                        "CPF", //Texto
                        "Certidão", //Texto
                        "Nome da Escola", //Texto
                        "Ano Escolar", //Texto
                        "Rua", //Texto
                        "Bairro", //Texto
                        "Cidade", //Texto
                        "CEP", //Texto
                        "Observações", //Texto
                        "Nome do Responsável",//Texto
                        "Data de Nascimento", //Data Simples
                        "Período entre Datas", //Período
                        "Sexo", //Combobox
                        "Tipo de Vínculo Familiar", //Combobox
                        "Beneficiário", //Combobox
                        "Tipo de Moradia", //Combobox
                        "Tipo de Renda", //Combobox
                        "Turno Escolar", //Combobox
                        "Situação no Projeto", //Combobox
                        "Turno no Projeto", //Combobox
                    };
                    break;
                case TipoFiltro.Voluntario:
                    TipoFiltroCombo.ItemsSource = new List<string>
                    {
                        "Nome",
                        "Data de Nascimento",
                        "Período entre Datas",
                        "Sexo",
                        "Profissão",
                        "RG",
                        "CPF",
                        "Email",                        
                        "Rua",
                        "Bairro",
                        "Cidade",
                        "CEP",
                        "Situação no Projeto",
                        "Turno no Projeto",
                        "Observações",
                    };
                    break;
            }

            TipoFiltroCombo.SelectionChanged += TipoComboData_SelectionChanged;
        }

        private void TipoComboData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tipoFiltro)
            {
                case TipoFiltro.Aluno:
                    if(TipoFiltroCombo.SelectedIndex <= 11)
                    {
                        AtivarVisibilidade(ValorTexto);
                    }
                    else if (TipoFiltroCombo.SelectedIndex == 12)
                    {
                        AtivarVisibilidade(ValorData);
                    }
                    else if (TipoFiltroCombo.SelectedIndex == 13)
                    {
                        AtivarVisibilidade(ValorPeriodoData);
                    }
                    else
                    {
                        AtivarVisibilidade(ValorCombo);
                    }
                    break;
                case TipoFiltro.Voluntario:
                    break;
            }
        }

        void AtivarVisibilidade(Control controleParaAtivar)
        {
            ValorTexto.Visibility = Visibility.Collapsed;
            ValorData.Visibility = Visibility.Collapsed;
            ValorPeriodoData.Visibility = Visibility.Collapsed;
            ValorCombo.Visibility = Visibility.Collapsed;
            ValorPeriodoData.Visibility = Visibility.Collapsed;

            controleParaAtivar.Visibility = Visibility.Visible;
        }
    }
}
