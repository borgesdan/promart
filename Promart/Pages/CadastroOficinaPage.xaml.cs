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
        public Oficina Oficina { get; private set; }

        public CadastroOficinaPage() : this(new Oficina())
        {            
        }

        public CadastroOficinaPage(Oficina oficina)
        {
            InitializeComponent();
            Oficina = oficina;

            //Helper.Controles.PopularOficinasListBox(OficinasList);

            TrocarVisibilidadeGrupo(AdicionarGrupo);

            AdicionarButton.Click += AdicionarButton_Click;
            ConfirmarAdicaoButton.Click += ConfirmarAdicaoButton_Click;
            OrdenarButton.Click += OrdenarButton_Click;
        }

        private void OrdenarButton_Click(object sender, RoutedEventArgs e)
        {  
        }

        private void ConfirmarAdicaoButton_Click(object sender, RoutedEventArgs e)
        {
            Oficina.Id = 0;
            Oficina.Nome = AddNomeText.Text;
            Oficina.Descricao = AddDescricaoText.Text;

            SqlAccess.Inserir(Oficina);
            MessageBox.Show($"A oficina '{Oficina.Nome}' foi adicionada com sucesso!", "Oficina Adicionada", MessageBoxButton.OK, MessageBoxImage.Information);
            //Helper.Controles.PopularOficinasListBox(OficinasList);                        

            AddNomeText.Text = string.Empty;
            AddDescricaoText.Text = string.Empty;
            AddNomeText.Focus();
        }

        private void AdicionarButton_Click(object sender, RoutedEventArgs e)
        {
            if(!AdicionarGrupo.IsEnabled)
            {
                TrocarVisibilidadeGrupo(AdicionarGrupo);
                AddNomeText.Focus();
            }
        }

        void TrocarVisibilidadeGrupo(GroupBox group)
        {
            group.IsEnabled = !group.IsEnabled;
            group.Visibility = group.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

        }
    }
}
