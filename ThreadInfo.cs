using Newtonsoft.Json;
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
        [JsonProperty("id", Order = 1)]
        public int Id {  get; set; }

        [JsonProperty("time", Order = 2)]
        public string? Time { get; set; }

        [JsonProperty("methods", Order = 3)]
        public List<MethodInfo> Methods;

        [JsonIgnore]
        public Stack<MethodInfo> Stack;

        public ThreadInfo(int id)
        {
            Id = id;
            Time = "";
            Methods = new List<MethodInfo>();
            Stack = new Stack<MethodInfo>();

        }
        public ThreadInfo(int id, string time, List<MethodInfo> methods) 
        {
            Id = id;
            Time = time;
            Methods = methods;
            Stack = new Stack<MethodInfo>();

        }

    }
}
