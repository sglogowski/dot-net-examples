using System.Diagnostics;

namespace DotNetExamples.PerformanceCounters
{
    public class PerformanceCounterExample
    {
        const string PerfCounterCategoryName = "DotNetExamples.PerformanceCounters.PerformanceCounterExample";

        const string PerfCounterTotalOperationsExecutedCounterName = "total operations executed";
        const string PerfCounterOperationsPerSecondCounterName = "operations per second";
        const string PerfCounterAverageTimePerOperationCounterName = "average time per operation";

        public static void Init()
        {
            // Check if existing performance counter category definition for given category name.
            var exists = PerformanceCounterCategory.Exists(PerfCounterCategoryName);
            if (exists)
            {
                // Deleting performance counter category definitions for given category name.
                PerformanceCounterCategory.Delete(PerfCounterCategoryName);
            }

            var counters = new CounterCreationDataCollection
            {
                new CounterCreationData()
                {
                    CounterName = PerfCounterTotalOperationsExecutedCounterName,
                    CounterHelp = "Total number of operations executed",
                    CounterType = PerformanceCounterType.NumberOfItems32
                    // How many times occured since create.
                },

                new CounterCreationData()
                {
                    CounterName = PerfCounterOperationsPerSecondCounterName,
                    CounterHelp = "Number of operations executed per second",
                    CounterType = PerformanceCounterType.RateOfCountsPerSecond32
                    // How many times occured per second.
                },

                new CounterCreationData()
                {
                    CounterName = PerfCounterAverageTimePerOperationCounterName,
                    CounterHelp = "Average duration per operation execution",
                    CounterType = PerformanceCounterType.AverageTimer32
                    // How long the average operations last.
                },

                new CounterCreationData()
                {
                    CounterName = $"{ PerfCounterAverageTimePerOperationCounterName } base",
                    CounterHelp = "Average duration per operation execution base",
                    CounterType = PerformanceCounterType.AverageBase
                    // Required for PerfCounterAverageTimePerOperationCounterName calculation, increament after AverageTimer32 
                }
            };

            // Creating performance counter category definitions for given category name with given defined counters.
            PerformanceCounterCategory.Create(
                categoryName: PerfCounterCategoryName,
                categoryHelp: "Sample category for Codeproject",
                categoryType: PerformanceCounterCategoryType.Unknown,
                counterData: counters
            );
        }

        /// <summary>
        /// Increment TotalOperationsExecuted performance counter.
        /// </summary>
        /// <param name="value">How many times operations occurred</param>
        public static void IncrementOperationsExecutedCounter(long value = 1)
        {
            var totalOperationsExecuted = new PerformanceCounter
            {
                CategoryName = PerfCounterCategoryName,
                CounterName = PerfCounterTotalOperationsExecutedCounterName,
                ReadOnly = false
            };

            totalOperationsExecuted.IncrementBy(value);
        }

        /// <summary>
        /// Increment OperationsPerSecond performance counter.
        /// </summary>
        /// <param name="value">How many times operations occurred</param>
        public static void IncrementOperationsPerSecondCounter(long value = 1)
        {
            var operationsPerSecond = new PerformanceCounter
            {
                CategoryName = PerfCounterCategoryName,
                CounterName = PerfCounterOperationsPerSecondCounterName,
                ReadOnly = false
            };

            operationsPerSecond.IncrementBy(value);
        }

        /// <summary>
        /// Increment AverageTimePerOperation performance counter.
        /// </summary>
        /// <param name="ellapsedTicks">How long all operations take, eg. Stopwatch.EllapsedTicks</param>
        /// <param name="value">How many times operations occurred</param>
        public static void IncrementAverageTimePerOperationCounter(long ellapsedTicks = 1, long value = 1)
        {
            var averageTimePerOperation = new PerformanceCounter
            {
                CategoryName = PerfCounterCategoryName,
                CounterName = PerfCounterAverageTimePerOperationCounterName,
                ReadOnly = false
            };

            var averageTimePerOperationBase = new PerformanceCounter
            {
                CategoryName = PerfCounterCategoryName,
                CounterName = $"{PerfCounterAverageTimePerOperationCounterName} base",
                ReadOnly = false
            };

            averageTimePerOperation.IncrementBy(ellapsedTicks);
            averageTimePerOperationBase.IncrementBy(value);
        }
    }
}
