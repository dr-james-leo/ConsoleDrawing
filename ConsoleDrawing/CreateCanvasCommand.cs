using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;


namespace ConsoleDrawing
{
    public class CreateCanvasCommand : CanvasCommand
    {
        public CreateCanvasCommand(Canvas iCanvas) : base(iCanvas)
        {
        }

        public override char SupportedCommand()
        {
            return 'C';
        }

        public override string GetUsage()
        {
            return "C w h - Create a new canvas of width w and height h";
        }

        public override int GetNumberOfParameters()
        {
            return 3;
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

                int requestedWidth;
                if (!int.TryParse(elements[1], out requestedWidth))
                {
                    _errorString = "First parameter must be an integer specifying the width.";
                    return false;
                }

                if (requestedWidth < 1)
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

                if (requestedHeight < 1)
                {
                    _errorString = "Height of canvas must be at least 1.";
                    return false;
                }

                if (requestedHeight > _canvas.MaxHeight)
                {
                    _errorString = "Maximum height of " + _canvas.MaxHeight + " exceeded.";
                    return false;
                }

                if (requestedWidth > _canvas.MaxWidth)
                {
                    _errorString = "Maximum width of " + _canvas.MaxWidth + " exceeded.";
                    return false;
                }

                _canvas.RefreshCanvasData(requestedWidth, requestedHeight);
                
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
