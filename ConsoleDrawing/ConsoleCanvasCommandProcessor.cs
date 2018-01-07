using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    public class ConsoleCanvasCommandProcessor: CanvasCommandProcessor
    {
        public ConsoleCanvasCommandProcessor(Canvas canvas): base(canvas)
        {
        }

        public override void ProcessInputs()
        {
            while (true)
            {
                Console.Write("\nenter command: ");

                string inputString = Console.ReadLine();

                int retCode = ProcessInputLine(inputString);
                if (retCode == 0)
                    break;

                if (retCode == -1)
                {
                    Console.WriteLine(_errorString);
                    _errorString = "";
                }
            }
        }
    }
}
