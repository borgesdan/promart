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
        Oficina oficina;
        readonly List<Voluntario> listaVoluntariosAtual = new();
        readonly List<Aluno> listaAlunosAtual = new();

        public OficinaInfoPage(Oficina oficina)
        {
            InitializeComponent();

            this.oficina = oficina;
            this.Loaded += async (object sender, RoutedEventArgs e) => await CarregarDados();
            AddVoluntarioButton.Click += async (object sender, RoutedEventArgs e) => await AdicionarVoluntario();
            AddAlunoButton.Click += async (object sender, RoutedEventArgs e) => await AdicionarAluno();
            RemoverVoluntarioButton.Click += async (object sender, RoutedEventArgs e) => await RemoverVoluntario();
            RemoverAlunoButton.Click += async (object sender, RoutedEventArgs e) => await RemoverAluno();
            AlunosList.SelectionChanged += (object sender, SelectionChangedEventArgs e) => HabilitarRemoverAlunoButton();
            VoluntariosList.SelectionChanged += (object sender, SelectionChangedEventArgs e) => HabilitarRemoverVoluntarioButton();
            AtualizarNomeButton.Click += async (object sender, RoutedEventArgs e) => await Renomear();
            NomeText.TextChanged += (object sender, TextChangedEventArgs e) => HabilitarRenomear();
            FecharButton.Click += (object sender, RoutedEventArgs e) => MainWindow.Instance?.CloseCurrentTab();
        }

        private void HabilitarRemoverAlunoButton()
        {
            RemoverAlunoButton.IsEnabled = AlunosList.SelectedItem != null;
        }
        
        private void HabilitarRemoverVoluntarioButton()
        {
            RemoverVoluntarioButton.IsEnabled = VoluntariosList.SelectedItem != null;
        }

        private void HabilitarRenomear()
        {
            string nome = NomeText.Text.Trim();
            AtualizarNomeButton.IsEnabled = !string.IsNullOrWhiteSpace(nome) && nome != oficina.Nome;
        }

        private async Task Renomear()
        {
            string nome = NomeText.Text.Trim();

            if(!string.IsNullOrWhiteSpace(nome) && nome != oficina.Nome)
            {
                var result = MessageBox.Show($"Deseja realmente renomar a oficina {oficina.Nome?.ToUpper()} para {nome.ToUpper()} ?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;

                oficina.Nome = nome;
                await SqlAccess.UpdateAsync(oficina);
                await CarregarDados();
            }
        }

        private async Task CarregarDados()
        {
            NomeText.Text = oficina.Nome;
            await CarregarVoluntarios();
            await CarregarAlunos();
        }

        private async Task CarregarVoluntarios()
        {
            listaVoluntariosAtual.Clear();
            VoluntariosList.ItemsSource = null;

            var voluntarios = await SqlAccess.GetAllAsync<Voluntario>();
            var result = await SqlAccess.GetAllAsync<VoluntarioOficina, Oficina>(oficina, nameof(oficina.Id), "IdOficina");            

            if (voluntarios != null && result != null)
            {   
                foreach (var item in result)
                {
                    listaVoluntariosAtual.Add(voluntarios.First(v => v.Id == item.IdVoluntario));                                        
                }
                
                VoluntariosList.ItemsSource = listaVoluntariosAtual;
            }
        }

        private async Task CarregarAlunos()
        {
            listaAlunosAtual.Clear();
            AlunosList.ItemsSource = null;

            var alunos = await SqlAccess.GetAllAsync<Aluno>();
            var result = await SqlAccess.GetAllAsync<AlunoOficina, Oficina>(oficina, nameof(oficina.Id), "IdOficina");

            if (alunos != null && result != null)
            {
                foreach (var item in result)
                {
                    listaAlunosAtual.Add(alunos.First(a => a.Id == item.IdAluno));                    
                }
                
                AlunosList.ItemsSource = listaAlunosAtual;
            }
        }

        private async Task AdicionarVoluntario()
        {
            AdicionarRelacionadoWindow relacionadoWindow = new(listaVoluntariosAtual);
            
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

        private async Task AdicionarAluno()
        {
            AdicionarRelacionadoWindow relacionadoWindow = new(listaAlunosAtual);

            if(relacionadoWindow.ShowDialog() == true)
            {
                var escolhidos = relacionadoWindow.AlunosSelecionados;

                if(escolhidos.Any())
                {
                    foreach(var item in escolhidos)
                    {
                        await SqlAccess.InsertAsync(new AlunoOficina() { IdAluno = item.Id, IdOficina = oficina.Id });
                    }
                }
            }

            await CarregarDados();
        }

        private async Task RemoverVoluntario()
        {
            if(VoluntariosList.SelectedItem != null)
            {
                Voluntario voluntario = (Voluntario)VoluntariosList.SelectedItem;
                string nome = voluntario.NomeCompleto ?? string.Empty;
                var result = MessageBox.Show($"Deseja realmente remover o voluntário {nome.ToUpper()} da oficina?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if(result == MessageBoxResult.Yes)
                {                    
                    await SqlAccess.DeleteAsync(new VoluntarioOficina() { IdVoluntario = voluntario.Id, IdOficina = oficina.Id });
                    await CarregarDados();
                }
            }
        }

        private async Task RemoverAluno()
        {
            if (AlunosList.SelectedItem != null)
            {
                Aluno aluno = (Aluno)AlunosList.SelectedItem;
                string nome = aluno.NomeCompleto ?? string.Empty;
                var result = MessageBox.Show($"Deseja realmente remover o aluno {nome.ToUpper()} da oficina?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    await SqlAccess.DeleteAsync(new AlunoOficina() { IdAluno = aluno.Id, IdOficina = oficina.Id });
                    await CarregarDados();
                }
            }
        }
    }    
}
