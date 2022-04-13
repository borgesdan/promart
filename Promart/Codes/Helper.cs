using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Promart.Data;
using Promart.Models;
using System.Windows;
using System.Windows.Input;
using Promart.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace Promart.Codes
{
    public class Helper
    {
        public static class Diretorios 
        {
            public static string ATUAL = Environment.CurrentDirectory;
            public static string SALVOS = ATUAL + "/Salvos";
            public static string FOTOS = SALVOS + "/Fotos";
            public static string FOTOS_ALUNOS = SALVOS + "/Fotos/Alunos";

            //Caminhos relativos
            public static string REL_SALVOS = "/Salvos";
            public static string REL_FOTOS = REL_SALVOS + "/Fotos";
            public static string REL_FOTOS_ALUNOS = REL_FOTOS + "/Alunos";
        }

        public static class Controles
        {
            public static TabItem AbrirNovaAba(TabControl tabControl, string tabHeader, Page contentPage)
            {
                TabItem tabItem = new();
                tabItem.MinWidth = 300;
                tabItem.MaxWidth = 300;
                tabItem.MinHeight = 25;
                tabItem.Header = new TabHeaderContentControl(tabHeader, 280, new RoutedEventHandler((o, t) => {
                    var result = MessageBox.Show("Todos os dados não salvos serão perdidos. Deseja fechar a aba?", "Fechar aba", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    
                    if(result == MessageBoxResult.Yes)
                    {
                        RemoverAba(tabItem);
                    }                    
                }));
                
                ScrollViewer scrollViewer = new();
                Frame frame = new();

                frame.Content = contentPage;
                scrollViewer.Content = frame;
                tabItem.Content = scrollViewer;                
                tabItem.IsSelected = true;

                tabControl.Items.Add(tabItem);
                return tabItem;
            }
            
            public static void RemoverAba(TabItem? tab)
            {
                if (tab != null && tab.Parent is TabControl)
                {
                    TabControl? tabControl = tab.Parent as TabControl;
                    tabControl?.Items.Remove(tab);
                }
            }

            public static bool DadosAlteradosAviso()
            {
                MessageBoxResult result = MessageBox.Show("Alguns dados foram alterados, deseja sair sem confirma-los?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return result == MessageBoxResult.Yes;
            }


            public static async Task<IEnumerable<Oficina>?> PopularOficinasListBoxAsync(ListBox listBox)
            {
                var oficinas = await SqlAccess.GetDadosAsync<Oficina>();
                listBox.ItemsSource = oficinas;

                return oficinas;
            }

            public static async Task PopularOficinasListComCheckBoxAsync(ListBox listBox, Action<object, RoutedEventArgs> checkBoxClickEventoComum)
            {
                var oficinas = await SqlAccess.GetDadosAsync<Oficina>();

                if (oficinas == null)
                    return;

                List<CheckBox> checkBoxes = new();
                foreach (var o in oficinas)
                {
                    CheckBox checkBox = new();
                    checkBox.Content = o;
                    checkBox.Click += (object sender, RoutedEventArgs e) => checkBoxClickEventoComum(sender, e);
                    checkBoxes.Add(checkBox);
                }

                listBox.ItemsSource = checkBoxes;
            }
        }
        
        public static class Util
        {
            public static string ObterExtensaoArquivo(string nomeArquivoComExtensao)
            {
                string[] extensao = nomeArquivoComExtensao.Split('.');
                return extensao.Length == 2 ? extensao[1] : string.Empty;
            }

            public static BitmapImage? AbrirSalvarImagem(string diretorioDestino, string nomeImagem)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Arquivos de Imagem (*.PNG, *.JPG, *.BMP)|*.png;*.jpg;*.bmp";
                var result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    if (!Directory.Exists(diretorioDestino))
                    {
                        Directory.CreateDirectory(diretorioDestino);
                    }

                    string extensao = ObterExtensaoArquivo(openFileDialog.SafeFileName);
                    string arquivoFoto = $"{nomeImagem}.{extensao[1]}";
                    string caminhoFinal = $"{diretorioDestino}/{arquivoFoto}";

                    File.Copy(openFileDialog.FileName, caminhoFinal);

                    return new BitmapImage(new Uri(caminhoFinal));
                }

                return null;
            }

            public static int ObterIdade(DateTime nascimento)
            {
                DateTime hoje = DateTime.Now;
                int idade = hoje.Year - nascimento.Year;                

                if (hoje.DayOfYear < nascimento.DayOfYear)
                {
                    idade--;
                }

                return idade;
            }
            
            public static bool VerificarSomenteNumero(Key key)
            {
                return (key >= Key.NumPad0 && key <= Key.NumPad9) || (key >= Key.D0 && key <= Key.D9);
            }

            public static void FormatarCPF(TextBox textBox)
            {
                if (textBox.Text.Length == 3 || textBox.Text.Length == 7)
                {
                    textBox.Text += ".";
                    textBox.CaretIndex = textBox.Text.Length;
                }
                if (textBox.Text.Length == 11)
                {
                    textBox.Text += "-";
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }

            public static string FormatarCPF(string numeroAtual)
            {
                if(numeroAtual.Length == 3 || numeroAtual.Length == 7)
                {
                    return numeroAtual += ".";
                }
                else if (numeroAtual.Length == 11)
                {
                    return numeroAtual += "-";
                }

                return numeroAtual;
            }
        }
    }
}
