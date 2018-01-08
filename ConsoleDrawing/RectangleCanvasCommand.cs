using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleDrawing
{
    public class RectangleCanvasCommand : CanvasCommand
    {
        public RectangleCanvasCommand(Canvas iCanvas) : base(iCanvas)
        {
        }

        public override char SupportedCommand()
        {
            return 'R';
        }

        public override string GetUsage()
        {
            return "R x1 y1 x2 y2 - Add a rectangle whose upper left corner is (x1, y1) and lower right corner is (x2, y2)";
        }

        public override int GetNumberOfParameters()
        {
            return 5;
        }

        // Return true is successful and false on error
        public override bool ProcessCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand))
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

                if (x2 < x1)
                {
                    _errorString = "x2 must be greater than x1.";
                    return false;
                }

                if (y2 < y1)
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
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return true;
            }
        }
    }
}
