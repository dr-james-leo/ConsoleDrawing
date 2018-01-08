using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    abstract public class Canvas
    {
        private int[,] _canvasData; // Holds the data specifying the drawings on the canvas
        private Hashtable _colourMap; // maps colours to integers for filling the canvas

        // Parameters with defaults
        private char _spaceChar = ' ';
        private char _lineChar = 'x';
        private char _topAndBottomEdgeChar = '-';
        private char _leftAndRightEdgeChar = '|';
        private int _maxWidth = 1000;
        private int _maxHeight = 1000;

        private int _width;
        private int _height;

        private bool hasCanvasBeenCreated = false;

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

        public string Error
        {
            get { return _errorString; }
        }

        // Creates a new Canvas to draw on
        // Return true is successful and false on error
        public bool Create(int requiredWidth, int requiredHeight)
        {
            _errorString = "";

            try
            {
                if(requiredHeight > _maxHeight)
                {
                    _errorString = "Maximum height of " + _maxHeight + " exceeded.";
                    return false;
                }

                if (requiredWidth > _maxWidth)
                {
                    _errorString = "Maximum width of " + _maxWidth + " exceeded.";
                    return false;
                }

                _width = requiredWidth;
                _height = requiredHeight;

                _canvasData = new int[_width, _height];
                _colourMap = new Hashtable();
                hasCanvasBeenCreated = true;
                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Checks that an inital canvas has been created and the values of x and y are within the canvas space
        // Return true is successful and false on error
        private bool UndertakeSanityCheck(int x, int y)
        {
            if (!hasCanvasBeenCreated)
            {
                _errorString = "Please create a canvas first.";
                return false;
            }

            if (x < 1 || x > _width)
            {
                _errorString = "x must be between 1 and " + _width + ".";
                return false;
            }

            if (y < 1 || y > _height)
            {
                _errorString = "y must be between 1 and " + _height + ".";
                return false;
            }

            return true;
        }

        // Fills the canvas with the specified colour from position x,y
        // Return true is successful and false on error
        public bool Fill(int x, int y, string colour)
        {
            _errorString = "";

            try
            {
                if(!UndertakeSanityCheck(x, y))
                    return false;

                int colourKey;

                // Don't want to map the same colour twice so check if we have already used it
                if(_colourMap.ContainsValue(colour))
                {
                    colourKey = _colourMap.Keys.OfType<int>().FirstOrDefault(a => (string)_colourMap[a] == colour);
                }
                else
                {
                    colourKey = 2 + _colourMap.Count;
                    _colourMap.Add(colourKey, colour);
                }

                if (!FillCell(x, y, colourKey))
                    return false;

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Used to fill the 4 neighbours of a cell, above, below, left and right
        // Return true is successful and false on error
        private bool FillNeighbours(int x, int y, int colourKey)
        {
            _errorString = "";

            try
            {
                if (!FillCell(x + 1, y, colourKey))
                    return false;

                if (!FillCell(x - 1, y, colourKey))
                    return false;

                if (!FillCell(x, y + 1, colourKey))
                    return false;

                if (!FillCell(x, y - 1, colourKey))
                    return false;

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Ensures the fill routine doesn't mpve outside the canvas size
        private bool isWithinBounds(int x, int y)
        {
            if (x < 1 || x > _width || y < 1 || y > _height)
                return false;
            else
                return true;
        }

        // Fills a cell with the specified colour and then tries to fill neighbours
        // Return true is successful and false on error
        private bool FillCell(int x, int y, int colourKey)
        {
            _errorString = "";

            try
            {
                if (isWithinBounds(x, y) && _canvasData[x - 1, y - 1] == 0)
                {
                    _canvasData[x - 1, y - 1] = colourKey;
                    if (!FillNeighbours(x, y, colourKey))
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
        public bool AddRectangle(int x, int y, int width, int height)
        {
            _errorString = "";

            try
            {
                if (!UndertakeSanityCheck(x, y))
                    return false;

                if (!AddHorizontalLine(x, y, width))
                    return false;

                if (!AddVerticalLine(x, y, height))
                    return false;

                if (!AddVerticalLine(x + width - 1, y, height))
                    return false;

                if (!AddHorizontalLine(x, y + height - 1, width))
                    return false;

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Return true is successful and false on error
        public bool AddHorizontalLine(int x, int y, int length)
        {
            _errorString = "";

            try
            {
                if (!UndertakeSanityCheck(x, y))
                    return false;

                if ((x + length - 1) > _width)
                {
                    _errorString = "Length of line too long.";
                    return false;
                }

                for (int i = 0; i < length; i++)
                    _canvasData[x - 1 + i, y - 1] = 1;
                
                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Return true is successful and false on error
        public bool AddVerticalLine(int x, int y, int length)
        {
            _errorString = "";

            try
            {
                if (!UndertakeSanityCheck(x, y))
                    return false;

                if ((y + length - 1) > _height)
                {
                    _errorString = "Length of line too long.";
                    return false;
                }

                for (int j = 0; j < length; j++)
                {
                    _canvasData[x - 1, y - 1 + j] = 1;
                }

                return true;
            }
            catch(Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Implement for the particular display being used
        abstract public bool Display();
     
        // Returns an empty string on error and sets _errorString;
        protected string RenderToString(string newLineChar)
        {
            _errorString = "";

            try
            {
                if (!hasCanvasBeenCreated)
                {
                    _errorString = "Please create a canvas first.";
                    return "";
                }

                StringBuilder displayString = new StringBuilder("");

                // Render the top line
                for (int i = 0; i < _width + 2; i++)
                    displayString.Append(_topAndBottomEdgeChar);

                displayString.Append(newLineChar);

                for (int j = 0; j < _height; j++)
                {
                    displayString.Append(_leftAndRightEdgeChar);

                    for (int i = 0; i < _width; i++)
                    {
                        switch (_canvasData[i, j])
                        {
                            case 0:
                                displayString.Append(_spaceChar);
                                break;

                            case 1:
                                displayString.Append(_lineChar);
                                break;

                            default:
                                string colour = (string)_colourMap[_canvasData[i, j]];
                                displayString.Append(colour);
                                break;
                        }
                    }

                    displayString.Append(_leftAndRightEdgeChar);
                    displayString.Append(newLineChar);
                }

                // Render the bottom line
                for (int i = 0; i < _width + 2; i++)
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