namespace FunkcjaKwadratowa
{
    internal class SquareFunction
    {
        public double? WartoscA { get; set; }
        public double? WartoscB { get; set; }
        public double? WartoscC { get; set; }
        public double? MiejscePierwsze { get; set; }
        public double? MiejsceDrugie { get; set; }
        public double Delta { get; set; }
    }

    public void Calculate(double a, double b, double c) {
        private double x1 = 0;
        private double x2 = 0;
        // private double result = 0; for deletion

        private double delta = 0; // pośrednie obliczenie
        delta = Math.Pow(b, 2) - (4 * a * c);

        if (delta > 0)
        {
            x1 = (Math.Sqrt(delta) - b) / (2 * a);
            x2 = (-Math.Sqrt(delta) - b) / (2 * a);

            MiejscePierwsze = x1; MiejsceDrugie = x2; Delta = delta;

        }
        else
        {
            return new Wynik { MiejscePierwsze = null, MiejsceDrugie = null, Delta = delta };

        }
    }



}