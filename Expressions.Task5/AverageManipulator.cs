using System.ComponentModel.Composition;
using Task1;

namespace Expressions.Task5
{
    [Export(typeof(IExpressionManipulator))]
    public class AverageManipulator : IExpressionManipulator
    {
        public IEnumerable<Expression> Expressions { get; }

        [ImportingConstructor]
        public AverageManipulator([Import("Expressions")] IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        public Expression Manipulate()
        {
            if (Expressions == null) 
                throw new ArgumentNullException(nameof(Expressions)); 
            else
            {
                Expression sum = Expressions.First();

                for (int i = 1; i < Expressions.Count(); i++)
                {
                    sum = sum + Expressions.ElementAt(i);
                }

                ConstantExpression n = Expressions.Count();
                return sum / n;
            }
        }
    }
}
