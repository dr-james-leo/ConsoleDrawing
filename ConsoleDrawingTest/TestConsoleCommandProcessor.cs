using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDrawing;

namespace ConsoleDrawingTest
{
    public class TestConsoleCommandProcessor
    {
        protected Canvas _canvas;

        public TestConsoleCommandProcessor()
        {
            _canvas = new Canvas();
        }

        public bool TestInputLine(string fullCommand)
        {
            try
            {
                _canvas.ExecuteCommand(fullCommand);
                return true;
            }
            catch (CommandException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetString()
        {
            return _canvas.ToString("");
        }
    }
}
