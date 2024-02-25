﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tracer.Interfaces;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private List<ThreadInfo> Threads;
        private IResultOutput ResultOutput;

        public Tracer(IResultOutput resultOutput)
        {
            Threads = new List<ThreadInfo>();
            ResultOutput = resultOutput;
        }
        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

            if (threadInfo == null)
            {
                threadInfo = new ThreadInfo(threadId);
                Threads.Add(threadInfo);
            }

            StackTrace stackTrace = new StackTrace();
            StackFrame? frame = stackTrace.GetFrame(1);

            string? MethodName = frame?.GetMethod()?.Name;
            string? ClassName = frame?.GetMethod()?.DeclaringType?.Name;

            MethodInfo methodInfo = new MethodInfo(MethodName, ClassName);


            if (threadInfo?.Stack.Count == 0)

                threadInfo.Methods.Add(methodInfo);

            else
                threadInfo?.Stack.Peek().Methods.Add(methodInfo);

            threadInfo?.Stack.Push(methodInfo);
            methodInfo.Stopwatch.Start();

        }
        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            var threadInfo = Threads.FirstOrDefault(t => t.Id == threadId);

            var methodInfo = threadInfo.Stack.Pop();

            methodInfo.Stopwatch.Stop();

            methodInfo.ExecutionTime = methodInfo.Stopwatch.Elapsed.TotalMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            var ResultThreads = new List<ThreadInfo>();

            foreach (var threadInfo in Threads)
            {
                var time = threadInfo.Methods.Sum(MethodInfo => CalcTotalTime(MethodInfo));
                ResultThreads.Add(new ThreadInfo(threadInfo.Id, $"{Math.Round(time)}ms", threadInfo.Methods));
            }
            return new TraceResult(ResultThreads, ResultOutput);


            double CalcTotalTime(MethodInfo method)
            {
                double time = 0;

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
