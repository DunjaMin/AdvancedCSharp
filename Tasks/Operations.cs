using System.Xml.Serialization;

namespace Task1
{
    public enum Operations
    {
        [XmlEnum(Name = "Addition")]
        Addition = '+',

        [XmlEnum(Name = "Substraction")]
        Substraction = '-',

        [XmlEnum(Name = "Division")]
        Division = '/',

        [XmlEnum(Name = "Multiplication")]
        Multiplication = '*'
    }
}