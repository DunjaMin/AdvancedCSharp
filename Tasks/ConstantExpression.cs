using System.Xml.Serialization;

namespace Task1
{
   [XmlRoot(ElementName = "ConstantExpression")]
    public class ConstantExpression : Expression
    {
        private double Value { get; set; }

        [XmlAttribute("Value")]
        public double GetValue
        {
            get { return Value; }
            set { Value = value; }
        }
        //was private
        public ConstantExpression() { }

        public ConstantExpression(double value)
        {
            Value = value;
        }

        static public implicit operator ConstantExpression(double value)
        {
            return new ConstantExpression(value);
        }

        public override Func<Dictionary<string, double>, double> Compile()
        {
            return parameters => Value;
        }
    }
}
