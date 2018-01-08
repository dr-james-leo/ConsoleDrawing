using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleDrawing
{
    class FillCanvasCommand: CanvasCommand
    {
        public FillCanvasCommand(Canvas iCanvas) : base(iCanvas)
        {
        }

        public override char SupportedCommand()
        {
            return 'B';
        }

        public override string GetUsage()
        {
            return "B x y c - Fill entire area connect to (x, y) with colour c";
        }

        public override int GetNumberOfParameters()
        {
            return 4;
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
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }
    }
}
