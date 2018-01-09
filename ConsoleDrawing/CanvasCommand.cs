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
        //protected int[,] _canvasData; // Holds the data specifying the drawings on the canvas
        //protected Hashtable _colourMap; // maps colours to integers for filling the canvas

        //// Parameters with defaults
        //protected char _spaceChar = ' ';
        //protected char _lineChar = 'x';
        //protected char _topAndBottomEdgeChar = '-';
        //protected char _leftAndRightEdgeChar = '|';
        //protected int _maxWidth = 1000;
        //protected int _maxHeight = 1000;

        //protected int _width;
        //protected int _height;

        //protected bool hasCanvasBeenCreated = false;

        public CanvasCommand(Canvas iCanvas)
        {
            _canvas = iCanvas;
        }

        abstract public char SupportedCommand();
        abstract public string GetUsage();
        abstract public bool ProcessCommand(string fullCommand);
        abstract public int GetNumberOfParameters();

        public string Error
        {
            get { return _errorString; }
        }

        //// Returns an empty string on error and sets _errorString;
        //public string RenderToString(string newLineChar)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (!hasCanvasBeenCreated)
        //        {
        //            _errorString = "Please create a canvas first.";
        //            return "";
        //        }

        //        StringBuilder displayString = new StringBuilder("");

        //        // Render the top line
        //        for (int i = 0; i < _width + 2; i++)
        //            displayString.Append(_topAndBottomEdgeChar);

        //        displayString.Append(newLineChar);

        //        for (int j = 0; j < _height; j++)
        //        {
        //            displayString.Append(_leftAndRightEdgeChar);

        //            for (int i = 0; i < _width; i++)
        //            {
        //                switch (_canvasData[i, j])
        //                {
        //                    case 0:
        //                        displayString.Append(_spaceChar);
        //                        break;

        //                    case 1:
        //                        displayString.Append(_lineChar);
        //                        break;

        //                    default:
        //                        string colour = (string)_colourMap[_canvasData[i, j]];
        //                        displayString.Append(colour);
        //                        break;
        //                }
        //            }

        //            displayString.Append(_leftAndRightEdgeChar);
        //            displayString.Append(newLineChar);
        //        }

        //        // Render the bottom line
        //        for (int i = 0; i < _width + 2; i++)
        //            displayString.Append(_topAndBottomEdgeChar);

        //        return displayString.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return "";
        //    }
        //}

        // Checks that an inital canvas has been created and the values of x and y are within the canvas space
        // Return true is successful and false on error
        protected bool UndertakeSanityCheck(int x, int y)
        {
            //if (!hasCanvasBeenCreated)
            //{
            //    _errorString = "Please create a canvas first.";
            //    return false;
            //}

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

                if (elements.Length < GetNumberOfParameters())
                {
                    _errorString = "Too few parameters. Usage is " + GetUsage() + ".";
                    return false;
                }

                if (elements.Length > GetNumberOfParameters())
                {
                    _errorString = "Too many parameters. Usage is " + GetUsage() + ".";
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
