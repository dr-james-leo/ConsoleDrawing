using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCanvasCommandProcessor inputProcessor = new ConsoleCanvasCommandProcessor();
            inputProcessor.ProcessInputs();
        }
    }
}
