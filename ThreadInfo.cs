using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class ThreadInfo
    {
        public int Id {  get; set; }
        public string? Time { get; set; }
        private Stopwatch Stopwatch { get; set; }

        public List<MethodInfo> Methods;
        public Stack<MethodInfo> Stack;
        public ThreadInfo(int id) 
        {
            Id = id;
            Methods = new List<MethodInfo>();
            Stack = new Stack<MethodInfo>();

        }
    }
}
