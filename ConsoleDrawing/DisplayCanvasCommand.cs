using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    public class DisplayCanvasCommand : CanvasCommand
    {
        public DisplayCanvasCommand(Canvas iCanvas) : base(iCanvas)
        {
        }

        public override char SupportedCommand()
        {
            return 'D';
        }

        public override string GetUsage()
        {
            return "D - Displays the current canvas";
        }

        public override int GetNumberOfParameters()
        {
            return 1;
        }

        // Return true is successful and false on error
        public override bool ProcessCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand))
                    return false;

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
