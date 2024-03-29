﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer.Interfaces;

namespace Tracer
{
    public class ResultOutput : IResultOutput
    {
        public void ConsoleOutput(string result)
        {
            Console.WriteLine(result);
        }

        public void FileOutput(string result, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteAsync(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
