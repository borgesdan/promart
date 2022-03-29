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
using Promart.Models;

namespace Promart.Pages
{
    /// <summary>
    /// Interaction logic for CadastroOficinaPage.xaml
    /// </summary>
    public partial class CadastroOficinaPage : Page
    {
        public Oficina? Oficina { get; private set; }

        public CadastroOficinaPage()
        {
            InitializeComponent();
        }

        public CadastroOficinaPage(Oficina oficina)
        {
            InitializeComponent();

            Oficina = oficina;
        }
    }
}
