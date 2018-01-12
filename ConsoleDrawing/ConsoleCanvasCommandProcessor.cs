using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    public class ConsoleCanvasCommandProcessor
    {
        protected Canvas _canvas;

        public ConsoleCanvasCommandProcessor()
        {
            _canvas = new Canvas();
            CreateCanvasCommand createCanvasCommand = new CreateCanvasCommand();
            _canvas.AddNewCommand(createCanvasCommand);

            LineCanvasCommand lineCanvasCommand = new LineCanvasCommand();
            _canvas.AddNewCommand(lineCanvasCommand);

            RectangleCanvasCommand rectangleCanvasCommand = new RectangleCanvasCommand();
            _canvas.AddNewCommand(rectangleCanvasCommand);

            FillCanvasCommand fillCanvasCommand = new FillCanvasCommand();
            _canvas.AddNewCommand(fillCanvasCommand);
        }

        public void ProcessInputs()
        {
            while (true)
            {
                Console.Write("\nenter command: ");

                string inputString = Console.ReadLine();

                if (!ProcessInputLine(inputString))
                    break;               
            }
        }

        // Returns true to continue and false to stop
        private bool ProcessInputLine(string fullCommand)
        {            
            try
            {
                fullCommand = fullCommand.Trim();

                if (fullCommand.Length == 0)
                {
                    Console.WriteLine("Please enter a command.");
                    return true;
                }

                char mainCommand = char.ToUpper(fullCommand.First<char>());

                switch (mainCommand)
                {
                    case 'Q':
                        return false;

                    case '?':
                        Console.WriteLine(_canvas.GetUsage("\n"));
                        break;

                    default:
                        if (_canvas.ExecuteCommand(fullCommand))
                        {
                            string displayString = _canvas.RenderToString("\n");
                            if (displayString.Length > 0)
                                Console.WriteLine(displayString);
                            else
                                Console.WriteLine(_canvas.Error);
                        }
                        else
                        {
                            Console.WriteLine(_canvas.Error);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error ocurred: " + ex.Message);
            }

            return true;
        }
    }
}
