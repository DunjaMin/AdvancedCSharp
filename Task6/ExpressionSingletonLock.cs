using System.Xml;
using System.Xml.Serialization;
using Task1;

namespace Task6
{
    public class ExpressionSingletonLock : IExpressionSingleton
    {

        private static ExpressionSingletonLock singletonInstanceLock;

        private static readonly object _lock = new object();
        private ExpressionSingletonLock() { }

        public static ExpressionSingletonLock GetInstanceLock()
        {
            if (singletonInstanceLock == null)
            {
                lock (_lock)
                {
                    if (singletonInstanceLock == null)
                    {
                        singletonInstanceLock = new ExpressionSingletonLock();
                        // singletonInstanceLock.expressionContainers = new List<ExpressionContainer>();

                    }
                }
            }
            return singletonInstanceLock;
        }

        List<ExpressionContainer> expressionContainers = new();


        private ReaderWriterLockSlim cacheLock = new();

        public void AddContainer(ExpressionContainer container)
        {
            cacheLock.EnterWriteLock();
            try
            {
                for (int i = 0; i < expressionContainers.Count; i++)
                {
                    if (container.Id == expressionContainers[i].Id)
                    {
                        //Thread.Sleep(4100);
                        throw new Exception("Container has to have unique ID");
                    }
                }
                expressionContainers.Add(container);

                //Thread.Sleep(50);
                Console.WriteLine("~New container added to the list: ");
                foreach (var c in expressionContainers)
                {
                    Console.WriteLine("({0}) {1}", c.Id, c.Name);
                }
            }
            finally { cacheLock.ExitWriteLock(); }
        }

        public List<int> ReturnIds()
        {
            cacheLock.EnterReadLock();
            try
            {
                // Thread.Sleep(3000);
                Console.WriteLine("Id array: ");
                return expressionContainers.Select(c => c.Id).ToList();
            }
            finally { cacheLock.ExitReadLock(); }
        }

        public ExpressionContainer GetContainer(int id)
        {
            cacheLock.EnterReadLock();
            try
            {
                var container = expressionContainers.Find(e => e.Id == id);
                if (container == null)
                {
                    throw new Exception("Container doesn't exist");
                }
                else
                {
                    // Thread.Sleep(3000);
                    Console.WriteLine("Container with a given Id is: " + container.Name);
                    return container;
                }
            }
            finally { cacheLock.ExitReadLock(); }
        }

        public void ChangeExpression(int id, Expression newExpression)
        {
            cacheLock.EnterWriteLock();
            try
            {
                var container = expressionContainers.Find(e => e.Id == id);
                if (container == null)
                {
                    throw new Exception("Container does not exist");
                }
                //Write Lock should protect this part under
                container.Expression = newExpression;
                container.ModificationTime = DateTime.Now;
                Thread.Sleep(75);
                Console.WriteLine("New expression is {0} , and modification time is: {1}", container.Expression, container.ModificationTime);
            }
            finally { cacheLock.ExitWriteLock(); }
        }

        public void ChangeContainerName(int id, string newName)
        {
            cacheLock.EnterWriteLock();
            try
            {
                var container = expressionContainers.Find(e => e.Id == id);
                if (container == null)
                {
                    throw new Exception("Container does not exist");
                }
                container.Name = newName;
                container.ModificationTime = DateTime.Now;
                Thread.Sleep(75);
                Console.WriteLine("New name is {0} , and modification time is: {1}", newName, container.ModificationTime);
            }
            finally { cacheLock.ExitWriteLock(); }
        }

        public bool Serialize(string path)
        {
            try
            {
                using (var stream = File.Create(path))
                {
                    var serializer = new XmlSerializer(typeof(List<ExpressionContainer>), new Type[] { typeof(Expression) });
                    // var serializer = new XmlSerializer(typeof(ExpressionSingletonLock));
                    serializer.Serialize(stream, expressionContainers);
                }
                return true;
            }
            catch
            {
                throw new Exception("Serialization failed");
            }

        }

        public List<ExpressionContainer> Deserialize(string path)
        {
            try
            {
                var mySerializer = new XmlSerializer(typeof(List<ExpressionContainer>), new Type[] { typeof(Expression) });
                using var myFileStream = new FileStream(path, FileMode.Open);
                expressionContainers = (List<ExpressionContainer>)mySerializer.Deserialize(myFileStream);

                myFileStream.Close();

                return expressionContainers;
            }
            catch
            {
                throw new Exception("Deserialization failed");
            }
        }
        public void XmlExpression(string path, XmlWriter writer, Expression ex)
        {
            if (ex is Task1.ConstantExpression)
            {
                var neew = (ConstantExpression)ex;

                writer.WriteStartElement("Expression");
                writer.WriteAttributeString("type", "ConstantExpression");
                writer.WriteAttributeString("value", neew.GetValue.ToString());
                writer.WriteEndElement();
            }
            else if (ex is ParameterizedExpression)
            {
                var neew = (ParameterizedExpression)ex;

                writer.WriteStartElement("Expression");
                writer.WriteAttributeString("type", "ParameterizedExpression");
                writer.WriteAttributeString("parameter", neew.GetParameter.ToString());
                writer.WriteEndElement();
            }
            else
            {
                var neew = (BinaryExpression)ex;
                writer.WriteStartElement("Expression");
                writer.WriteAttributeString("type", "BinaryExpression");

                writer.WriteStartElement("Operand1");
                XmlExpression(path, writer, neew.GetOperand1);
                writer.WriteEndElement();

                writer.WriteStartElement("Operand2");
                XmlExpression(path, writer, neew.GetOperand2);
                writer.WriteEndElement();

                writer.WriteStartElement("Operation");
                writer.WriteValue(neew.GetOperation.ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public void XmlWrite(string path)
        {
            DateTime date = DateTime.Now;

            XmlWriter xmlWriter = XmlWriter.Create(path);

            xmlWriter.WriteStartElement("ArrayOfExpressionContainer");

            foreach (var c in expressionContainers)
            {
                xmlWriter.WriteStartElement("ExpressionContainer");

                xmlWriter.WriteStartElement("Id");
                xmlWriter.WriteValue(c.Id);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteValue(c.Name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("CreationTime");
                xmlWriter.WriteValue(c.CreationTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ModificationTime");
                xmlWriter.WriteValue(c.ModificationTime);
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteStartElement("Expression");
                XmlExpression(path, xmlWriter, c.Expression);

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        public Expression ExpressionOpRead(XmlReader read)
        {
            var expressionType = read.GetAttribute("type");
            var returnExpression = new BinaryExpression();
            dynamic operand;

            if (expressionType is "ConstantExpression")
            {
                try
                {
                    operand = new ConstantExpression();

                    operand.GetValue = Convert.ToDouble(read.GetAttribute("value"));
                    Console.WriteLine("Value: " + operand.GetValue);
                    return operand;
                }
                catch
                {
                    throw new Exception("Something is missing");
                }

            }
            else if (expressionType is "ParameterizedExpression")
            {
                try
                {
                    operand = new ParameterizedExpression();
                    operand.GetParameter = read.GetAttribute("parameter");
                    Console.WriteLine("Parameter: " + operand.GetParameter);
                    return operand;
                }
                catch
                {
                    throw new Exception("Something is missing");
                }
            }
            else if (expressionType is "BinaryExpression")
            {
                try
                {
                    operand = new BinaryExpression();
                    returnExpression = new BinaryExpression();
                    read.Read();
                    if (read.Name is "Operand1")
                    {
                        read.Read();
                        operand.GetOperand1 = ExpressionOpRead(read);
                    }

                    read.Read();
                    read.Read();
                    if (read.Name is "Operand2")
                    {
                        read.Read();
                        operand.GetOperand2 = ExpressionOpRead(read);
                    }

                    read.Read();
                    read.Read();
                    if (read.Name is "Operation")
                    {
                        var @operator = read.ReadElementContentAsString();
                        operand.GetOperation = (Operations)Enum.Parse(typeof(Operations), @operator);

                    }
                    if (operand != null)
                    {
                        returnExpression = operand;
                    }
                    return returnExpression;
                }
                catch
                {
                    throw new Exception("Something is missing");
                }
            }
            return returnExpression;
        }
        public List<ExpressionContainer> XmlRead(string path)
        {

            XmlReader reader = XmlReader.Create(path);

            var expressionContainers1 = new List<ExpressionContainer>();
            int i = -1;

            while (reader.Read())
            {
                reader.Read();
                if (reader.Name == "ArrayOfExpressionContainer")
                {
                    reader.Read();
                }
                if (reader.IsStartElement() && reader.Name == "ExpressionContainer")
                {
                    expressionContainers1.Add(new ExpressionContainer());
                    i++;
                    Console.WriteLine("Container added");
                    reader.Read();
                }
                if (reader.Name is "Id")
                {
                    expressionContainers1[i].Id = reader.ReadElementContentAsInt();
                    Console.WriteLine(expressionContainers1[i].Id);
                }
                if (reader.Name is "Name")
                {
                    expressionContainers1[i].Name = reader.ReadElementContentAsString();
                    Console.WriteLine(expressionContainers1[i].Name);
                }
                if (reader.Name is "CreationTime")
                {
                    expressionContainers1[i].CreationTime = reader.ReadElementContentAsDateTime();
                    Console.WriteLine(expressionContainers1[i].CreationTime);
                }
                if (reader.Name is "ModificationTime")
                {
                    expressionContainers1[i].ModificationTime = reader.ReadElementContentAsDateTime();
                    Console.WriteLine(expressionContainers1[i].ModificationTime);
                }
                if (reader.IsStartElement() && reader.Name is "Expression")
                {
                    expressionContainers1[i].Expression = ExpressionOpRead(reader);
                }
            }
            reader.Close();
            return expressionContainers1;
        }
    }
}
