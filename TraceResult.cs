using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        List<ThreadInfo> Threads { get; set; }
        public TraceResult(List<ThreadInfo> threadsInfo) 
        {
            Threads = threadsInfo;
        }
    }
}
