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
using Microsoft.Win32;
using System.IO;
using Promart.Codes;

namespace Promart.Windows
{
    /// <summary>
    /// Interaction logic for DefinirFundoWindow.xaml
    /// </summary>
    public partial class DefinirFundoWindow : Window
    {
        public string Arquivo { get; private set; } = string.Empty;
        public double OpacidadeValor { get; private set; }
        public int RedimensionamentoValor { get; private set; }

        public DefinirFundoWindow()
        {
            InitializeComponent();
            AbrirArquivos.Click += AbrirImagem;
            Opacidade.ValueChanged += TrocarOpacidade;
            Redimensionamento.SelectionChanged += Redimensionar;
            Definir.Click += DefinirImagem;
        }

        private void DefinirImagem(object sender, RoutedEventArgs e)
        {
            if (Arquivo != string.Empty)
            {
                OpacidadeValor = Opacidade.Value;
                RedimensionamentoValor = (int)Previa.Stretch;
                DialogResult = true;                
            }
            else
            {
                DialogResult = false;
            }
        }

        private void Redimensionar(object sender, SelectionChangedEventArgs e)
        {
            Previa.Stretch = (Stretch)Redimensionamento.SelectedIndex;
        }

        private void TrocarOpacidade(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Previa.Opacity = Opacidade.Value;
        }

        private void AbrirImagem(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Arquivos PNG (*.png)|*.png";
            fileDialog.Multiselect = false;           

            if(fileDialog.ShowDialog() == true)
            {
                Arquivo = fileDialog.FileName;
                CaminhoImagem.Text = fileDialog.FileName;

                Opacidade.IsEnabled = true;
                Redimensionamento.IsEnabled = true;
                Definir.IsEnabled = true;                

                Previa.Source = new BitmapImage(new Uri(fileDialog.FileName));         
                Previa.Stretch = (Stretch)Redimensionamento.SelectedIndex;                
            }
            else
            {
                Opacidade.IsEnabled = false;
                Redimensionamento.IsEnabled = false;
                Definir.IsEnabled = false;
            }
        }
    }
}
