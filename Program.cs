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
        private Person [] peepz = new Person [] {
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
                new Person { Name = "Murder Manfred" },
            };

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

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished.");
        }

        public static int CalculateResult()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("All done");
            return 99;
        }

        public static void DoMoreWork(int taskNumber)
        {
            Console.WriteLine($"Task {taskNumber} starting");
            Thread.Sleep(2000);
            Console.WriteLine($"Task {taskNumber} finished");
        }

        public static void FooTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Foo");
        }

        public static void BarTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Bar");
        }

        public static void DoChild(object state)
        {
            Console.WriteLine($"Child {state} starting");
            Thread.Sleep(2000);
            Console.WriteLine($"Child {state} finished");
        }

        public static void ThreadHello()
        {
            Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        public static void WorkOnData(object data)
        {
            Console.WriteLine($"Working on {data}");
            Thread.Sleep(1000);
        }

        public static void DisplayThread(Thread t)
        {
            Console.WriteLine($"Name: {t.Name}");
            Console.WriteLine($"Culture: {t.}");
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
            // var result = (from person in peepz.AsParallel()
            //                     where person.City == "Seattle"
            //                     orderby (person.Name)   // Order the result
            //                     select new
            //                     {
            //                         Name = person.Name
            //                     }).AsSequential().Take(4); // Keep the order and take the first 4

            // foreach(var person in result)
            // {
            //     Console.WriteLine(person.Name);
            // }
            // Console.WriteLine("All done.");

            // 1-9 PLINQ
            // var result = from person in peepz.AsParallel()
            //              where person.City == "Seattle"
            //              select person;
            // result.ForAll(person => Console.WriteLine(person.Name));

            // 1-10 Exceptions in PLINQ
            // try
            // {
            //     var result = from person in peepz.AsParallel()
            //                  where person.City == "Seattle"
            //                  select person;
            //     result.ForAll(person => Console.WriteLine(person.Name));
            // }
            // catch (AggregateException e)
            // {
            //     Console.WriteLine(e.InnerExceptions.Count + " exceptions");
            // }

            // 1-11 Create a task (corrected)
            // Task newTask = new Task(DoWork);
            // newTask.Start();
            // newTask.Wait();

            // 1-12 Run a task
            // Task newTask = Task.Run(() => DoWork()); // Apparently, you need the stupid layer of lambda here
            // newTask.Wait();

            // 1-13 Task returning a value (corrected)
            // Task<int> task = Task.Run(() => CalculateResult());
            // Console.WriteLine(task.Result);

            // 1-14 Await all
            // Task[] Tasks = new Task[10];
            // var numberSeries = Enumerable.Range(0, 10);
            // foreach(int number in numberSeries)
            // {
            //     Tasks[number] = Task.Run(() => DoMoreWork(number));
            // }
            // Task.WaitAll(Tasks);

            // 1-15 Continuation
            // Task task = Task.Run(() => FooTask());
            // task.ContinueWith((prevTask) => BarTask());
            // Console.ReadKey();

            // 1-16 Continuation options
            // Task task = Task.Run(() => FooTask());
            // task.ContinueWith((prevTask) => BarTask(), TaskContinuationOptions.OnlyOnRanToCompletion);
            // Console.ReadKey();

            // 1-17 Attached child task
            // var parentTask = Task.Factory.StartNew(() => {
            //     Console.WriteLine("Parent starts.");

            //     var range = Enumerable.Range(0, 10);
            //     foreach(int number in range)
            //     {
            //         Task.Factory.StartNew(
            //             (item) => DoChild(item),
            //             number,
            //             TaskCreationOptions.AttachedToParent);
            //     }
            // });

            // parentTask.Wait();
            // Console.WriteLine("Parent finished.");

            // 1-18 Creating threads
            // Thread newThread = new Thread(ThreadHello);
            // newThread.Start();

            // 1-19 Using ThreadStart
            // ThreadStart ts = new ThreadStart(ThreadHello);
            // Thread newThread = new Thread(ts);
            // newThread.Start();

            // 1-20 Threads and lambda expressions
            // Thread newThread = new Thread(() => 
            // {
            //     Console.WriteLine("Hello from the lambda thread");
            //     Thread.Sleep(2000);
            // });
            // newThread.Start();

            // 1-21 ParametrizedThreadStart
            // ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            // Thread thread = new Thread(ps);
            // thread.Start("Wololo");

            // 1-22 Thread with lambda parameter
            // Thread newThread = new Thread((data) =>
            // {
            //     Console.WriteLine($"Working on {data}");
            //     Thread.Sleep(1000);
            // });
            // newThread.Start("Foobar");

            // 1-23 Aborting a thread
            // Thread tickTock = new Thread(() =>
            // {
            //     while(true)
            //     {
            //         Console.WriteLine("Tick");
            //         Thread.Sleep(1000);
            //         Console.WriteLine("Tock");
            //         Thread.Sleep(1000);
            //     }
            // });
            // tickTock.Start();

            // Console.WriteLine("Press a key to stop the thread");
            // Console.ReadKey();
            // tickTock.Abort();
            // Console.WriteLine("Killed it...");

            // 1-24 Aborting using a shared flag variable
            // bool tickTockRunning = true;

            // Thread tickTock = new Thread(() =>
            // {
            //     while(tickTockRunning)
            //     {
            //         Console.WriteLine("Tick");
            //         Thread.Sleep(1000);
            //         Console.WriteLine("Tock");
            //         Thread.Sleep(1000);
            //     }
            // });
            // tickTock.Start();

            // Console.WriteLine("Press a key to stop the thread");
            // Console.ReadKey();
            // tickTockRunning = false;
            // Console.WriteLine("Killing it...");

            // 1-25 Using join
            // Thread threadToWaitFor = new Thread(() =>
            // {
            //     Console.WriteLine("Thread starting");
            //     Thread.Sleep(2000);
            //     Console.WriteLine("Thread done");
            // });
            // threadToWaitFor.Start();
            // Console.WriteLine("Joining thread");
            // threadToWaitFor.Join(); // What am I joining it with. Seems useless...
            // Console.WriteLine("Press a key to exit");
            // Console.ReadKey();

            // 1-26 ThreadLocal
            // ThreadLocal<Random> RandomGenerator =
            // new ThreadLocal<Random>(() =>
            // {
            //     return new Random(2);
            // });

            // Thread t1 = new Thread(() =>
            // {
            //     var range = Enumerable.Range(0, 5);

            //     foreach(int nr in range)
            //     {
            //         int randomNumber = RandomGenerator.Value.Next(10);
            //         Console.WriteLine($"T1: {randomNumber}");
            //         Thread.Sleep(500);
            //     }
            // });

            // Thread t2 = new Thread(() =>
            // {
            //     var range = Enumerable.Range(0, 5);
                
            //     foreach(int nr in range)
            //     {
            //         int randomNumber = RandomGenerator.Value.Next(10);
            //         Console.WriteLine($"T2: {randomNumber}");
            //         Thread.Sleep(500);
            //     }
            // });

            // t1.Start();
            // t2.Start();
            // Console.ReadKey();

            // 1-27 Thread context

        }
    }
}
