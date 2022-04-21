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
using Promart.Pages;
using Promart.Windows;
using Promart.Models;
using Promart.Codes;
using Promart.Data;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public static MainWindow? Instance { get; private set; }              

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            var page = new CadastroVoluntarioPage();

            FecharProgramaMenu.Click += (sender, e) => this.Close();

            CadastrarAlunoButton.Click += (object sender, RoutedEventArgs e) => CadastrarAluno();
            CadastrarAlunoMenu.Click += (object sender, RoutedEventArgs e) => CadastrarAluno();
            CadastrarVoluntarioBtn.Click += (object sender, RoutedEventArgs e) => CadastrarVoluntario();
            CadastrarVoluntarioMenu.Click += (object sender, RoutedEventArgs e) => CadastrarVoluntario();
            RelatoriosButton.Click += (object sender, RoutedEventArgs e) => AbrirRelatorios();
            BuscarAlunoButton.Click += (object sender, RoutedEventArgs e) => AbrirRelatoriosComNome();
            BackupButton.Click += async (object sender, RoutedEventArgs e) => await CriarBackup();
            RestaurarButton.Click += async (object sender, RoutedEventArgs e) => await RestaurarBackup();
            AbrirOficinasButton.Click += (object sender, RoutedEventArgs e) => AbrirOficinas();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //var result = MessageBox.Show("O programa será encerrado e toda alteração não salva será perdida. Deseja fechar o programa?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning) ;

            //if (result == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //}

            base.OnClosing(e);
        }

        private void CadastrarAluno()
        {
            CadastroAlunoPage page = new CadastroAlunoPage(new Aluno());
            page.Tab = AbrirNovaAba("Novo Aluno", page);
        }

        private void CadastrarVoluntario()
        {
            CadastroVoluntarioPage page = new CadastroVoluntarioPage(new Voluntario());
            page.Tab = AbrirNovaAba("Novo Voluntário", page);
        }

        private void AbrirOficinas()
        {
            CadastroOficinaPage page = new CadastroOficinaPage();
            AbrirNovaAba("Oficinas", page);
        }

        private void AbrirRelatorios()
        {
            TabelaDadosPage page = new();
            AbrirNovaAba("Relatórios", page);
        }

        private void AbrirRelatoriosComNome()
        {
            TabelaDadosPage page = new(NomeAlunoBusca.Text);
            AbrirNovaAba($"Relatórios: {NomeAlunoBusca.Text}", page);
        }

        public async Task CriarBackup()
        {
            if (!Directory.Exists(Helper.Diretorios.BACKUPS))
            {
                Directory.CreateDirectory(Helper.Diretorios.BACKUPS);
            }

            DateTime data = DateTime.Now;

            string ano = data.Year.ToString();
            string mes = data.Month < 10 ? $"0{data.Month}" : data.Month.ToString();
            string dia = data.Day < 10 ? $"0{data.Day}" : data.Day.ToString();
            string hora = data.Hour < 10 ? $"0{data.Hour}" : data.Hour.ToString();
            string minutos = data.Minute < 10 ? $"0{data.Minute}" : data.Minute.ToString();
            string final = $"{ano}-{mes}-{dia}-{hora}-{minutos}-promartbd";
            var retorno = await Backup.Criar($"{Helper.Diretorios.BACKUPS}\\{final}.bak");

            if (retorno)
            {
                MessageBox.Show("Backup Completo.");
            }
        }

        public async Task RestaurarBackup()
        {
            if (!Directory.Exists(Helper.Diretorios.BACKUPS))
            {
                Directory.CreateDirectory(Helper.Diretorios.BACKUPS);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Arquivos de backup (*.BAK)|*.bak";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;            
            var result = openFileDialog.ShowDialog();

            if(result == true)
            {
                string backup = openFileDialog.FileName;

                var retorno = await Backup.Restaurar($"{backup}");

                if (retorno)
                {
                    MessageBox.Show("A restauração foi feita com sucesso.");
                }
            }
        }

        public TabItem AbrirNovaAba(string nome, Page page)
        {
            return Helper.Controles.AbrirNovaAba(TabConteudo, nome, page);
        }

        public void FecharAbaAtual()
        {
            TabConteudo.Items.RemoveAt(TabConteudo.SelectedIndex);
        }
    }
}
