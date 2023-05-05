using Task1;

namespace Task6
{
    internal interface IExpressionSingleton
    {
        public List<int> ReturnIds();

        public ExpressionContainer GetContainer(int id);

        public void ChangeExpression(int id, Expression newExpression);

        public void ChangeContainerName(int id, string newName);
    }
}
