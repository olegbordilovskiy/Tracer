using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadInfo
    {
        public int Id {  get; set; }
        public string? Time { get; set; }
        private Stopwatch Stopwatch { get; set; }

        public List<MethodInfo> Methods;
        public Stack<MethodInfo> Stack;

        public ThreadInfo(int id)
        {
            Id = id;
            Time = "";
            Methods = new List<MethodInfo>();
            Stack = new Stack<MethodInfo>();

        }
        public ThreadInfo(int id, string time) 
        {
            Id = id;
            Time = time;
            Methods = new List<MethodInfo>();
            Stack = new Stack<MethodInfo>();

        }

    }
}
