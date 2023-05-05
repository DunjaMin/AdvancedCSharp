using System.ComponentModel;
using System.Reflection;

namespace Reflection
{
    public class Calculator
    {
        double value1;
        double value2;
        private double number;
        public Calculator(double v1, double v2, double num)
        {
            value1 = v1;
            value2 = v2;
            number = num;
        }

        public double Number
        {
            get { return number; }
            set { number = value; }
        }
        public static double Add(double number, double value)
        {
            return number + value;
        }
        public static double Devide(double number, double value)
        {
            return number / value;
        }


        [DefaultValue(3.14)]
        public static double Pi()
        {
            return 3.14;
        }
        public static double SquarePi()
        {
            return 3.14 * 3.14;
        }
    }
}
