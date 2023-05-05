using System.Xml.Serialization;

namespace Task1
{
    [XmlRoot("ParameterizedExpression")]
    public class ParameterizedExpression : Expression
    {

        private string Parameter;

        public ParameterizedExpression() { }

        [XmlAttribute("Parameter")]
        public string GetParameter { get { return Parameter; } set { Parameter = value; } }

        public ParameterizedExpression(string parameter)
        {
            Parameter = parameter;
        }

        static public implicit operator ParameterizedExpression(string value)
        {
            return new ParameterizedExpression(value);
        }

        public override Func<Dictionary<string, double>, double> Compile()
        {
            double Calculate(Dictionary<string, double> parameters)
            {
                if (parameters.TryGetValue(Parameter, out double value))
                    return value;
                else
                    throw new Exception("There's no key");
            }
            return Calculate;
        }
    }
}
