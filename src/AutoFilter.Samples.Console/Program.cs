using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoFilter.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // The data sample.
            var data = new SampleTarget[] {
                new SampleTarget() { String = "String ABC", DateTime = new DateTime(2001, 1, 2), Int32 = 1, Boolean = true },
                new SampleTarget() { String = "String CDE", DateTime = new DateTime(2003, 4, 5), Int32 = 2, Boolean = false },
                new SampleTarget() { String = "String EFG", DateTime = new DateTime(2006, 7, 8), Int32 = 3, Boolean = true },
            };

            // The auto filter builder and strategies selected.
            var builder = new AutoFilterBuilder<SampleTarget>(
                new BooleanEqualAutoFilterStrategy(),
                new StringContainsIgnoreCaseAutoFilterStrategy(),
                new DateTimeEqualAutoFilterStrategy(),
                new Int32EqualAutoFilterStrategy());

            do
            {
                Console.Clear();
                Console.WriteLine("Available targets:");
                ListTargets(data);

                Console.WriteLine("Type your filter:");
                var filter = Console.ReadLine();

                // Build the auto filter expression using the filter typed by the user.
                var expression = builder.Build(filter);

                // Filters the data.
                var filteredData = data.Where(expression.Compile());
                ListTargets(filteredData);

                Console.WriteLine("Type any key to filter again or type ESC to exit.");                
                
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

        }

        private static void ListTargets(IEnumerable<SampleTarget> filteredTargets)
        {
            foreach (var t in filteredTargets)
            {
                Console.WriteLine(
                    "String: {0}, DateTime: {1}, Int32: {2}, Boolean: {3}", 
                    t.String, 
                    t.DateTime, 
                    t.Int32, 
                    t.Boolean);
            }
        }
    }
}
