using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Task1;

namespace Expressions.Task5
{
    public class Program
    {
        public static void Main()
        {
            ParameterizedExpression[] expressionArray = new ParameterizedExpression[3]
             {
                    new ParameterizedExpression("x1"),
                    new ParameterizedExpression("x2"),
                    new ParameterizedExpression("x3"),
             };

            Dictionary<string, double> Values = new()
            {
                { "x1", 9 },
                { "x2", 8 },
                { "x3", 7 }
            };

            Runner runner = new(expressionArray);
            runner.Run();
            runner.Calculate(Values);
        }
    }

    public class Runner
    {
        [ImportMany(typeof(IExpressionManipulator))]
        private IEnumerable<IExpressionManipulator>? expressionManipulators;

        [Export("Expressions")]
        public IEnumerable<Expression> Expressions { get; }

        public Runner(IEnumerable<Expression> expressions)
        {
            Expressions = expressions;
        }

        public void Run()
        {
            try
            {
                var catalog = new AggregateCatalog();
                var ac = new AssemblyCatalog(typeof(Program).Assembly);

                catalog.Catalogs.Add(ac);

                var container = new CompositionContainer(catalog);

                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public void Calculate(Dictionary<string, double> values)
        {
            if (expressionManipulators is null)
                throw new ArgumentNullException(nameof(expressionManipulators));
            else
                foreach (IExpressionManipulator expressionManipulator in expressionManipulators)
                {
                    var func = expressionManipulator.Manipulate();
                    var compiled = func.Compile();
                    Console.WriteLine(compiled(values));
                }
        }
    }
}
