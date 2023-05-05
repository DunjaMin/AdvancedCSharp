using System.Xml.Serialization;

namespace Task1
{
    [XmlInclude(typeof(ConstantExpression))]
    [XmlInclude(typeof(ParameterizedExpression))]
    [XmlInclude(typeof(BinaryExpression))]
    [XmlRoot("Expression")]
    public abstract class Expression
    {
        public static BinaryExpression operator +(Expression a, Expression b) { return new BinaryExpression(a, b, Operations.Addition); }

        public static BinaryExpression operator *(Expression a, Expression b) { return new BinaryExpression(a, b, Operations.Multiplication); }

        public static BinaryExpression operator -(Expression a, Expression b) { return new BinaryExpression(a, b, Operations.Substraction); }

        public static BinaryExpression operator /(Expression a, Expression b) { return new BinaryExpression(a, b, Operations.Division); }

        public abstract Func<Dictionary<string, double>, double> Compile();
    }
}

