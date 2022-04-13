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
using Promart.Models;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for NovoMembroComposicaoWindow.xaml
    /// </summary>
    public partial class NovoMembroComposicaoWindow : Window
    {
        public AlunoVinculo? Vinculo { get; set; }
        ushort idade = 0;
        ushort renda = 0;

        public NovoMembroComposicaoWindow()
        {
            InitializeComponent();

            AdicionarButton.Click += AdicionarVinculo;
            IdadeText.PreviewTextInput += IdadeText_PreviewTextInput;
            RendaText.PreviewTextInput += RendaText_PreviewTextInput;
        }

        private void RendaText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidarNumero(e.Text);
        }

        private void IdadeText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidarNumero(e.Text);
        }

        private bool ValidarNumero(string texto)
        {
            return ushort.TryParse(texto, out _);            
        }

        private bool ValidarDados()
        {
            if (string.IsNullOrEmpty(NomeText.Text))
            {
                MessageBox.Show("Digite o nome do membro da família.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                NomeText.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(IdadeText.Text) && ushort.TryParse(IdadeText.Text, out idade))
            {
                if (idade > 130)
                    idade = 130;

                IdadeText.Text = "130";

            }
            else
            {
                IdadeText.Text = "0";
            }

            if (string.IsNullOrEmpty(RendaText.Text) && !ushort.TryParse(IdadeText.Text, out renda))
            {
                RendaText.Text = "0";
            }

            return true;
        }

        private void AdicionarVinculo(object sender, RoutedEventArgs e)
        {
            if (ValidarDados())
            {
                Vinculo = new AlunoVinculo();
                Vinculo.NomeFamiliar = NomeText.Text;
                Vinculo.Idade = idade;
                Vinculo.Ocupacao = OcupacaoText.Text;
                Vinculo.Renda = renda;
                Vinculo.Escolaridade = EscolaridadeText.Text;

                DialogResult = true;
            }
        }
    }
}
