using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   
            GC.WaitForPendingFinalizers();  

            task.Run();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < repetitionCount; i++) task.Run();
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds / repetitionCount;
		}
	}

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            throw new NotImplementedException();
        }
    }
}