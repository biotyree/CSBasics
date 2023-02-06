using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace StructBenchmarking
{
    public interface IChartDataBuilder
    {
        ChartData BuildChartData(IBenchmark benchmark, int repetitionsCount);
    }

    public class BuildChartData
    {
        public static ChartData GetChartData(IChartDataBuilder builder, IBenchmark benchmark, int repetitionsCount)
        {
            return builder.BuildChartData(benchmark, repetitionsCount);
        }
    }

    public class BuilderChartDataForMethodCall : IChartDataBuilder
    {
        public ChartData BuildChartData(IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var size in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(size), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(size), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }

    public class BuilderChartDataForArrayCreation : IChartDataBuilder
    {
        public ChartData BuildChartData(IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var size in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(new ClassArrayCreationTask(size), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(size,
                    benchmark.MeasureDurationInMs(new StructArrayCreationTask(size), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
            => BuildChartData.GetChartData(new BuilderChartDataForArrayCreation(), benchmark, repetitionsCount);

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount)
            => BuildChartData.GetChartData(new BuilderChartDataForMethodCall(), benchmark, repetitionsCount);
    }
}