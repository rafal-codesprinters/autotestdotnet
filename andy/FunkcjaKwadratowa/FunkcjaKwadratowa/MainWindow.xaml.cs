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
        }

        private static void Calculate(double a, double b, double c)
        {
            double x1 = 0;
            double x2 = 0;
            double result = 0;

            // todo napisz obliczanie rozwiązań (miejsc zerowych) funkcji kwadratowej 
            // jeśli nie pamiętasz jak to się liczy to tutaj jest ściąga
            // http://matma.prv.pl/kwadratowa.php
            // postaraj się napisac to samodzielnie a nie googlując implementację
            // powodzenia :)

            double delta = 0; // pośrednie obliczenie
            delta = Math.Pow(b, 2) - (4 * a * c);

            if (delta > 0)
            {

                x1 = (Math.Sqrt(delta) - b) / (2 * a);
                x2 = (-Math.Sqrt(delta) - b) / (2 * a);

            }
            else
            {
                miejsce_wyniku.Text = "Ujemna delta!";
                
                // Console.WriteLine("Ujemna delta ({0:G}) - x1 i x2 nie istnieją.", delta);
            }
        }

        private void guzik_wyczysc_Click(object sender, RoutedEventArgs e)
        {
            miejsce_wyniku.Text = "Wyczyszczone";
        }

        private void oblicz_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
