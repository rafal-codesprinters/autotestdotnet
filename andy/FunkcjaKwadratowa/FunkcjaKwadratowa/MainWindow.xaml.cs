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

        public MainWindow()
        {
            InitializeComponent();
            miejsce_wyniku.Text = "Program wystartował.";
        }

        private void Calculate(double a, double b, double c)
        {
            double x1 = 0;
            double x2 = 0;
            double result = 0;

            double delta = 0; // pośrednie obliczenie
            delta = Math.Pow(b, 2) - (4 * a * c);

            if (delta > 0)
            {
                miejsce_wyniku.Text = "";
                miejsce_wyniku.Text = "Delta: " + delta;

                x1 = (Math.Sqrt(delta) - b) / (2 * a);
                x2 = (-Math.Sqrt(delta) - b) / (2 * a);

                miejsce_wyniku.Text += "\r\nX1 = " + x1 + "\r\n";
                miejsce_wyniku.Text += "X2 = " + x2;
            }
            else
            {
                miejsce_wyniku.Text = ("Ujemna delta! Wyznaczenie niemożliwe.");
                              
                // Console.WriteLine("Ujemna delta ({0:G}) - x1 i x2 nie istnieją.", delta);
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

            a = Double.Parse(varA.Text);
            b = Double.Parse(varB.Text);
            c = Double.Parse(varC.Text);

            Calculate(a, b, c);
        }
    }
}
