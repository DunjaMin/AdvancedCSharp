using Task1;

namespace Task6
{
    public class Program
    {
        public static void Main()
        {
            ConstantExpression a = new(2);
            ConstantExpression b = new(2);

            ParameterizedExpression y = "l";

            ParameterizedExpression c = "c";
            ParameterizedExpression k = new("k");
            ParameterizedExpression d = "d";

            Expression ne = a - c;
            Expression ne2 = b - k;
            Expression ne3 = c - ne;

            ExpressionSingletonLock singleton = ExpressionSingletonLock.GetInstanceLock();

            /* var container1 = new ExpressionContainer(1, "Dunja", ne3);
             var container2 = new ExpressionContainer(2, "Sofija", b);
             var container3 = new ExpressionContainer(3, "Damir", c);

             singleton.AddContainer(container1);
             singleton.AddContainer(container2);
             singleton.AddContainer(container3);

             singleton.ChangeContainerName(1, "Ivana");
             singleton.ReturnIds();

             Console.WriteLine("\n\n");

             Thread thread1 = new(() => singleton.ChangeContainerName(1, "Marija"));

             Thread thread2 = new(() => singleton.ChangeContainerName(1, "Sanja"));
             thread2.Start();

             Thread thread3 = new(() => singleton.ReturnIds());

             var container4 = new ExpressionContainer(4, "Milos", b);
             singleton.AddContainer(container4);

             Thread thread4 = new(() => singleton.ReturnIds());
             thread4.Start();

             Thread thread5 = new(() => singleton.GetContainer(2));
             thread5.Start();

             var container5 = new ExpressionContainer(5, "Luka", b);
             singleton.AddContainer(container5);

             Thread thread6 = new(() => singleton.GetContainer(5));

             thread6.Start();
             thread1.Start();
             thread3.Start();

             thread1.Join();
             thread2.Join();
             thread3.Join();
             thread4.Join();
             thread5.Join();
             thread6.Join();

             Console.WriteLine("\n\n Serialization: \n");

             singleton.Serialize("Doesn'twork.xml");*/

            /* var deserialization = singleton.Deserialize("Doesn'twork.xml");
             foreach (var dd in deserialization)
             {
                 Console.WriteLine(dd.Id);
                 Console.WriteLine(dd.Name);
                 Console.WriteLine(dd.Expression);
             }*/
            //singleton.ReturnIds();
            //singleton.Serialize("text");
            //singleton.XmlWrite("newFile.xml");
            var list = singleton.XmlRead("newFile.xml");
            Console.WriteLine("Names of the containers are: ");
            foreach (var l in list)
            {
                Console.WriteLine(l.Id + " " + l.Name + " " + l.Expression);
            }
        }
    }
}