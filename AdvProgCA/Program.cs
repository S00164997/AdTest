using System;
using System.Threading;

namespace AdvProgCA
{
    class Program
    {

        //1.	Create a static array of 20 integers named with a name of your choice.
        static int[] values = new int[20];
        static Thread OneThousand = new Thread(new ThreadStart(FillArray));
        //4.	Create 2 other threads. Name each thread with a name of your choice e.g.Thread_1 and Thread_2 in the example below
        static Thread Thread_1 = new Thread(new ThreadStart(DisplayArray));
        static Thread Thread_2 = new Thread(new ThreadStart(wasteTime));
        //•	Create a per-thread static field named countAccess
        [ThreadStatic] static int countAccess1 = 0;
        [ThreadStatic] static int countAccess2 = 0;

        static readonly object _object = new object();//used to lock





        static void Main(string[] args)
        {
            //Thread.CurrentThread.Name = "MainThread";

            OneThousand.Name = "Thread to fill array";
            OneThousand.Start();

            Thread_1.Name = "Thread_1 ";
            Thread_1.Start();

            
            Thread_2.Name = "Thread_2 ";
            Thread_2.Start();
            int answer=20;

            
              Console.WriteLine("Enter the number of  elements " + Thread_1.Name.ToString() + " to access(Max 20)");
              answer =Console.Read();

            if (answer>20)
            {
                Console.WriteLine("Enter the number of  elements " + Thread_1.Name.ToString() + " to access(Max 20)");
                answer = Console.Read();
            }
            
            Console.WriteLine("read element"+ answer+"Which is "+values[answer].ToString());
        }

        public static void FillArray()
        {
            Random rnd = new Random();
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = rnd.Next(1, 30);
                Console.WriteLine(values[i].ToString());
            }
            Console.WriteLine(OneThousand.Name + " EXIT");
        }

        public static void DisplayArray()
        {
            try
            {
                lock (_object)
                {



                    //Thread.Sleep(1000);//to wait for array to populate

                    Thread_2.Join();

                    for (int i = 0; i < values.Length; i++)
                    {
                        if (Thread.CurrentThread.Name == Thread_1.Name)
                        {
                            countAccess1++;
                        }
                        else
                        {
                            countAccess2++;
                        }
                        countAccess1++;
                        if (values.Length >= 10)
                        {
                            //Thread_1.Abort();

                            Thread_2.Join();
                        }
                        Console.WriteLine(Thread.CurrentThread.Name + values[i].ToString());
                    }
                    // not working but this is how to kill a thread
                    //if (Thread_1.IsAlive == true)
                    //{

                    //    Thread_1.Abort();
                    //}
                    //if (Thread_2.IsAlive == true)
                    //{

                    //    Thread_2.Abort();
                    //}
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        public static void wasteTime()
        {
            Thread.Sleep(1000);
        }



    }
}
