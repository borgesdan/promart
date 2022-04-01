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

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for NovoAlunoWindow.xaml
    /// </summary>
    public partial class NovoCadastroWindow : Window
    {
        public string NomeRecebido { get; private set; } = String.Empty;

        public NovoCadastroWindow(string titulo, string frase)
        {
            InitializeComponent();

            Title = titulo;
            mensagemLabel.Content = frase;

            ConfirmarBtn.Click += (object sender, RoutedEventArgs e) =>
            {
                NomeRecebido = NomeAlunoText.Text;
                DialogResult = true;                
            };

            PreviewKeyDown += NovoCadastroWindow_PreviewKeyDown;

            NomeAlunoText.Focus();
        }

        private void NovoCadastroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                NomeRecebido = NomeAlunoText.Text;
                DialogResult = true;
            }

            if (e.Key == Key.Escape)
            {
                DialogResult = false;
            }
        }
    }
}
