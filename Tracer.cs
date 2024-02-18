using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tracer.Interfaces;

namespace Tracer
{
    internal class Tracer : ITracer
    {
        private List<ThreadInfo> Threads;
        //private Stopwatch Stopwatch { get; set; }
        //private TimeSpan ElapsedTime { get; set; }
        //private TraceResult TraceResult { get; set; }
        public Tracer()
        {
            Threads = new List<ThreadInfo>();
        }
        public void StartTrace()
        {

            int threadId = Thread.CurrentThread.ManagedThreadId;

            var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

            if (threadInfo == null)
            {
                var newThread = new ThreadInfo (threadId);
                Threads.Add(newThread);
            }

            StackTrace stackTrace = new StackTrace();
            StackFrame? frame = stackTrace.GetFrame(1);

            string? MethodName = frame?.GetMethod()?.Name;
            string? ClassName = frame?.GetMethod()?.DeclaringType?.Name;

            MethodInfo methodInfo = new MethodInfo (MethodName, ClassName);

            Threads[threadId].Stack.Push(methodInfo);
            Threads[threadId].Methods.Add(methodInfo);

            methodInfo.Stopwatch.Start();

        }
        public void StopTrace()
        {
            //Stopwatch.Stop();

            //TraceResult.ExecutionTime = Stopwatch.Elapsed.TotalMilliseconds.ToString();

        }

        void GetInformation()
        {

        }

        public TraceResult GetTraceResult() 
        {
            return new TraceResult();
        }

    }
}
