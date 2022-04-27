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
using Promart.Models;
using Promart.Data;
using Promart.Windows;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for OficinaInfoPage.xaml
    /// </summary>
    public partial class OficinaInfoPage : Page
    {
        Oficina oficina { get; set; }
        List<Voluntario> listaVoluntariosAtual = new List<Voluntario>();

        public OficinaInfoPage(Oficina oficina)
        {
            InitializeComponent();

            this.oficina = oficina;
            this.Loaded += async (object sender, RoutedEventArgs e) => await CarregarDados();
            AddVoluntarioButton.Click += async (object sender, RoutedEventArgs e) => await AdicionarVoluntario();
        }

        private void AddVoluntarioButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task CarregarDados()
        {
            NomeText.Text = oficina.Nome;
            await CarregarVoluntarios();
            await CarregarAlunos();
        }

        private async Task CarregarVoluntarios()
        {
            var voluntarios = await SqlAccess.GetAllAsync<Voluntario>();
            var result = await SqlAccess.GetAllAsync<VoluntarioOficina, Oficina>(oficina, nameof(oficina.Id), "IdOficina");
            
            if(voluntarios != null && result != null)
            {
                listaVoluntariosAtual.Clear();
                
                foreach (var item in result)
                {
                    listaVoluntariosAtual.Add(voluntarios.First(v => v.Id == item.IdVoluntario));
                    VoluntariosList.ItemsSource = null;
                    VoluntariosList.ItemsSource = listaVoluntariosAtual;
                }                
            }
        }

        private async Task CarregarAlunos()
        {
            var alunos = await SqlAccess.GetAllAsync<Aluno>();
            var result = await SqlAccess.GetAllAsync<AlunoOficina, Oficina>(oficina, nameof(oficina.Id), "IdOficina");

            if (alunos != null && result != null)
            {
                AlunosList.Items.Clear();
                foreach (var item in result)
                {
                    AlunosList.Items.Add(alunos.First(a => a.Id == item.IdAluno));
                }
            }
        }

        private async Task AdicionarVoluntario()
        {
            AdicionarRelacionadoWindow relacionadoWindow = new AdicionarRelacionadoWindow(listaVoluntariosAtual);
            
            if(relacionadoWindow.ShowDialog() == true)
            {
                var escolhidos = relacionadoWindow.VoluntariosSelecionados;

                if (escolhidos.Any())
                {
                    foreach(var item in escolhidos)
                    {
                        await SqlAccess.InsertAsync(new VoluntarioOficina() { IdVoluntario = item.Id, IdOficina = oficina.Id });
                    }
                }
            }

            await CarregarDados();
        }
    }    
}
