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

namespace Promart.Codes
{
    public class Helper
    {
        public static class Controles
        {
            public static TabItem AbrirNovaAba(TabControl tabControl, string tabHeader, Page contentPage)
            {
                TabItem tabItem = new();
                tabItem.MinWidth = 300;
                tabItem.MaxWidth = 300;
                tabItem.MinHeight = 25;
                tabItem.Header = tabHeader;                
                
                ScrollViewer scrollViewer = new();
                Frame frame = new();

                frame.Content = contentPage;
                scrollViewer.Content = frame;
                tabItem.Content = scrollViewer;

                tabItem.Header = tabHeader;                
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
