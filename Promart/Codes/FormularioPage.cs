using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Promart.Codes
{
    public class FormularioPage<T> : Page where T : class, new()
    {
        public bool DadosAlterados { get; protected set; }
        public T Modelo { get; protected set; }
        public TabItem? Tab { get; set; }
        public MainWindow? MainWindow { get; set; }

        public FormularioPage() : this(new T())
        {
        }

        public FormularioPage(T modelo)
        {
            this.Modelo = modelo;
            MainWindow = Window.GetWindow(this) as MainWindow;
        }
    }
}
