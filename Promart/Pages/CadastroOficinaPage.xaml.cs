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
using Promart.Windows;

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
            AdicionarButton.Click += async (object sender, RoutedEventArgs e) => await Adicionar();
            DadosButton.Click += (object sender, RoutedEventArgs e) => AbrirDados();
            RemoverButton.Click += async (object sender, RoutedEventArgs e) => await Remover();
            OficinasList.MouseDoubleClick += (object sender, MouseButtonEventArgs e) => AbrirDados();
        }

        private async Task Carregar()
        {
            var result = await SqlAccess.GetAllAsync<Oficina>();

            if(result != null)
            {
                OficinasList.Items.Clear();

                foreach(var item in result.Reverse())
                {
                    OficinasList.Items.Add(item);
                }
            }
        }        

        private async Task Adicionar()
        {
            AdicionarButton.IsEnabled = false;

            if (!string.IsNullOrWhiteSpace(NomeText.Text))
            {
                Oficina oficina = new();
                oficina.Nome = NomeText.Text;

                var result = await SqlAccess.InsertAsync(oficina);

                if(result != -1)
                {
                    await Carregar();
                    NomeText.Text = string.Empty;
                }
            }

            AdicionarButton.IsEnabled = true;
        }

        private async Task Remover()
        {
            var result = MessageBox.Show("Deseja realmente excluir essa oficina? Essa operação não pode ser desfeita.", "Excluir Oficina", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            if(OficinasList.SelectedIndex != -1)
            {
                Oficina oficina = (Oficina)OficinasList.SelectedValue;
                
                await SqlAccess.DeleteAllAsync<AlunoOficina, Oficina>(oficina, nameof(Oficina.Id), "IdOficina");
                await SqlAccess.DeleteAllAsync<VoluntarioOficina, Oficina>(oficina, nameof(Oficina.Id), "IdOficina");
                await SqlAccess.DeleteAsync(oficina);
                await Carregar();
            }
        }

        private async Task ModificarOficina()
        {
            AdicionarButton.IsEnabled = false;
            OficinasList.IsEnabled = false;

            if (OficinasList.SelectedIndex != -1)
            {
                NovoCadastroWindow novo = new NovoCadastroWindow("Renomear Oficina", "Digite o novo nome da Oficina");                
                var result = novo.ShowDialog();

                if(result == true) 
                {
                    Oficina oficina = (Oficina)OficinasList.SelectedValue;
                    oficina.Nome = novo.NomeRecebido;
                    await SqlAccess.UpdateAsync(oficina);
                    await Carregar();
                }
            }

            OficinasList.IsEnabled = true;
            AdicionarButton.IsEnabled = true;
        }

        private void AbrirDados()
        {
            if(OficinasList.SelectedIndex != -1)
            {
                Oficina oficina = (Oficina)OficinasList.SelectedItem;
                MainWindow.Instance?.AbrirNovaAba(oficina.Nome ?? "Dados da Oficina", new OficinaInfoPage(oficina));
            }
        }
    }
}
