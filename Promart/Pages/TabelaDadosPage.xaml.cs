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
using Promart.Codes;
using Promart.Data;
using Promart.Windows;
using Promart.Models;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for TabelaDadosPage.xaml
    /// </summary>
    public partial class TabelaDadosPage : Page
    {
        public TabelaDadosPage()
        {
            InitializeComponent();
            SelecionarButton.Click += async (object sender, RoutedEventArgs e) =>
            {
                SelecionarButton.IsEnabled = false;

                switch (TipoTabelaCombo.SelectedIndex)
                {
                    case 0:                        
                        var alunos = await SqlAccess.GetDadosAsync<Aluno>();                        
                        DadosDataGrid.ItemsSource = null;
                        DadosDataGrid.ItemsSource = alunos;                        
                        break;
                    case 1:
                        var voluntarios = await SqlAccess.GetDadosAsync<Voluntario>();
                        DadosDataGrid.ItemsSource = null;
                        DadosDataGrid.ItemsSource = voluntarios;
                        break;
                }

                SelecionarButton.IsEnabled = true;
            };
        }
    }
}
