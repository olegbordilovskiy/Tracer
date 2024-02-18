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
                var newThread = new ThreadInfo(threadId);
                Threads.Add(newThread);
            }

            StackTrace stackTrace = new StackTrace();
            StackFrame? frame = stackTrace.GetFrame(1);

            string? MethodName = frame?.GetMethod()?.Name;
            string? ClassName = frame?.GetMethod()?.DeclaringType?.Name;

            MethodInfo methodInfo = new MethodInfo(MethodName, ClassName);

            Threads[threadId].Stack.Push(methodInfo);
            Threads[threadId].Methods.Add(methodInfo);

            methodInfo.Stopwatch.Start();

        }
        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

            var methodInfo = threadInfo.Stack.Pop();

            methodInfo.Stopwatch.Stop();

            methodInfo.ExecutionTime = methodInfo.Stopwatch.Elapsed;
        }

        public TraceResult GetTraceResult()
        {
            var ResultThreads = new List<ThreadInfo>();

            foreach (var threadInfo in Threads)
            {
                var time = threadInfo.Methods.Sum(MethodInfo => CalcTotalTime(MethodInfo).Milliseconds);
                ResultThreads.Add(new ThreadInfo(threadInfo.Id, $"{time}ms"));
            }
            return new TraceResult(ResultThreads);


            TimeSpan CalcTotalTime(MethodInfo method)
            {
                TimeSpan time = TimeSpan.Zero;

                if (method.Methods.Count != 0)
                {
                    foreach (var methodInfo in method.Methods)
                    {
                        time += CalcTotalTime(methodInfo);
                    }
                }

                time += method.ExecutionTime;

                return time;
            }
        }
    }
}
