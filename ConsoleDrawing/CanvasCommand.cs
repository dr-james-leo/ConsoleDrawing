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
        
        public CanvasCommand(Canvas iCanvas)
        {
            _canvas = iCanvas;
        }

        abstract public char SupportedCommand { get; }
        abstract public string Usage { get; }
        abstract public bool ProcessCommand(string fullCommand);
        abstract public int NumberOfParameters { get; }

        public string Error
        {
            get { return _errorString; }
        }

        // Checks that an inital canvas has been created and the values of x and y are within the canvas space
        // Return true is successful and false on error
        protected bool UndertakeSanityCheck(int x, int y)
        {          
            int[,] canvasData = _canvas.GetCanvasData();

            if (x < 1 || x > canvasData.GetLength(0))
            {
                _errorString = "x must be between 1 and " + canvasData.GetLength(0) + ".";
                return false;
            }

            if (y < 1 || y > canvasData.GetLength(1))
            {
                _errorString = "y must be between 1 and " + canvasData.GetLength(1) + ".";
                return false;
            }

            return true;
        }

        // Return true if parameters are ok or false if not
        protected bool isNumberOfParametersOK(string fullCommand)
        {
            _errorString = "";

            try
            {
                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                if (elements.Length < NumberOfParameters)
                {
                    _errorString = "Too few parameters. Usage is " + Usage + ".";
                    return false;
                }

                if (elements.Length > NumberOfParameters)
                {
                    _errorString = "Too many parameters. Usage is " + Usage + ".";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }


    }
}
