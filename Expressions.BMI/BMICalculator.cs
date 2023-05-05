using Attributes;
using Task1;

namespace Expressions.BMI
{
    [ExportClass]
    public class BMICalculator
    {
        private readonly ParameterizedExpression Mass;
        private readonly ParameterizedExpression Height;

        // Can be done without constructor

        public BMICalculator(string mass, string height)
        {
            Mass = mass;
            Height = height;
        }

        [ExportPropery]
        public Expression BMI => Mass / (Height * Height);

        // Second solution
        // public Expression BMI => new ParameterizedExpression("mass") / (new ParameterizedExpression("height") * new ParameterizedExpression("height"));
    }
}