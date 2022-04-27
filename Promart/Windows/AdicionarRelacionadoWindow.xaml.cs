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
using Promart.Codes;
using Promart.Models;
using Promart.Data;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for AdicionarRelacionadoWindow.xaml
    /// </summary>
    public partial class AdicionarRelacionadoWindow : Window
    {
        // 0 - alunos
        // 1 - voluntários
        byte modoRelacao = 0;
        List<Voluntario> voluntarioList = new List<Voluntario>();
        List<Aluno> alunoList = new List<Aluno>();
        List<Voluntario> exclusaoVoluntarioList = new List<Voluntario>();
        List<Aluno> exclusaoAlunoList = new List<Aluno>();

        public List<Voluntario> VoluntariosSelecionados { get; } = new List<Voluntario>();
        public List<Aluno> AlunosSelecionados { get; } = new List<Aluno>();

        public AdicionarRelacionadoWindow(List<Voluntario> exclusao)
        {
            InitializeComponent();
            exclusaoVoluntarioList = exclusao;
            modoRelacao = 1;
            InicializarEventos();
        }

        public AdicionarRelacionadoWindow(List<Aluno> exclusao)
        {
            InitializeComponent();
            exclusaoAlunoList = exclusao;
            modoRelacao = 0;
            InicializarEventos();
        }

        private void InicializarEventos()
        {
            this.Loaded += (object sender, RoutedEventArgs e) => CarregarDados();
            this.AddButton.Click += (object sender, RoutedEventArgs e) => FinalizarTarefa();
        }

        private void FinalizarTarefa()
        {
            if(modoRelacao == 1)
            {
                foreach(var item in RelacionadosList.SelectedItems)
                {
                    if(item != null)
                    {
                        VoluntariosSelecionados.Add((Voluntario)item);
                    }
                }

                DialogResult = true;
            }
        }

        private async void CarregarDados()
        {
            if(modoRelacao == 1)
            {
                var voluntarios = await SqlAccess.GetAllAsync<Voluntario>();

                if (voluntarios != null)
                {
                    var list = voluntarios.ToList();
                    list.RemoveAll(x => exclusaoVoluntarioList.Where(e => e.Id == x.Id).Any());

                    RelacionadosList.ItemsSource = list;
                }
            }            
        }
    }
}
