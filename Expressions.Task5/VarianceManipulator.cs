using System.ComponentModel.Composition;
using Task1;

namespace Expressions.Task5
{

    [Export(typeof(IExpressionManipulator))]
    public class VarianceManipulator : IExpressionManipulator
    {
        private readonly ConstantExpression @const = 1;

        public IEnumerable<Expression> Expressions { get; }

        [ImportingConstructor]
        public VarianceManipulator([Import("Expressions")] IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        public Expression Manipulate()
        {
            AverageManipulator averageManipulator = new AverageManipulator(Expressions);
            var X = averageManipulator.Manipulate();
             
            //Check if Expressions are null or empty
            Expression sum = (Expressions.First() - X);

            for (int i = 1; i < Expressions.Count(); i++)
            {
                sum = sum + (Expressions.ElementAt(i) - X) * (Expressions.ElementAt(i) - X);
            }
            ConstantExpression n = Expressions.Count();
            return sum / (n - @const);            
        }
    }
}
