﻿namespace _01ThreadingDemos
{
    using System;
    using System.Threading;

    public class Demo03
    {
        public static void ThreadProc()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);

                // Yield the rest of the time slice.
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            }
        }

        public static void Run()
        {
            Thread t = new Thread(new ThreadStart(ThreadProc));
            //t.IsBackground = true;
            t.Start();
        }
    }
}