using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.

            task.Run();
            var stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            for (var i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }

    public class StringConstructor : ITask
    {
        public string Str { get; set; }

        public void Run()
        {
            Str = new string('a', 10000);
        }
    }

    public class StringBuilderTask : ITask
    {
        public string Str { set; get; }

        public void Run()
        {
            var strBuilder = new StringBuilder();
            for (var i = 0; i < 10000; i++)
            {
                strBuilder.Append("a");
            }
            Str = strBuilder.ToString();
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var timer = new Benchmark();
            var repetitionCount = 10000;
            var strByConstructor = new StringConstructor();
            var strByBuilder = new StringBuilderTask();

            Assert.Less(timer.MeasureDurationInMs(strByConstructor, repetitionCount),
                        timer.MeasureDurationInMs(strByBuilder, repetitionCount));
        }
    }
}