using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Interfaces
{
    public interface IResultOutput
    {
        void ConsoleOutput(string result);
        void FileOutput(string result, string path);
    }
}
