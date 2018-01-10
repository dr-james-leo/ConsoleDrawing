using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDrawing;


namespace ConsoleDrawingTest
{
    class TestConsoleCommandProcessor : ConsoleCanvasCommandProcessor
    {
        public bool TestInputLine(string fullCommand)
        {
            return _canvas.ExecuteCommand(fullCommand);
        }

        public string GetString()
        {
            return _canvas.RenderToString("");
        }
    }
}
