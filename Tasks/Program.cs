namespace Task1
{
    public static class StringExtension
    {
        public static void ToStringExtenstion(this Expression expression)
        {
            if (expression is ConstantExpression constantexpression)
                Console.Write(constantexpression.GetValue);            
            else if (expression is ParameterizedExpression parameterizedexpression)
                Console.Write(parameterizedexpression.GetParameter);           
            else
            {
                BinaryExpression binaryexpression = (BinaryExpression)expression;
                Expression left = binaryexpression.GetOperand1;
                Expression right = binaryexpression.GetOperand2;
                Operations operation = binaryexpression.GetOperation;

                if (operation is Operations.Addition)
                {
                    Console.Write("(");
                    ToStringExtenstion(left);
                    Console.Write("+");
                    ToStringExtenstion(right);
                    Console.Write(")");
                }
                else if (operation is Operations.Division)
                {
                    ToStringExtenstion(left);
                    Console.Write("/");
                    ToStringExtenstion(right);
                }
                else if (operation is Operations.Multiplication)
                {
                    ToStringExtenstion(left);
                    Console.Write("*");
                    ToStringExtenstion(right);
                }
                else
                {
                    Console.Write("(");
                    ToStringExtenstion(left);
                    Console.Write("-");
                    ToStringExtenstion(right);
                    Console.Write(")");
                }
            }
        }
    }
    public class Aritm
    {
        public static void Main()
        {
            #region Expressions

            Dictionary<string, double> Values = new()
            {
                { "k", 0 },
                { "c", 2 },
                { "Mass", 55 },
                { "Height", 165 },
                { "d", 3 }
            };

            ConstantExpression a = new(2);
            ConstantExpression b = new(7.0);
            
            ParameterizedExpression y = "l";
            ParameterizedExpression c = "c";
            ParameterizedExpression k = new ("k");
            ParameterizedExpression d = "d";

            Expression ne = a - c;
            Expression ne2 = b - k;
            Expression ne3 = c - ne;
            Expression ex = (k + ne) / (b - new ConstantExpression(1));
            //Expression BMI=();
            Expression ex2 = a - c * b;
            Expression ne4 = c + d;


            ne.ToStringExtenstion();
            var func = ne.Compile();
            Console.WriteLine(" = " + func(Values));

            ne2.ToStringExtenstion();
            var func2 = ne2.Compile();
            Console.WriteLine(" = " + func2(Values));

            ex.ToStringExtenstion();
            var func1 = ex.Compile();
            Console.WriteLine(" = " + func1(Values));

            ex2.ToStringExtenstion();
            var func8 = ex2.Compile();
            Console.WriteLine(" = " + func8(Values));
            #endregion
        }
    }
}

