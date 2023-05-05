using System.Reflection;

//var attributes = typeof(Car).GetProperty("Id").GetCustomAttributes().Select(a => a.GetType());
//Console.WriteLine(attributes.First().Name);
/*
Type t = Type.GetType("Reflection.Calculator");
PropertyInfo[] properties = t.GetProperties();
foreach (PropertyInfo property in properties)
{
    Console.WriteLine(property.Name);
}*/

Assembly asm = Assembly.LoadFrom(@"C:\Users\dunja.mincic\source\repos\Tasks\Reflection\bin\Debug\net6.0\Reflection.dll");
Type ty = asm.GetType("Reflection.Calculator");

var methodInfo = ty.GetMethod("Devide", new Type[] { typeof(double), typeof(double) });
if (methodInfo == null)
{
    throw new Exception("No such method exists.");
}

object[] constructorParameters = new object[3];
constructorParameters[0] = 1;
constructorParameters[1] = 2;
constructorParameters[1] = 3;

var o = Activator.CreateInstance(ty, constructorParameters);

object[] parameters = new object[2];
parameters[0] = 124;
parameters[1] = 176;

var r = methodInfo.Invoke(o, parameters);
Console.WriteLine(r);
