using Task1;

namespace Task6
{
    public class ExpressionSingleton : IExpressionSingleton
    {

        private static ExpressionSingleton singletonInstance;

        private static readonly object _lock = new object();
        private ExpressionSingleton() { }

        public static ExpressionSingleton GetInstance()
        {
            if (singletonInstance == null)
            {
                lock (_lock)
                {
                    if (singletonInstance == null)
                    {
                        singletonInstance = new ExpressionSingleton();
                    }
                }
            }
            return singletonInstance;
        }

        readonly List<ExpressionContainer> expressionContainers = new();

        internal void AddContainer(ExpressionContainer container)
        {
            lock (this)
            {
                for (int i = 0; i < expressionContainers.Count; i++)
                {
                    if (container.Id == expressionContainers[i].Id)
                    {
                        Thread.Sleep(4100);
                        throw new Exception("Container has to have unique ID");
                    }
                }
                expressionContainers.Add(container);

                Thread.Sleep(50);
                Console.WriteLine("~New container added to the list: ");
                foreach (var c in expressionContainers)
                {
                    Console.WriteLine("({0}) {1}", c.Id, c.Name);
                }
            }
        }

        public List<int> ReturnIds()
        {
            List<int> returnId = new();
            Console.WriteLine("Id array: ");
            lock (this)
            {
                foreach (var container in expressionContainers)
                {
                    returnId.Add(container.Id);
                    Console.Write(" " + container.Id);
                }
                Console.WriteLine();
                Thread.Sleep(3000);
                return returnId;
            }
        }

        public ExpressionContainer GetContainer(int id)
        {
            lock (this)
            {

                foreach (var container in expressionContainers)
                {
                    if (container.Id == id)
                    {
                        Console.WriteLine("Container with a given Id is: " + container.Name);
                        return container;
                    }
                }
            }
            Thread.Sleep(200);
            throw new Exception("Container doesn't exist");
        }


        public void ChangeExpression(int id, Expression newExpression)
        {
            lock (this)
            {
                foreach (var container in expressionContainers)
                {
                    if (container.Id == id)
                    {
                        //container.Expression = newExpression;
                        container.ModificationTime = DateTime.Now;
                        Thread.Sleep(75);
                        //Console.WriteLine("New expression is {0} , and modification time is: {1}", container.Expression, container.ModificationTime);
                    }
                }
            }
        }

        public void ChangeContainerName(int id, string newName)
        {
            lock (this)
            {
                foreach (var container in expressionContainers)
                {
                    if (container.Id == id)
                    {
                        Thread.Sleep(1800);
                        container.Name = newName;
                        container.ModificationTime = DateTime.Now;
                        Console.WriteLine("New name is {0} , and modification time is: {1}", newName, container.ModificationTime);
                    }
                }
            }
        }
    }
}
