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
            foreach (var item in RelacionadosDataGrid.SelectedItems)
            {
                if (item != null)
                {
                    if (modoRelacao == 0)
                        AlunosSelecionados.Add((Aluno)item);
                    else if (modoRelacao == 1)
                        VoluntariosSelecionados.Add((Voluntario)item);
                }
            }

            DialogResult = true;
        }

        private async void CarregarDados()
        {
            RelacionadosDataGrid.Items.Clear();
            if (modoRelacao == 0)
            {
                await CarregarAlunos();
            }          
            else if (modoRelacao == 1)
            {
               await CarregarVoluntarios();
            }
            
            if(RelacionadosDataGrid.Items.Count <= 0)
            {
                MessageBox.Show("Não há opções a serem selecionadas.", "Sobre", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = false;
            }
        }

        private async Task CarregarAlunos()
        {
            var alunos = await SqlAccess.GetAllAsync<Aluno>();

            if (alunos != null)
            {
                var list = alunos.ToList();
                list.RemoveAll(x => exclusaoAlunoList.Where(e => e.Id == x.Id).Any());

                RelatorioAluno relatorioAluno = new RelatorioAluno(alunos);
                relatorioAluno.PopularColunasDataGrid(RelacionadosDataGrid);

                foreach(var aluno in list)
                {
                    if(aluno.SituacaoProjeto == 3)
                    {
                        RelacionadosDataGrid.Items.Add(aluno);
                    }
                }                
            }
        }

        private async Task CarregarVoluntarios()
        {
            var voluntarios = await SqlAccess.GetAllAsync<Voluntario>();

            if (voluntarios != null)
            {
                var list = voluntarios.ToList();
                list.RemoveAll(x => exclusaoVoluntarioList.Where(e => e.Id == x.Id).Any());

                RelatorioVoluntario relatorioVoluntario = new RelatorioVoluntario(voluntarios);
                relatorioVoluntario.PopularColunasDataGrid(RelacionadosDataGrid);
                list.ForEach(i => RelacionadosDataGrid.Items.Add(i));
            }
        }
    }
}
