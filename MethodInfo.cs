using Newtonsoft.Json;
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
        [JsonProperty("name")]
        public string? MethodName { get; set; }

        [JsonProperty("class")]
        public string? ClassName { get; set; }

        [JsonProperty("time")]
        public double ExecutionTime { get; set; }

        [JsonIgnore]
        public Stopwatch Stopwatch { get; set; }

        [JsonProperty("methods")]
        public List<MethodInfo> Methods { get; set; }

        public MethodInfo(string? methodName, string? className)
        {
            MethodName = methodName;
            ClassName = className;
            Methods = new List<MethodInfo>();
            Stopwatch = new Stopwatch();
        }

    }
}
