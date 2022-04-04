using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Promart.Data;
using Promart.Models;
using System.Windows;

namespace Promart.Codes
{
    public class Helper
    {
        public static class Controles
        {
            public static TabItem AbrirNovaAba(TabControl tabControl, string tabHeader, Page contentPage)
            {
                TabItem tabItem = new();
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

            public static ListBox PopularOficinasList(ListBox listBox, Action<object, RoutedEventArgs> checkBoxClickEventoComum)
            {
                var oficinas = SqlAccess.GetDados<Oficina>();
                List<CheckBox> checkBoxes = new();

                foreach (var o in oficinas)
                {
                    CheckBox checkBox = new();
                    checkBox.Content = o;
                    checkBox.Click += (object sender, RoutedEventArgs e) => checkBoxClickEventoComum(sender, e);
                    checkBoxes.Add(checkBox);
                }

                listBox.ItemsSource = checkBoxes;

                return listBox;
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
        }
    }
}
