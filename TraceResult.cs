using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tracer.Interfaces;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Tracer
{
    public class TraceResult : ISerializer
    {
        private IResultOutput ResultOutput { get; set; }

        public List<ThreadInfo> Threads { get; set; }
        public TraceResult(List<ThreadInfo> threadsInfo, IResultOutput resultOutput)
        {
            Threads = threadsInfo;
            ResultOutput = resultOutput;
        }

        public string SerializeToJSON()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public string SerializeToXML()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, this);
                return sw.ToString();
            }
        }

        public void OutputToConsole()
        {
            ResultOutput.ConsoleOutput(SerializeToJSON()); 
        }

        public async Task OutputToFile(string path)
        {
            await ResultOutput.FileOutput(SerializeToJSON(), path); 
        }

    }
}
