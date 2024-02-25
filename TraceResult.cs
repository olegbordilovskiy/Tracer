﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tracer.Interfaces;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;

namespace Tracer
{
    [XmlRoot("root")]
    public class TraceResult : ISerializer
    {
        private IResultOutput ResultOutput { get; set; }

        [XmlElement("thread")]
        public List<ThreadInfo> Threads { get; set; }
        public TraceResult(List<ThreadInfo> threadsInfo, IResultOutput resultOutput)
        {
            Threads = threadsInfo;
            ResultOutput = resultOutput;
        }

        public TraceResult() { }

        public string SerializeToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string SerializeToXML()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "   "
            };

            using (StringWriter sw = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                xmlSerializer.Serialize(writer, this, namespaces);
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
