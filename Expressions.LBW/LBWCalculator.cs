using Attributes;
using Task1;

namespace Expressions.LBW
{
    [ExportClass]
    public class LBWCalculator
    {
        private readonly ParameterizedExpression Mass;
        private readonly ParameterizedExpression Height;

        private readonly ConstantExpression const1 = 1.09;
        private readonly ConstantExpression const2 = 100;
        private readonly ConstantExpression const3 = 128;

        public LBWCalculator(string mass, string height)
        {
            Mass = mass;
            Height = height;
        }

        [ExportPropery]
        public Expression LBW => (const1 * Mass) - const3 * (Mass * Mass / (const2 * Height) * Mass);
    }
}