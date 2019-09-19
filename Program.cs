using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace C__certificate_training
{
    class Person
            {
                public string Name { get; set; }
                public string City { get; set; }
            }
    
    class Program
    {
        static void Task1()
        {
            Console.WriteLine("Task 1 starging");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }

        static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(1000);
            Console.WriteLine("Task 2 ending");
        }

        static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }

        static void Main(string[] args)
        {
            // 1-1 (corrected)
            // Parallel.Invoke(Task1, Task2);
            // Console.WriteLine("Finishing processing. Press a key to end.");

            // 1-2
            // var items = Enumerable.Range(0, 500);
            // Parallel.ForEach(items, item =>
            // {
            //     WorkOnItem(item);
            // });
            // Console.WriteLine("Finishing processing. Press a key to end.");

            // 1-3 (corrected)
            // var items = Enumerable.Range(0, 500).ToArray();
            // Parallel.ForEach(items, (item) =>
            // {
            //     WorkOnItem(item);
            // });
            // Console.WriteLine("All done. Press a key to end it all.");

            // 1-4 (corrected)
            // var items = Enumerable.Range(0, 500).ToArray();
            // ParallelLoopResult result = Parallel.ForEach(items, (int item, ParallelLoopState loopState)=>
            // {
            //     if (item == 200)
            //     {
            //         loopState.Break();
            //     }

            //     WorkOnItem(item);
            // });
            // Console.WriteLine($"Completed: {result.IsCompleted}");
            // Console.WriteLine($"Items: {result.LowestBreakIteration}");
            // Console.WriteLine("All done. Going to take a nap now.");

            // 1-5 / 1-6 / 1-7
            Person [] peepz = new Person [] {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "Seattle" },
                new Person { Name = "James", City = "London" },
            };

            // var result = from person in peepz.AsParallel()
            //                                  .AsOrdered()   // Reorder to original order after execution. Impacts performance.
            //                                  .WithDegreeOfParallelism(4)    // Max 4 CPUs
            //                                  .WithExecutionMode(ParallelExecutionMode.ForceParallelism) // Multithread even when it won't improve performance
            //              where person.City == "Seattle"
            //              select person;
            
            // foreach(var person in result)
            // {
            //     Console.WriteLine(person.Name);
            // }
            // Console.WriteLine("All done.");

            // 1-8
            var result = (from person in peepz.AsParallel()
                                where person.City == "Seattle"
                                orderby (person.Name)   // Order the result
                                select new
                                {
                                    Name = person.Name
                                }).AsSequential().Take(4); // Keep the order and take the first 4
            GCNotificationStatus
            foreach(var person in result)
            {
                Console.WriteLine(person.Name);
            }
            Console.WriteLine("All done.");
        }
    }
}
