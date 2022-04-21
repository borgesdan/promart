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
using Promart.Codes;
using Promart.Data;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastroOficinaPage.xaml
    /// </summary>
    public partial class CadastroOficinaPage : Page
    {
        public CadastroOficinaPage()
        {
            InitializeComponent();
            this.Loaded += async (object sender, RoutedEventArgs e) => await Carregar();
            Adicionar.Click += async (object sender, RoutedEventArgs e) => await Add();
            OficinasList.PreviewKeyDown += async (object sender, KeyEventArgs e) => await DeletarOficina(e);
        }

        private async Task DeletarOficina(KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                await Remover();
            }
        }

        private async Task Carregar()
        {
            var result = await SqlAccess.GetDadosAsync<Oficina>();

            if(result != null)
            {
                OficinasList.Items.Clear();

                foreach(var item in result.Reverse())
                {
                    OficinasList.Items.Add(item);
                }
            }
        }

        private async Task Add()
        {
            Adicionar.IsEnabled = false;

            if (!string.IsNullOrWhiteSpace(Nome.Text))
            {
                
                Oficina oficina = new Oficina();
                oficina.Nome = Nome.Text;

                var result = await SqlAccess.InserirAsync(oficina);

                if(result != -1)
                {
                    await Carregar();
                    Nome.Text = string.Empty;
                }
            }

            Adicionar.IsEnabled = true;
        }

        private async Task Remover()
        {
            Adicionar.IsEnabled = false;
            OficinasList.IsEnabled = false;

            if(OficinasList.SelectedIndex != -1)
            {
                Oficina oficina = (Oficina)OficinasList.SelectedValue;
                await SqlAccess.TAlunoOficinas.RemoverAlunosDaOficina(oficina);
                await SqlAccess.TVoluntarioOficinas.RemoverVoluntariosDaOficina(oficina);
                await SqlAccess.DeletarAsync(oficina);
                await Carregar();
            }

            OficinasList.IsEnabled = true;
            Adicionar.IsEnabled = true;
        }

        private async Task EditarNome()
        {
            Adicionar.IsEnabled = false;
            OficinasList.IsEnabled = false;

            OficinasList.IsEnabled = true;
            Adicionar.IsEnabled = true;
        }
    }
}
