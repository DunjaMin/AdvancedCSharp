using Task1;

namespace Expressions.Task5
{
    public interface IExpressionManipulator
    {
        IEnumerable<Expression> Expressions { get; }

        public Expression Manipulate();
    }
}
