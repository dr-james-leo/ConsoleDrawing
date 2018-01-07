using System;
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
            ConsoleCanvas consoleCanvas = new ConsoleCanvas();
            //ConsoleCanvas consoleCanvas = new ConsoleCanvas(' ', '-', '*', '*', 100, 200);
            ConsoleCanvasCommandProcessor myInputProcessor = new ConsoleCanvasCommandProcessor(consoleCanvas);
            myInputProcessor.ProcessInputs();
        }
    }
}
