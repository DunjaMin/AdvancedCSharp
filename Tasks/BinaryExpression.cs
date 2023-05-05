using System.Xml.Serialization;

namespace Task1
{
   [XmlRoot("BinaryExpression")]
    public class BinaryExpression : Expression
    {
        private Expression Operand1;

        private Expression Operand2;

        private Operations Operation;

        public BinaryExpression() { }

        [XmlElement(ElementName = "Operand1")]
        public Expression GetOperand1
        {
            get => Operand1;
            set { Operand1 = value; }
        }

        [XmlElement(ElementName = "Operand2")]
        public Expression GetOperand2
        {
            get => Operand2;
            set { Operand2 = value; }
        }

        [XmlElement(ElementName = "Operation")]
        public Operations GetOperation
        {
            get => Operation;
            set { Operation = value; }
        }

        public BinaryExpression(Expression op1, Expression op2, Operations operation)
        {
            Operand1 = op1;
            Operand2 = op2;
            Operation = operation;
        }

        public override Func<Dictionary<string, double>, double> Compile()
        {
            return parameters =>
            {
                switch (Operation)
                {
                    case Operations.Addition:
                        return Operand1.Compile()(parameters) + Operand2.Compile()(parameters);

                    case Operations.Substraction:
                        return Operand1.Compile()(parameters) - Operand2.Compile()(parameters);

                    case Operations.Multiplication:
                        return Operand1.Compile()(parameters) * Operand2.Compile()(parameters);

                    case Operations.Division:
                        return Operand1.Compile()(parameters) / Operand2.Compile()(parameters);

                    default:
                        throw new Exception("Operation not supported");
                }
            };
        }
    }
}
