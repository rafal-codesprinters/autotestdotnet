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

namespace FunkcjaKwadratowa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal class Wynik
        {

            public double? MiejscePierwsze { get; set; }
            public double? MiejsceDrugie { get; set; }
            public double Delta { get; set;  }
        }

        public MainWindow()
        {
            InitializeComponent();
            miejsce_wyniku.Text = "Program wystartował.";
        }

        Wynik Calculate(double a, double b, double c)
        {
            double x1 = 0;
            double x2 = 0;
            double result = 0;

            double delta = 0; // pośrednie obliczenie
            delta = Math.Pow(b, 2) - (4 * a * c);

            if (delta > 0)
            {
                x1 = (Math.Sqrt(delta) - b) / (2 * a);
                x2 = (-Math.Sqrt(delta) - b) / (2 * a);

                return new Wynik { MiejscePierwsze = x1, MiejsceDrugie = x2, Delta = delta };

            }
            else
            {
                return new Wynik { MiejscePierwsze = null, MiejsceDrugie = null, Delta = delta };
                              
            }
        }

        private void guzik_wyczysc_Click(object sender, RoutedEventArgs e)
        {
            miejsce_wyniku.Text = "Wyczyszczone";
            varA.Clear();
            varB.Clear();
            varC.Clear();
        }

        private void oblicz_Click(object sender, RoutedEventArgs e)
        {
            double a, b, c = 0;
            

            if (Double.TryParse(varA.Text, out a) && Double.TryParse(varB.Text, out b) && Double.TryParse(varC.Text, out c))
            {

                Wynik rezultat = Calculate(a, b, c);

                if (rezultat.Delta < 0)
                {
                    miejsce_wyniku.Text = "";
                    miejsce_wyniku.Text = "Ujemna delta (" + rezultat.Delta + ")!. Wyznaczenie niemożliwe.";
                }
                else
                {
                    miejsce_wyniku.Text = "";
                    miejsce_wyniku.Text = "Delta: " + rezultat.Delta;
                    miejsce_wyniku.Text += "\r\nX1 = " + rezultat.MiejscePierwsze + "\r\n";
                    miejsce_wyniku.Text += "X2 = " + rezultat.MiejsceDrugie;
                }

            } else
            {
                miejsce_wyniku.Text = "";
                miejsce_wyniku.Text = "Wprowadzono błędne wartości \r\n(najpewniej nie są to liczby).";
            }

        }
    }
}
