using System;
using System.Diagnostics;

namespace DotNetExamples.PerformanceCounters
{
    public class Program
    {
        static void Main(string[] args)
        {
            //PerformanceCounterExample.Init();
            var watch = new Stopwatch();

            while (true)
            {
                Console.WriteLine("Start processing");
                watch.Start();

                var operations = 0;

                for (
                    var pressedKey = ConsoleKey.Escape;
                    pressedKey != ConsoleKey.Spacebar; 
                    pressedKey = Console.ReadKey().Key
                )
                {
                    Console.WriteLine($"Not Spacebar, increments operations counter: {++operations}");
                }

                watch.Stop();
                Console.WriteLine("Stop processing");

                PerformanceCounterExample.IncrementOperationsExecutedCounter(operations);
                PerformanceCounterExample.IncrementOperationsPerSecondCounter(operations);
                PerformanceCounterExample.IncrementAverageTimePerOperationCounter(watch.ElapsedTicks, operations);
            }
        }
    }
}
