using Attributes;
using System.Reflection;
using Task1;

namespace Task4
{
    public class Program
    {
        private static void Main()
        {
            Dictionary<string, double> Values = new()
            {
                { "mass", 55 },
                { "height", 165 },
            };

            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Libraries";

            foreach (string dll in Directory.GetFiles(path, "Expressions.*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));

            Console.WriteLine("Classes with attributes are:\n");

            foreach (Assembly assembly in allAssemblies)
            {
                var testType =
                from t in assembly.GetTypes()
                where t.GetCustomAttributes(false).Any(a => a is ExportClass)
                select t;

                foreach (Type type in testType)
                {
                    Console.WriteLine("Class: " + type.Name);

                    object[] constructorParameters = new object[2];
                    constructorParameters[0] = "mass";
                    constructorParameters[1] = "height";

                    var instance = Activator.CreateInstance(type, constructorParameters);

                    Console.WriteLine("-Name of the property is: ");

                    var Properties =
                       from m in type.GetProperties()
                       where m.GetCustomAttributes(false).Any(a => a is ExportPropery)
                       select m;

                    foreach (var property in Properties)
                    {
                        Console.WriteLine("\t" + property.Name);
                        var exp = property.GetValue(instance);
                        if (exp is null)
                            continue;
                        else
                        {
                            Expression expression = (Expression)exp;
                            var Value = expression.Compile();
                            Console.Write("\n Your " + property.Name + " is: ");
                            Console.WriteLine(" = " + Value(Values));
                        }
                    }
                }
            }
        }
    }
}
