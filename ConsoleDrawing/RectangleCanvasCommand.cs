using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    public class RectangleCanvasCommand : LineCanvasCommand
    {      
        public override char SupportedCommand
        {
            get { return 'R'; }
        }

        public override string Usage
        {
            get { return "R x1 y1 x2 y2 - Add a rectangle whose upper left corner is (x1, y1) and lower right corner is (x2, y2)"; }
        }

        public override int NumberOfParameters
        {
            get { return 5; }
        }

        // Return true is successful and false on error
        public override bool ProcessCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!_canvas.HasCanvasBeenCreated())
                {
                    _errorString = "Please create a canvas first.";
                    return false;
                }

                string[] elements = GetParameters(fullCommand);
                if (elements == null)
                    return false;

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

                int colourKey = _canvas.GetColourKeyFor('x');

                int rectangleWidth = x2 - x1 + 1;
                int rectangleHeight = y2 - y1 + 1;
                if (!AddRectangle(x1, y1, rectangleWidth, rectangleHeight, colourKey))
                {
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

        // Return true is successful and false on error
        public bool AddRectangle(int x, int y, int width, int height, int colourKey)
        {
            _errorString = "";

            try
            {
                if (!areXAndYWithinBounds(x, y))
                    return false;

                if (!AddHorizontalLine(x, y, width, colourKey))
                    return false;

                if (!AddVerticalLine(x, y, height, colourKey))
                    return false;

                if (!AddVerticalLine(x + width - 1, y, height, colourKey))
                    return false;

                if (!AddHorizontalLine(x, y + height - 1, width, colourKey))
                    return false;

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
