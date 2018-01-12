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
        abstract public bool ProcessCommand(string fullCommand);
        abstract public int NumberOfParameters { get; }

        public string Error
        {
            get { return _errorString; }
        }

        // Checks the values of x and y are within the canvas space
        // Return true is successful and false on error
        protected bool areXAndYWithinBounds(int x, int y)
        {

            if (x < 1 || x > _canvas.CanvasWidth)
            {
                _errorString = "x must be between 1 and " + _canvas.CanvasWidth + ".";
                return false;
            }

            if (y < 1 || y > _canvas.CanvasHeight)
            {
                _errorString = "y must be between 1 and " + _canvas.CanvasHeight + ".";
                return false;
            }

            return true;
        }

        // Returns parameters as array if ok or null if not
        protected string[] GetParameters(string fullCommand)
        {
            _errorString = "";

            try
            {
                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                if (elements.Length < NumberOfParameters)
                {
                    _errorString = "Too few parameters. Usage is " + Usage + ".";
                    return null;
                }

                if (elements.Length > NumberOfParameters)
                {
                    _errorString = "Too many parameters. Usage is " + Usage + ".";
                    return null;
                }

                return elements;
            }
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return null;
            }
        }
    }
}
