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
        public int ProcessInputLine(string fullCommand)
        {
            int retCode = 1;
            _errorString = "";

            try
            {
                fullCommand = fullCommand.Trim();

                if(fullCommand.Length == 0)
                {
                    _errorString = "Please enter a command.";
                    return -1;
                }

                char mainCommand = fullCommand.First<char>();

                switch (mainCommand)
                {
                    case 'C':   
                        retCode = ProcessCreateCanvasCommand(fullCommand);
                        break;

                    case 'L':
                        retCode = ProcessLineCommand(fullCommand);
                        break;

                    case 'R':
                        retCode = ProcessRectangleCommand(fullCommand);
                        break;

                    case 'B':
                        retCode = ProcessBucketFillCommand(fullCommand);
                        break;

                    case 'Q':
                        retCode = 0;
                        break;

                    default:
                        retCode = -1;
                        _errorString = fullCommand + " is an unrecognised command.";
                        break;
                }
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                retCode = -1;
            }

            return retCode;
        }

        // Return 1 if parameters are ok or -1 if not
        private int CheckNumberOfParameters(string fullCommand, int numberOfParameters, string correctCommandUsage)
        {
            _errorString = "";

            try
            {
                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                if (elements.Length < numberOfParameters)
                {
                    _errorString = "Too few parameters. Usage is " + correctCommandUsage + ".";
                    return -1;
                }

                if (elements.Length > numberOfParameters)
                {
                    _errorString = "Too many parameters. Usage is " + correctCommandUsage + ".";
                    return -1;
                }

                return 1;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return -1;
            }
        }

        // Returns 1 on success or -1 on error
        private int ProcessBucketFillCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (CheckNumberOfParameters(fullCommand, 4, "B x y c") == -1)
                    return -1;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x;
                if (!int.TryParse(elements[1], out x))
                {
                    _errorString = "First parameter must be an integer specifying x.";
                    return -1;
                }

                int y;
                if (!int.TryParse(elements[2], out y))
                {
                    _errorString = "Second parameter must be an integer specifying y.";
                    return -1;
                }

                string colour;
                if (elements[3].Length > 1)
                {
                    _errorString = "Third parameter must be a single character specifying the colour.";
                    return -1;
                }
                else
                    colour = elements[3];

                if (canvas.Fill(x, y, colour) == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                if (canvas.Display() == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                return 1;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return -1;
            }
        }

        // Returns 1 on success or -1 on error
        private int ProcessRectangleCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (CheckNumberOfParameters(fullCommand, 5, "R x1 y1 x2 y2") == -1)
                    return -1;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x1;
                if (!int.TryParse(elements[1], out x1))
                {
                    _errorString = "First parameter must be an integer specifying x1.";
                    return -1;
                }

                int y1;
                if (!int.TryParse(elements[2], out y1))
                {
                    _errorString = "Second parameter must be an integer specifying y1.";
                    return -1;
                }

                int x2;
                if (!int.TryParse(elements[3], out x2))
                {
                    _errorString = "First parameter must be an integer specifying x2.";
                    return -1;
                }

                int y2;
                if (!int.TryParse(elements[4], out y2))
                {
                    _errorString = "Second parameter must be an integer specifying y2.";
                    return -1;
                }

                if(x2 < x1)
                {
                    _errorString = "x2 must be greater than x1.";
                    return -1;
                }

                if(y2 < y1)
                {
                    _errorString = "y2 must be greater than y1.";
                    return -1;
                }

                int rectangleWidth = x2 - x1 + 1;
                int rectangleHeight = y2 - y1 + 1;
                if (canvas.AddRectangle(x1, y1, rectangleWidth, rectangleHeight) == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }                   

                if (canvas.Display() == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                return 1;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return -1;
            }
        }

        // Returns 1 on success or -1 on error
        private int ProcessLineCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (CheckNumberOfParameters(fullCommand, 5, "L x1 y1 x2 y2") == -1)
                    return -1;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x1;
                if (!int.TryParse(elements[1], out x1))
                {
                    _errorString = "First parameter must be an integer specifying x1.";
                    return -1;
                }

                int y1;
                if (!int.TryParse(elements[2], out y1))
                {
                    _errorString = "Second parameter must be an integer specifying y1.";
                    return -1;
                }

                int x2;
                if (!int.TryParse(elements[3], out x2))
                {
                    _errorString = "First parameter must be an integer specifying x2.";
                    return -1;
                }

                int y2;
                if (!int.TryParse(elements[4], out y2))
                {
                    _errorString = "Second parameter must be an integer specifying y2.";
                    return -1;
                }

                int length;
                if (y1 == y2)
                {
                    int x = x1;
                    if (x1 > x2)
                        x = x2;
                    length = Math.Abs(x2 - x1) + 1;
                    if (canvas.AddHorizontalLine(x, y1, length) == -1)
                    {
                        _errorString = canvas.Error;
                        return -1;
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
                        if (canvas.AddVerticalLine(x1, y, length) == -1)
                        {
                            _errorString = canvas.Error;
                            return -1;
                        }
                    }
                    else
                    {
                        _errorString = "Either x1 must equal x2 for a vertical line or y1 must equal y2 for a horizontal line.";
                        return -1;
                    }
                }

                if(canvas.Display() == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                return 1;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return -1;
            }
        }

        // Returns 1 on success or -1 on error
        private int ProcessCreateCanvasCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (CheckNumberOfParameters(fullCommand, 3, "C w h") == -1)
                    return -1;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int requestedWidth;
                if (!int.TryParse(elements[1], out requestedWidth))
                {
                    _errorString = "First parameter must be an integer specifying the width.";
                    return -1;
                }

                if(requestedWidth < 1)
                {
                    _errorString = "Width of canvas must be at least 1.";
                    return -1;
                }

                int requestedHeight;
                if (!int.TryParse(elements[2], out requestedHeight))
                {
                    _errorString = "Second parameter must be an integer specifying the height.";
                    return -1;
                }

                if(requestedHeight < 1)
                {
                    _errorString = "Height of canvas must be at least 1.";
                    return -1;
                }
                
                // If this line has been reached then parameters are good.
                if (canvas.Create(requestedWidth, requestedHeight) == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                if(canvas.Display() == -1)
                {
                    _errorString = canvas.Error;
                    return -1;
                }

                return 1;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return -1;
            }
        }
    }
}
