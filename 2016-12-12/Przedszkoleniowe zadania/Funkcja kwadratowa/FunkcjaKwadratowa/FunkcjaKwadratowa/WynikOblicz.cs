using System.Collections;
using System.Collections.Generic;

namespace FunkcjaKwadratowa
{
    internal class WynikOblicz : IEnumerable<double>
    {
        private double[] m_miejscaZerowe;

        public WynikOblicz(params double[] miejscaZerowe)
        {
            m_miejscaZerowe = miejscaZerowe;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)m_miejscaZerowe).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}