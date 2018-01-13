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
            _canvas.ExecuteCommand(fullCommand);
            return true;
        }

        public string GetString()
        {
            return _canvas.RenderToString("");
        }
    }
}
