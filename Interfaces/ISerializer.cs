using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Interfaces
{
    public interface ISerializer
    {
        string SerializeToJSON();
        string SerializeToXML();
    }
}
