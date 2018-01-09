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
            ConsoleCanvas consoleCanvas = new ConsoleCanvas();
            //ConsoleCanvas consoleCanvas = new ConsoleCanvas(' ', '-', '*', '*', 100, 200);

            //Hashtable commandMap = new Hashtable();

            //CanvasCommand createCanvasCommand = new CreateCanvasCommand();
            //commandMap.Add(createCanvasCommand.SupportedCommand(), createCanvasCommand);

            //CanvasCommand lineCanvasCommand = new LineCanvasCommand();
            //commandMap.Add(lineCanvasCommand.SupportedCommand(), lineCanvasCommand);

            //CanvasCommand rectangleCanvasCommand = new RectangleCanvasCommand();
            //commandMap.Add(rectangleCanvasCommand.SupportedCommand(), rectangleCanvasCommand);

            //CanvasCommand fillCanvasCommand = new FillCanvasCommand();
            //commandMap.Add(fillCanvasCommand.SupportedCommand(), fillCanvasCommand);

            //CanvasCommand displayCanvasCommand = new DisplayCanvasCommand();
            //commandMap.Add(displayCanvasCommand.SupportedCommand(), displayCanvasCommand);



            ConsoleCanvasCommandProcessor myInputProcessor = new ConsoleCanvasCommandProcessor(consoleCanvas);
            myInputProcessor.ProcessInputs();
        }
    }
}
