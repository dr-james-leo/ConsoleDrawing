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

                ReturnCodes retCode = ProcessInputLine(inputString);
                
                if (retCode == ReturnCodes.Stop)
                    break;

                if (retCode == ReturnCodes.Error)
                {
                    Console.WriteLine(_errorString);
                    _errorString = "";
                }
                else
                {
                    if (retCode == ReturnCodes.Usage)
                        Console.WriteLine(GetUsage("\n"));
                }
            }
        }
    }
}
