using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodInfo
    {
        public string? MethodName { get; set; }
        public string? ClassName { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public Stopwatch Stopwatch { get; set; }

        public List<MethodInfo> Methods { get; set; }

        public MethodInfo(string? methodName, string? className)
        {
            MethodName = methodName;
            ClassName = className;
            Methods = new List<MethodInfo>();
        }

    }
}
