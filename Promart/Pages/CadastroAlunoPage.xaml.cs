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
    /// Interaction logic for CadastrarAlunoPage.xaml
    /// </summary>
    public partial class CadastroAlunoPage : Page
    {
        public Aluno? Aluno { get; private set; }

        public CadastroAlunoPage()
        {
            InitializeComponent();
        }

        public CadastroAlunoPage(Aluno aluno)
        {
            InitializeComponent();
            Aluno = aluno;
            NomeAlunoText.Text = aluno.NomeCompleto;
        }
    }
}
