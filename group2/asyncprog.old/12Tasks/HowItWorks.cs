﻿namespace _12Tasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class HowItWorks
    {
        public static void Run()
        {
            var task = CallCount();
            task.Wait();

            Console.WriteLine($"finish on main thread [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static async Task CallCount()
        {
            var task = new Task(Count);

            task.Start();

            for (var i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Writing from callCount loop: {i}, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
            }

            Console.WriteLine($"just before await, [thread id: {Thread.CurrentThread.ManagedThreadId}]");

            await task;

            Console.WriteLine($"callCount completed, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
        }

        private static void Count()
        {
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(2000);

                Console.WriteLine($"count loop: {i}, [thread id: {Thread.CurrentThread.ManagedThreadId}]");
            }
        }
    }
}