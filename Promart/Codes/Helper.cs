using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Promart.Codes
{
    public class Helper
    {
        public static class Controles
        {
            public static void AbrirNovaAba(TabControl tabControl, string tabHeader, Page contentPage)
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
