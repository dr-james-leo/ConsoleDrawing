using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    public class Canvas
    {
        private int[,] _canvasData; // Holds the data specifying the drawings on the canvas
        private Hashtable _colourList; // maps colours to integers for filling the canvas
        private Hashtable _commandList = new Hashtable(); // supported commands
        
        // Parameters with defaults
        private char _spaceChar = ' ';
        private char _lineChar = 'x';
        private char _topAndBottomEdgeChar = '-';
        private char _leftAndRightEdgeChar = '|';
        private int _maxWidth = 1000;
        private int _maxHeight = 1000;
        
        // Gets set if any method errors
        protected string _errorString = "";

        public Canvas()
        {
        }

        // Allows overriding the default values for the maximum size of the canvas
        public Canvas(int maxWidth, int maxHeight)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
        }

        // Allows overriding the defaults values for the maximum size of the canvas and the characters used to draw the canvas
        public Canvas(char spaceChar, char lineChar, char topAndBottomEdgeChar, char leftAndRightEdgeChar, int maxWidth, int maxHeight)
        {
            _spaceChar = spaceChar;
            _lineChar = lineChar;
            _topAndBottomEdgeChar = topAndBottomEdgeChar;
            _leftAndRightEdgeChar = leftAndRightEdgeChar;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
        }

        public char SpaceChar
        {
            get { return _spaceChar; }
        }

        public char LineChar
        {
            get { return _lineChar; }
        }

        public bool AddNewCommand(CanvasCommand canvasCommand)
        {
            if (_commandList[canvasCommand.SupportedCommand] == null)
            {
                canvasCommand.SetCanvas(this);
                _commandList.Add(canvasCommand.SupportedCommand, canvasCommand);
                return true;
            }
            else
            {
                _errorString = "Command " + canvasCommand.SupportedCommand + " already supported";
                return false;
            }
        }

        public void RefreshCanvasData(int requiredWidth, int requiredHeight)
        {
            _canvasData = new int[requiredWidth, requiredHeight];
            _colourList = new Hashtable();
            _colourList.Add(0, _spaceChar); // Add in the spaces
        }

        public int GetColourKeyFor(char colour)
        {
            int colourKey;

            // Don't want to map the same colour twice so check if we have already used it
            if (_colourList.ContainsValue(colour))
            {
                colourKey = _colourList.Keys.OfType<int>().FirstOrDefault(a => ((char)_colourList[a] == colour));
            }
            else
            {
                colourKey = _colourList.Count;
                _colourList.Add(colourKey, colour);
            }

            return colourKey;
        }

        public int MaxHeight
        {
            get { return _maxHeight; }
        }

        public int MaxWidth
        {
            get { return _maxWidth;  }
        }
        
        public bool HasCanvasBeenCreated()
        {
            if (_canvasData == null)
                return false;
            else
                return true;
        }

        public int CanvasWidth
        {
            get
            {
                if (HasCanvasBeenCreated())
                    return _canvasData.GetLength(0);
                else
                    return 0;
            }
        }

        public int CanvasHeight
        {
            get
            {
                if (HasCanvasBeenCreated())
                    return _canvasData.GetLength(1);
                else
                    return 0;
            }
        }

        public int[,] CanvasData
        {
            get
            {
                return _canvasData;
            }
        }
        
        public string Error
        {
            get { return _errorString; }
        }
        
        public string GetUsage(string newLineChar)
        {
            StringBuilder usageString = new StringBuilder("");

            foreach(char key in _commandList.Keys)
            {
                CanvasCommand canvasCommand = (CanvasCommand)_commandList[key];
                usageString.Append(newLineChar + canvasCommand.Usage);
            }

            return usageString.ToString();
        }
        
        public bool ExecuteCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                char mainCommand = fullCommand.First<char>();

                CanvasCommand canvasCommand = (CanvasCommand)_commandList[mainCommand];
                if (canvasCommand == null)
                {
                    _errorString = fullCommand + " is an unrecognised command.";
                    return false;
                }
                else
                {
                    if (!canvasCommand.ProcessCommand(fullCommand))
                    {
                        _errorString = canvasCommand.Error;
                        return false;
                    }                    
                }
                        
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }

            return true;
        }

        // Checks the values of x and y are within the canvas space
        // Return true is successful and false on error
        //virtual public bool areXAndYWithinBounds(int x, int y)
        //{

        //    if (x < 1 || x > CanvasWidth)
        //    {
        //        _errorString = "x must be between 1 and " + CanvasWidth + ".";
        //        return false;
        //    }

        //    if (y < 1 || y > CanvasHeight)
        //    {
        //        _errorString = "y must be between 1 and " + CanvasHeight + ".";
        //        return false;
        //    }

        //    return true;
        //}

        // Returns an empty string on error and sets _errorString;
        public string RenderToString(string newLineChar)
        {
            _errorString = "";

            try
            {
                if (!HasCanvasBeenCreated())
                {
                    _errorString = "Please create a canvas first.";
                    return "";
                }

                StringBuilder displayString = new StringBuilder("");

                // Render the top line
                for (int i = 0; i < CanvasWidth + 2; i++)
                    displayString.Append(_topAndBottomEdgeChar);

                displayString.Append(newLineChar);

                // Render the canvas data
                for (int j = 0; j < CanvasHeight; j++)
                {
                    displayString.Append(_leftAndRightEdgeChar);

                    for (int i = 0; i < CanvasWidth; i++)
                    {
                        char colour = (char)_colourList[_canvasData[i, j]];
                        displayString.Append(colour);                       
                    }

                    displayString.Append(_leftAndRightEdgeChar);
                    displayString.Append(newLineChar);
                }

                // Render the bottom line
                for (int i = 0; i < CanvasWidth + 2; i++)
                    displayString.Append(_topAndBottomEdgeChar);

                return displayString.ToString();
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return "";
            }          
        }
    }
}