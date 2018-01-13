using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    abstract public class CanvasCommand
    {
        protected string _errorString = "";
        protected Canvas _canvas;
        
        public void SetCanvas(Canvas canvas)
        {
            _canvas = canvas;
        }

        abstract public char SupportedCommand { get; }
        abstract public string Usage { get; }
        abstract public void ProcessCommand(string fullCommand);
        abstract public int NumberOfParameters { get; }

        public string Error
        {
            get { return _errorString; }
        }

        // Checks the values of x and y are within the canvas space
        protected void CheckXAndYWithinBounds(int x, int y)
        {
            if (x < 1 || x > _canvas.CanvasWidth)
                throw new CommandException("x must be between 1 and " + _canvas.CanvasWidth + ".");

            if (y < 1 || y > _canvas.CanvasHeight)
                throw new CommandException("y must be between 1 and " + _canvas.CanvasHeight + ".");
        }

        // Returns parameters as array if ok or null if not
        protected string[] GetParameters(string fullCommand)
        {
            string pattern = @"\s+";
            string[] elements = Regex.Split(fullCommand, pattern);

            if (elements.Length < NumberOfParameters)
                throw new CommandException("Too few parameters. Usage is " + Usage + ".");
                
            if (elements.Length > NumberOfParameters)
                throw new CommandException("Too many parameters. Usage is " + Usage + ".");
               
            return elements;           
        }
    }
}
