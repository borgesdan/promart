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
using System.Diagnostics;
using Promart.Controls;

namespace Promart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public static MainWindow? Instance { get; private set; }
        bool imgFundoCarregado = false;

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
            
            AbrirOficinasButton.Click += (object sender, RoutedEventArgs e) => AbrirOficinas();
            AbrirOficinasMenu.Click += (object sender, RoutedEventArgs e) => AbrirOficinas();

            RelatoriosButton.Click += (object sender, RoutedEventArgs e) => AbrirRelatorios();
            AbrirRelatoriosMenu.Click += (object sender, RoutedEventArgs e) => AbrirRelatorios();
            BuscarAlunoButton.Click += (object sender, RoutedEventArgs e) => AbrirRelatoriosComNome();
            
            BackupButton.Click += async (object sender, RoutedEventArgs e) => await CriarBackup();
            CriarBackupMenu.Click += async (object sender, RoutedEventArgs e) => await CriarBackup();
            RestaurarBackupMenu.Click += async (object sender, RoutedEventArgs e) => await RestaurarBackup();
            AbrirPastaBackupMenu.Click += (object sender, RoutedEventArgs e) => AbrirPastaBackups();

            DefinirFundoMenu.Click += (object sender, RoutedEventArgs e) => DefinirFundo();
            TabConteudo.SelectionChanged += TabConteudo_SelectionChanged;

            this.Loaded += MainWindow_Loaded;

            LogFile.WriteLine("programa iniciado");
            LogFile.WriteLine("programa iniciado 2"); 
            LogFile.WriteLine("programa iniciado 3"); 
        }

        private void TabConteudo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabConteudo.Visibility = TabConteudo.Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            RetanguloFundo.Visibility = TabConteudo.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarFundo();
        }

        private void DefinirFundo()
        {
            DefinirFundoWindow definirFundoWindow = new DefinirFundoWindow();
            definirFundoWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            if(definirFundoWindow.ShowDialog() == true)
            {
                try
                {
                    imgFundoCarregado = false;
                    FileInfo arquivo = new FileInfo(definirFundoWindow.Arquivo);
                    string extensao = Helper.Util.ObterExtensaoArquivo(arquivo.Name);

                    Guid guidName = Guid.NewGuid();
                    string nomeFinal = $"{Helper.Diretorios.SALVOS}\\{guidName}.{extensao}";

                    Helper.Diretorios.CriarPasta($"{Helper.Diretorios.SALVOS}");
                    File.Copy(arquivo.FullName, nomeFinal, true);

                    string txt = $"{guidName}.{extensao};{definirFundoWindow.OpacidadeValor};{definirFundoWindow.RedimensionamentoValor}";
                    File.WriteAllText($"{Helper.Diretorios.SALVOS}\\fundo.txt", txt);

                    CarregarFundo();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao tentar carregar a imagem. \n\n {ex.Message}", "Erro", MessageBoxButton.OK);
                    imgFundoCarregado = true;
                }                
            }
        }

        private void CarregarFundo()
        {
            if (!imgFundoCarregado)
            {
                imgFundoCarregado = true;

                try
                {
                    if (File.Exists($"{Helper.Diretorios.SALVOS}\\fundo.txt"))
                    {
                        string txt = File.ReadAllText($"{Helper.Diretorios.SALVOS}\\fundo.txt");
                        string[] valores = txt.Split(";");

                        if (valores.Length == 3)
                        {
                            if (!File.Exists($"{Helper.Diretorios.SALVOS}\\{valores[0]}"))
                                return;

                            BitmapImage bmpFundo = new BitmapImage();
                            bmpFundo.BeginInit();
                            bmpFundo.UriSource = new Uri($"{Helper.Diretorios.SALVOS}\\{valores[0]}");
                            bmpFundo.CacheOption = BitmapCacheOption.OnLoad;
                            bmpFundo.EndInit();

                            ImagemFundo.ImageSource = bmpFundo;

                            double imgOpacidade;
                            double.TryParse(valores[1], out imgOpacidade);
                            ImagemFundo.Opacity = imgOpacidade;

                            int imgStretch;
                            int.TryParse(valores[2], out imgStretch);
                            ImagemFundo.Stretch = (Stretch)imgStretch;
                        }
                        else
                        {
                            File.Delete($"{Helper.Diretorios.SALVOS}\\fundo.txt");
                        }
                    }
                }
                catch
                {
                    File.Delete($"{Helper.Diretorios.SALVOS}\\fundo.txt");
                }            
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var result = MessageBox.Show("Deseja fechar o programa?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

            base.OnClosing(e);
        }

        private void CadastrarAluno()
        {
            CadastroAlunoPage page = new();
            AbrirNovaAba(page);
        }

        private void CadastrarVoluntario()
        {
            CadastroVoluntarioPage page = new();
            AbrirNovaAba("Novo Voluntário", page);
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
            string segundos = data.Second < 10 ? $"0{data.Second}" : data.Second.ToString();
            string final = $"{ano}-{mes}-{dia}-{hora}{minutos}{segundos}-promartbd";
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
                var dialog = MessageBox.Show("Deseja realmente restaurar o banco de dados para o arquivo selecionado? Essa ação não pode ser desfeita.", "Restaurar", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dialog == MessageBoxResult.No)
                    return;

                string backup = openFileDialog.FileName;

                var retorno = await Backup.Restaurar($"{backup}");

                if (retorno)
                {
                    MessageBox.Show("A restauração foi feita com sucesso.");
                }
            }
        }

        public void AbrirPastaBackups()
        {
            if (!Directory.Exists(Helper.Diretorios.BACKUPS))
            {
                Directory.CreateDirectory(Helper.Diretorios.BACKUPS);
            }

            Process.Start("Explorer", $"{Helper.Diretorios.BACKUPS}");
        }

        public TabItem AbrirNovaAba(IMainWindowPage tabPage)
        {
            TabItem tabItem = new();
            tabItem.MinWidth = 300;
            tabItem.MaxWidth = 300;
            tabItem.MinHeight = 25;
            tabItem.Header = new TabHeaderContentControl(tabPage.TitleHeader, 280, new RoutedEventHandler((o, t) =>
            {
                CloseCurrentTab();
            }));

            ScrollViewer scrollViewer = new();
            Frame frame = new();

            frame.Content = tabPage;
            scrollViewer.Content = frame;
            tabItem.Content = scrollViewer;
            tabItem.IsSelected = true;

            TabConteudo.Items.Add(tabItem);

            return tabItem;
        }

        public TabItem AbrirNovaAba(string title, Page page)
        {
            return Helper.Controles.AbrirNovaAba(TabConteudo, title, page);
        }

        /// <summary>
        /// Fecha a aba que está sendo exibida ao usuário.
        /// </summary>
        public void CloseCurrentTab()
        {
            if (TabConteudo.Items.Count == 0)
                return;

            var content = (ScrollViewer)TabConteudo.SelectedContent;

            if (content != null)
            {
                Frame frame = (Frame)content.Content;

                if(frame != null && frame.Content is IMainWindowPage tPage)
                {
                    if(!tPage.CanClose)
                    {
                        var result = MessageBox.Show(tPage.CloseWarging, "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.No)
                            return;
                    }
                }
            }

            TabConteudo.Items.RemoveAt(TabConteudo.SelectedIndex);
        }

        /// <summary>
        /// Altera o nome da aba que está sendo exibida ao usuário
        /// </summary>
        public void SetCurrentHeaderTab(string header)
        {
            if (TabConteudo.Items.Count == 0)
                return;

            var tab = (TabItem)TabConteudo.Items[TabConteudo.SelectedIndex];

            if(tab != null)
            {
                var headerValue = (TabHeaderContentControl)tab.Header;

                if (headerValue != null)
                {
                    headerValue.HeaderText = header;
                }
            }
        }

        /// <summary>
        /// Fecha a aba que está sendo exibida ao usuário.
        /// </summary>
        public static void CloseTab()
        {
            Instance?.CloseCurrentTab();
        }

        /// <summary>
        /// Altera o nome da aba que está sendo exibida ao usuário
        /// </summary>
        public static void SetHeaderTab(string header)
        {
            Instance?.SetCurrentHeaderTab(header);
        }
    }
}