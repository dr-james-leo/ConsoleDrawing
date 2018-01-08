using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleDrawing
{
    abstract public class CanvasCommand
    {
        protected string _errorString = "";
        protected Canvas canvas;

        public CanvasCommand(Canvas iCanvas)
        {
            canvas = iCanvas;
        }

        public string Error
        {
            get { return _errorString; }
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

        abstract public char SupportedCommand();
        abstract public string GetUsage();
        abstract public bool ProcessCommand(string fullCommand);
        abstract public int GetNumberOfParameters();
    }
}
