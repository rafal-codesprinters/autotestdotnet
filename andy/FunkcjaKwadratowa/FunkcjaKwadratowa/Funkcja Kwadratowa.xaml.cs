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

                Wynik rezultat = SquareFunction.Calculate(a, b, c);

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

            } else // Walidacja się nie udała
            {
                miejsce_wyniku.Text = "";
                miejsce_wyniku.Text = "Wprowadzono błędne wartości \r\n(najpewniej nie są to liczby).";
            }

        }
    }
}
