using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleDrawing
{
    abstract public class CanvasCommandProcessor
    {
        public enum ReturnCodes { OK, Error, Stop};

        protected string _errorString = "";
        protected Canvas canvas;

        public CanvasCommandProcessor(Canvas iCanvas)
        {
            canvas = iCanvas;
        }

        public string Error
        {
            get { return _errorString; }
        }

        // Implement this method in subclass depending on the environment
        abstract public void ProcessInputs();

        // Returns 1 to continue, 0 to quit or -1 if error
        public ReturnCodes ProcessInputLine(string fullCommand)
        {
            ReturnCodes retCode = ReturnCodes.OK;
            _errorString = "";

            try
            {
                fullCommand = fullCommand.Trim();

                if(fullCommand.Length == 0)
                {
                    _errorString = "Please enter a command.";
                    return ReturnCodes.Error;
                }

                char mainCommand = fullCommand.First<char>();

                switch (mainCommand)
                {
                    case 'C':
                        if (ProcessCreateCanvasCommand(fullCommand))
                            retCode = ReturnCodes.OK;
                        else
                            retCode = ReturnCodes.Error;

                        break;

                    case 'L':
                        if (ProcessLineCommand(fullCommand))
                            retCode = ReturnCodes.OK;
                        else
                            retCode = ReturnCodes.Error;

                        break;

                    case 'R':
                        if (ProcessRectangleCommand(fullCommand))
                            retCode = ReturnCodes.OK;
                        else
                            retCode = ReturnCodes.Error;
                       
                        break;

                    case 'B':
                        if (ProcessBucketFillCommand(fullCommand))
                            retCode = ReturnCodes.OK;
                        else
                            retCode = ReturnCodes.Error;
                     
                        break;

                    case 'Q':
                        retCode = ReturnCodes.Stop;
                        break;

                    default:
                        retCode = ReturnCodes.Error;
                        _errorString = fullCommand + " is an unrecognised command.";
                        break;
                }
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                retCode = ReturnCodes.Error;
            }

            return retCode;
        }

        // Return true if parameters are ok or false if not
        private bool isNumberOfParametersOK(string fullCommand, int numberOfParameters, string correctCommandUsage)
        {
            _errorString = "";

            try
            {
                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                if (elements.Length < numberOfParameters)
                {
                    _errorString = "Too few parameters. Usage is " + correctCommandUsage + ".";
                    return false;
                }

                if (elements.Length > numberOfParameters)
                {
                    _errorString = "Too many parameters. Usage is " + correctCommandUsage + ".";
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Return true is successful and false on error
        private bool ProcessBucketFillCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand, 4, "B x y c"))
                    return false;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x;
                if (!int.TryParse(elements[1], out x))
                {
                    _errorString = "First parameter must be an integer specifying x.";
                    return false;
                }

                int y;
                if (!int.TryParse(elements[2], out y))
                {
                    _errorString = "Second parameter must be an integer specifying y.";
                    return false;
                }

                string colour;
                if (elements[3].Length > 1)
                {
                    _errorString = "Third parameter must be a single character specifying the colour.";
                    return false;
                }
                else
                    colour = elements[3];

                if (!canvas.Fill(x, y, colour))
                {
                    _errorString = canvas.Error;
                    return false;
                }

                if (!canvas.Display())
                {
                    _errorString = canvas.Error;
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Return true is successful and false on error
        private bool ProcessRectangleCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand, 5, "R x1 y1 x2 y2"))
                    return false;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x1;
                if (!int.TryParse(elements[1], out x1))
                {
                    _errorString = "First parameter must be an integer specifying x1.";
                    return false;
                }

                int y1;
                if (!int.TryParse(elements[2], out y1))
                {
                    _errorString = "Second parameter must be an integer specifying y1.";
                    return false;
                }

                int x2;
                if (!int.TryParse(elements[3], out x2))
                {
                    _errorString = "First parameter must be an integer specifying x2.";
                    return false;
                }

                int y2;
                if (!int.TryParse(elements[4], out y2))
                {
                    _errorString = "Second parameter must be an integer specifying y2.";
                    return false;
                }

                if(x2 < x1)
                {
                    _errorString = "x2 must be greater than x1.";
                    return false;
                }

                if(y2 < y1)
                {
                    _errorString = "y2 must be greater than y1.";
                    return false;
                }

                int rectangleWidth = x2 - x1 + 1;
                int rectangleHeight = y2 - y1 + 1;
                if (!canvas.AddRectangle(x1, y1, rectangleWidth, rectangleHeight))
                {
                    _errorString = canvas.Error;
                    return false;
                }                   

                if (!canvas.Display())
                {
                    _errorString = canvas.Error;
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return true;
            }
        }

        // Return true is successful and false on error
        private bool ProcessLineCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand, 5, "L x1 y1 x2 y2"))
                    return false;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x1;
                if (!int.TryParse(elements[1], out x1))
                {
                    _errorString = "First parameter must be an integer specifying x1.";
                    return false;
                }

                int y1;
                if (!int.TryParse(elements[2], out y1))
                {
                    _errorString = "Second parameter must be an integer specifying y1.";
                    return false;
                }

                int x2;
                if (!int.TryParse(elements[3], out x2))
                {
                    _errorString = "First parameter must be an integer specifying x2.";
                    return false;
                }

                int y2;
                if (!int.TryParse(elements[4], out y2))
                {
                    _errorString = "Second parameter must be an integer specifying y2.";
                    return false;
                }

                int length;
                if (y1 == y2)
                {
                    int x = x1;
                    if (x1 > x2)
                        x = x2;
                    length = Math.Abs(x2 - x1) + 1;
                    if (!canvas.AddHorizontalLine(x, y1, length))
                    {
                        _errorString = canvas.Error;
                        return false;
                    }
                }
                else
                {
                    if (x1 == x2)
                    {
                        int y = y1;
                        if (y1 > y2)
                            y = y2;
                        length = Math.Abs(y2 - y1) + 1;
                        if (!canvas.AddVerticalLine(x1, y, length))
                        {
                            _errorString = canvas.Error;
                            return false;
                        }
                    }
                    else
                    {
                        _errorString = "Either x1 must equal x2 for a vertical line or y1 must equal y2 for a horizontal line.";
                        return false;
                    }
                }

                if(!canvas.Display())
                {
                    _errorString = canvas.Error;
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Return true is successful and false on error
        private bool ProcessCreateCanvasCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand, 3, "C w h"))
                    return false;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int requestedWidth;
                if (!int.TryParse(elements[1], out requestedWidth))
                {
                    _errorString = "First parameter must be an integer specifying the width.";
                    return false;
                }

                if(requestedWidth < 1)
                {
                    _errorString = "Width of canvas must be at least 1.";
                    return false;
                }

                int requestedHeight;
                if (!int.TryParse(elements[2], out requestedHeight))
                {
                    _errorString = "Second parameter must be an integer specifying the height.";
                    return false;
                }

                if(requestedHeight < 1)
                {
                    _errorString = "Height of canvas must be at least 1.";
                    return false;
                }
                
                // If this line has been reached then parameters are good.
                if (!canvas.Create(requestedWidth, requestedHeight))
                {
                    _errorString = canvas.Error;
                    return false;
                }

                if(!canvas.Display())
                {
                    _errorString = canvas.Error;
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }
    }
}
