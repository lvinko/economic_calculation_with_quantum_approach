using System;

namespace HybridSearch
{
    public class Product
    {
        public string Name { get; set; }
        public double Vi { get; set; }
        public double U2i { get; set; }
        public double U1i { get; set; }
        public int Qi { get; private set; }
        public int Mi { get; private set; }

        // Constructor
        public Product(string name, double vi, double u2i, double u1i)
        {
            Name = name;
            Vi = vi;
            U2i = u2i;
            U1i = u1i;
            CalculateValues();
        }

        // Calculate Qi and Mi automatically based on formulas
        private void CalculateValues()
        {
            // Qi = ⌊(2 * Vi * U2i / U1i)^0.5⌋
            double calculation = Math.Sqrt(2 * Vi * U2i / U1i);
            Qi = (int)Math.Floor(calculation);

            // Mi = ⌊Qi / 2⌋ + 1
            Mi = (int)Math.Floor(Qi / 2.0) + 1;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Vi: {Vi}, U2i: {U2i}, U1i: {U1i}, Qi: {Qi}, Mi: {Mi}";
        }
    }
}
