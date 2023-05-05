using System.Xml.Serialization;
using Task1;

namespace Task6
{   
    [XmlRoot("ExpressionContainer")]
    public class ExpressionContainer
    {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "CreationTime")]
        public DateTime CreationTime { get; set; }

        [XmlElement(ElementName = "ModificationTime")]
        public DateTime ModificationTime { get; set; }

        [XmlElement(ElementName = "Expression")]
        public Expression Expression { get; set; }

        public ExpressionContainer(int id, string name, Expression expression)
        {
            Id = id;
            Name = name;
            Expression = expression;
            CreationTime = DateTime.Now;
            ModificationTime = DateTime.Now;
            Console.WriteLine("Expression container created at {0}, Name: {1}", CreationTime, this.Name);
        }

        public ExpressionContainer() { }
    }
}
