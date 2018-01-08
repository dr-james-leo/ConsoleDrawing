using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    abstract public class CanvasCommandProcessor
    {
        public enum ReturnCodes { OK, Error, Stop, Usage};

        protected string _errorString = "";
        protected Canvas canvas;

        private Hashtable _commandMap;

        //private CanvasCommand createCanvasCommand;
        //private CanvasCommand lineCanvasCommand;
        //private CanvasCommand rectangleCanvasCommand;
        //private CanvasCommand fillCanvasCommand;
        //private CanvasCommand displayCanvasCommand;

        public CanvasCommandProcessor(Canvas iCanvas, Hashtable iCommandMap)
        {
            canvas = iCanvas;
            _commandMap = iCommandMap;
            //createCanvasCommand = new CreateCanvasCommand(canvas);
            //lineCanvasCommand = new LineCanvasCommand(canvas);
            //rectangleCanvasCommand = new RectangleCanvasCommand(canvas);
            //fillCanvasCommand = new FillCanvasCommand(canvas);
            //displayCanvasCommand = new DisplayCanvasCommand(canvas);

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
                    case 'Q':
                        retCode = ReturnCodes.Stop;
                        break;

                    case '?':
                        retCode = ReturnCodes.Usage;
                        break;

                    default:
                        CanvasCommand canvasCommand = (CanvasCommand)_commandMap[mainCommand];
                        if (canvasCommand == null)
                        {
                            retCode = ReturnCodes.Error;
                            _errorString = fullCommand + " is an unrecognised command.";
                        }
                        else
                        {
                            if (canvasCommand.ProcessCommand(fullCommand))
                                retCode = ReturnCodes.OK;
                            else
                                retCode = ReturnCodes.Error;
                        }
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

        public string GetUsage(string newLineChar)
        {
            //return CUsage + newLineChar + LUsage + newLineChar + RUsage + newLineChar + BUsage + newLineChar + DUsage;
            return "";
        }

        //// Return true if parameters are ok or false if not
        //private bool isNumberOfParametersOK(string fullCommand, int numberOfParameters, string correctCommandUsage)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        string pattern = @"\s+";
        //        string[] elements = Regex.Split(fullCommand, pattern);

        //        if (elements.Length < numberOfParameters)
        //        {
        //            _errorString = "Too few parameters. Usage is " + correctCommandUsage + ".";
        //            return false;
        //        }

        //        if (elements.Length > numberOfParameters)
        //        {
        //            _errorString = "Too many parameters. Usage is " + correctCommandUsage + ".";
        //            return false;
        //        }

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}  
    }
}
