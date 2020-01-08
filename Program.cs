using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.Concurrent;

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
            Console.WriteLine($"Culture: {t.CurrentCulture}");
            Console.WriteLine($"Priority: {t.Priority}");
            Console.WriteLine($"Context: {t.ExecutionContext}");
            Console.WriteLine($"Is background: {t.IsBackground}");
            Console.WriteLine($"Is pool: {t.IsThreadPoolThread}");
        }

        public static async Task<string> FetchWebPage(string url)
        {
            HttpClient httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }

        public static List<int[]> sliceArray(List<int> toSlice, int sliceSize)  // Omnisharp kep bitching about types when I tried to make the toSlice array type agnostic :(
        {
            List<int[]> slices = new List<int[]>();
            var toSliceArray = toSlice.ToArray();
            int slicesAmount = (toSlice.Count() / sliceSize) + 1;

            for (int i = 0; i < slicesAmount; i++)
            {
                int sliceEnd = (i + 1) * sliceSize;
                int sliceStart = sliceEnd - sliceSize;
                
                try
                {
                    var newSlice = toSliceArray[sliceStart..sliceEnd];
                    slices.Add(newSlice);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    sliceEnd = toSliceArray.Count();
                    var newSlice =  toSliceArray[sliceStart..sliceEnd];
                    slices.Add(newSlice);
                }
            }
            return slices;
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
            // Thread.CurrentThread.Name = "Main method";
            // DisplayThread(Thread.CurrentThread);

            // 1-28 Thread pool
            // Action<object> doWork = (object state) =>
            // {
            //     Console.WriteLine($"Wololo: {state}");
            // };

            // var range = Enumerable.Range(0, 50);
            // foreach(int stateNr in range)
            // {
            //     ThreadPool.QueueUserWorkItem(state => doWork(stateNr));
            // }
            // Console.ReadKey();

            // 1-34 Awaiting parallel tasks (modified for console application)
            // Func<string [], Task<IEnumerable<string>>> fetchWebPages = async (string [] urls) =>
            // {
            //     var tasks = new List<Task<string>>();

            //     foreach(string url in urls)
            //     {
            //         Task<string> newTask = FetchWebPage(url);
            //         tasks.Add(newTask);
            //     }

            //     return await Task.WhenAll(tasks);   // Wait until all tasks are completed
            // };
            
            // var webPages = new string[] {
            //     "https://tweakers.net/",
            //     "https://www.nu.nl/"
            // };
            // var results = fetchWebPages(webPages).Result;

            // foreach(string result in results)
            // {
            //     Console.WriteLine(result);
            // }

            // 1-35/36 Using BlockingCollection
            // A collection that can only hold 5 items.
            // BlockingCollection<int> data = new BlockingCollection<int>(new ConcurrentStack<int>(), 5);

            // Task.Run(() =>
            // {
            //     // Attempt to add 10 items to the collection. Blocks after the 5th.
            //     var range = Enumerable.Range(0, 10);

            //     foreach(int nr in range)
            //     {
            //         data.Add(nr);
            //         Console.WriteLine($"Data {nr} has been added to collection.");
            //     }
            //     // When all 10 are added, indicate there is nothing more to add
            //     data.CompleteAdding();
            // });

            // Console.ReadKey();
            // Console.WriteLine("Reading collection");

            // Task.Run(() =>
            // {
            //     // Try to take as long as CompleteAdding hasn't been called
            //     while (!data.IsCompleted)
            //     {
            //         try
            //         {
            //             int v = data.Take();
            //             Console.WriteLine($"Data {v} has been taken from collection.");
            //         }
            //         catch (InvalidOperationException) {}
            //     }
            // });
            // Console.ReadKey();

            // 1-37 Concurrent queue
            // ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
            // queue.Enqueue("Rob");
            // queue.Enqueue("Miles");
            // string str;

            // if (queue.TryPeek(out str))
            // {
            //     Console.WriteLine($"Peek: {str}");
            // }
            // if (queue.TryDequeue(out str))
            // {
            //     Console.WriteLine($"Peek: {str}");
            // }

            // 1-37 Concurrent stack
            // ConcurrentStack<string> queue = new ConcurrentStack<string>();
            // queue.Push("Rob");
            // queue.Push("Miles");
            // string str;

            // if (queue.TryPeek(out str))
            // {
            //     Console.WriteLine($"Peek: {str}");
            // }
            // if (queue.TryPop(out str))
            // {
            //     Console.WriteLine($"Pop: {str}");
            // }

            // 1-38 Concurrent bag
            // ConcurrentBag<string> bag = new ConcurrentBag<string>();
            // bag.Add("Rob");
            // bag.Add("Miles");
            // bag.Add("Hull");
            // string str;

            // if (bag.TryPeek(out str))
            // {
            //     Console.WriteLine($"peek: {str}");
            // }
            // if (bag.TryTake(out str))
            // {
            //     Console.WriteLine($"Take: {str}");
            // }

            // 1-40 Concurrent dictionary
            // Adding Rob to dictionary
            // ConcurrentDictionary<string, int> ages = new ConcurrentDictionary<string, int>();
            // if (ages.TryAdd("Rob", 21)) Console.WriteLine("Rob added succesfully.");
            // Console.WriteLine("Rob's age is {0}", ages["Rob"]);

            // // Set age to 22 if age is 21
            // if (ages.TryUpdate("Rob", 22, 21))  Console.WriteLine("Age updated succesfully");
            // Console.WriteLine("Rob's new age is {0}", ages["Rob"]);

            // // Increment age atomically using factory methode
            // Console.WriteLine(  "Rob's age udpated to {0}",
            //                     ages.AddOrUpdate("Rob", 1, (name, age) => age += 1));   // Set age to 1 if Rob doesn't exist yet
            // Console.WriteLine("Rob's new age is {0}", ages["Rob"]);

            // 1-41 Single task summing
            // int[] items = Enumerable.Range(0, 50000001).ToArray();

            // long total = 0;

            // foreach(int item in items)
            // {
            //     total += item;
            // }
            // Console.WriteLine($"Total is {total}");

            // 1-42 Bad task interaction - lost train of thought, the nex day. Not very important though
            // List<int> items = Enumerable.Range(0, 50000002).ToList();
            
            // int rangeSize = 1000;
            // List<int[]> itemsSliced = sliceArray(items, rangeSize);
            
            // List<Task> tasks = new List<Task>();
            // long sharedTotal = 0;

            // Action<int, int> addRangeOfValues = (int start, int end) =>
            // {
            //     while (start < end)
            //     {
            //         sharedTotal += items[start];
            //     }
            // };

            // 1-44 Using locking (43 was useless)
            // List<int> bigRange = Enumerable.Range(0, 50000001).ToList();
            // int smallRangeSize = 1000;

            // var smallRanges = sliceArray(bigRange, smallRangeSize);

            // long sharedTotal = 0;
            // object sharedTotalLock = new object();

            // Func<List<int>, long> addRangeOfValues = (List<int> range) =>
            // {
            //     long subTotal = range.Aggregate((acc, x) => acc + x);
            //     return subTotal;
            // };

            // List<Task> tasks = new List<Task>();

            // 1-48: Interlock total
            List<int> items = Enumerable.Range(0, 50000).ToList();
            long sharedTotal = 0;

            Action<int, int> addRangeOfValues = (int start, int end) =>
            {
                long subTotal = 0;

                while (start < end)
                {
                    subTotal += items[start];
                    start++;
                }

                Interlocked.Add(ref sharedTotal, subTotal);
            };
        }
    }
}
