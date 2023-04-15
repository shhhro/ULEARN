using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface ITaskFactory
    {
        ITask CreateTaskForClasses(int size);
        ITask CreateTaskForStructure(int size);
    }

    public class ArrayCreationTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForClasses(int size)
        {
            return new ClassArrayCreationTask(size);
        }

        public ITask CreateTaskForStructure(int size)
        {
            return new StructArrayCreationTask(size);
        }
    }

    public class MethodCallTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForClasses(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }

        public ITask CreateTaskForStructure(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }
    }

    public class Experiments
    {
        public static ChartData BuildCharts(string title, IBenchmark benchmark, int repetitionsCount, ITaskFactory taskFactory)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var size in Constants.FieldCounts)
            {
                var taskClasses = taskFactory.CreateTaskForClasses(size);
                var taskStructure = taskFactory.CreateTaskForStructure(size);
                classesTimes.Add(new ExperimentResult(size, benchmark.MeasureDurationInMs((ITask)taskClasses, repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(size, benchmark.MeasureDurationInMs((ITask)taskStructure, repetitionsCount)));
            }

            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            return BuildCharts("Array creation", benchmark, repetitionsCount, new ArrayCreationTaskFactory());
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            return BuildCharts("Method call", benchmark, repetitionsCount, new MethodCallTaskFactory());
        }
    }
}