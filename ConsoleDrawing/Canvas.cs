using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    abstract public class Canvas
    {
        private int[,] _canvasData; // Holds the data specifying the drawings on the canvas
        private Hashtable _colourMap; // maps colours to integers for filling the canvas
        private Hashtable _commandMap = new Hashtable(); // supported commands

        //private const string CreateCanvasUsage = "C w h - Create a new canvas of width w and height h";
        private const string DisplayCanvasUsage = "D - Displays canvas";

        // Parameters with defaults
        private char _spaceChar = ' ';
        private char _lineChar = 'x';
        private char _topAndBottomEdgeChar = '-';
        private char _leftAndRightEdgeChar = '|';
        private int _maxWidth = 1000;
        private int _maxHeight = 1000;

        //private int _width;
        //private int _height;

        //private bool hasCanvasBeenCreated = false;

        // Gets set if any method errors
        protected string _errorString = "";

        public Canvas()
        {
            LoadSupportedCommands();
        }

        // Allows overriding the default values for the maximum size of the canvas
        public Canvas(int maxWidth, int maxHeight)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            LoadSupportedCommands();
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
            LoadSupportedCommands();
        }

        public void RefreshCanvasData(int requiredWidth, int requiredHeight)
        {
            _canvasData = new int[requiredWidth, requiredHeight];
            _colourMap = new Hashtable();
        }

        public int MaxHeight
        {
            get { return _maxHeight; }
        }

        public int MaxWidth
        {
            get { return _maxWidth;  }
        }

        public Hashtable GetColourMap()
        {
            return _colourMap;
        }

        public bool HasCanvasBeenCreated()
        {
            if (_canvasData == null)
                return false;
            else
                return true;
        }

        public int GetCanvasWidth()
        {
            if (HasCanvasBeenCreated())
                return _canvasData.GetLength(0);
            else
                return 0;
        }

        public int GetCanvasHeight()
        {
            if (HasCanvasBeenCreated())
                return _canvasData.GetLength(1);
            else
                return 0;
        }

        public int[,] GetCanvasData()
        {
            return _canvasData;
        }

        private void LoadSupportedCommands()
        {
            CreateCanvasCommand createCanvasCommand = new CreateCanvasCommand(this);
            _commandMap.Add(createCanvasCommand.SupportedCommand(), createCanvasCommand);

            LineCanvasCommand lineCanvasCommand = new LineCanvasCommand(this);
            _commandMap.Add(lineCanvasCommand.SupportedCommand(), lineCanvasCommand);

            RectangleCanvasCommand rectangleCanvasCommand = new RectangleCanvasCommand(this);
            _commandMap.Add(rectangleCanvasCommand.SupportedCommand(), rectangleCanvasCommand);

            FillCanvasCommand fillCanvasCommand = new FillCanvasCommand(this);
            _commandMap.Add(fillCanvasCommand.SupportedCommand(), fillCanvasCommand);
        }

        // Implement for the particular display being used
        abstract public bool Display();

        public string Error
        {
            get { return _errorString; }
        }

        public void AddCommand(CanvasCommand command)
        {
            _commandMap.Add(command.SupportedCommand(), command);
        }

        public string GetUsage(string newLineChar)
        {
            StringBuilder usageString = new StringBuilder(DisplayCanvasUsage);

            foreach(CanvasCommand canvasCommand in _commandMap)
            {
                usageString.Append(newLineChar + canvasCommand.GetUsage());
            }

            return usageString.ToString();
        }

        //// Creates a new Canvas to draw on, usage is C w h
        //// Return true is successful and false on error
        //private bool Create(string fullCommand)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        string pattern = @"\s+";
        //        string[] elements = Regex.Split(fullCommand, pattern);

        //        if (elements.Length < 3)
        //        {
        //            _errorString = "Too few parameters. Usage is " + CreateCanvasUsage + ".";
        //            return false;
        //        }

        //        if (elements.Length > 3)
        //        {
        //            _errorString = "Too many parameters. Usage is " + CreateCanvasUsage + ".";
        //            return false;
        //        }

        //        int requestedWidth;
        //        if (!int.TryParse(elements[1], out requestedWidth))
        //        {
        //            _errorString = "First parameter must be an integer specifying the width.";
        //            return false;
        //        }

        //        if (requestedWidth < 1)
        //        {
        //            _errorString = "Width of canvas must be at least 1.";
        //            return false;
        //        }

        //        int requestedHeight;
        //        if (!int.TryParse(elements[2], out requestedHeight))
        //        {
        //            _errorString = "Second parameter must be an integer specifying the height.";
        //            return false;
        //        }

        //        if (requestedHeight < 1)
        //        {
        //            _errorString = "Height of canvas must be at least 1.";
        //            return false;
        //        }


        //        //int height = _canvasData.GetLength(0);
        //        //int width = _canvasData.GetLength(1);
        //        if (requestedHeight > _maxHeight)
        //        {
        //            _errorString = "Maximum height of " + _maxHeight + " exceeded.";
        //            return false;
        //        }

        //        if (requestedWidth > _maxWidth)
        //        {
        //            _errorString = "Maximum width of " + _maxWidth + " exceeded.";
        //            return false;
        //        }

        //        //_width = requiredWidth;
        //        //_height = requiredHeight;

        //        _canvasData = new int[requestedWidth, requestedHeight];
        //        _colourMap = new Hashtable();
        //        //hasCanvasBeenCreated = true;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        public bool ExecuteCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
              char mainCommand = fullCommand.First<char>();

                switch (mainCommand)
                {
                    case 'D':
                        if (!Display())
                            return false;
                        break;

                    default:
                        CanvasCommand canvasCommand = (CanvasCommand)_commandMap[mainCommand];
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
                            else
                            {
                                if (!Display())
                                    return false;
                            }
                        }
                        break;
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
        virtual public bool UndertakeSanityCheck(int x, int y)
        {

            if (x < 1 || x > GetCanvasWidth())
            {
                _errorString = "x must be between 1 and " + GetCanvasWidth() + ".";
                return false;
            }

            if (y < 1 || y > GetCanvasHeight())
            {
                _errorString = "y must be between 1 and " + GetCanvasHeight() + ".";
                return false;
            }

            return true;
        }

        //// Fills the canvas with the specified colour from position x,y
        //// Return true is successful and false on error
        //public bool Fill(int x, int y, string colour)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if(!UndertakeSanityCheck(x, y))
        //            return false;

        //        int colourKey;

        //        // Don't want to map the same colour twice so check if we have already used it
        //        if(_colourMap.ContainsValue(colour))
        //        {
        //            colourKey = _colourMap.Keys.OfType<int>().FirstOrDefault(a => (string)_colourMap[a] == colour);
        //        }
        //        else
        //        {
        //            colourKey = 2 + _colourMap.Count;
        //            _colourMap.Add(colourKey, colour);
        //        }

        //        if (!FillCell(x, y, colourKey))
        //            return false;

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        //// Used to fill the 4 neighbours of a cell, above, below, left and right
        //// Return true is successful and false on error
        //private bool FillNeighbours(int x, int y, int colourKey)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (!FillCell(x + 1, y, colourKey))
        //            return false;

        //        if (!FillCell(x - 1, y, colourKey))
        //            return false;

        //        if (!FillCell(x, y + 1, colourKey))
        //            return false;

        //        if (!FillCell(x, y - 1, colourKey))
        //            return false;

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        //// Ensures the fill routine doesn't mpve outside the canvas size
        //private bool isWithinBounds(int x, int y)
        //{
        //    if (x < 1 || x > _width || y < 1 || y > _height)
        //        return false;
        //    else
        //        return true;
        //}

        //// Fills a cell with the specified colour and then tries to fill neighbours
        //// Return true is successful and false on error
        //private bool FillCell(int x, int y, int colourKey)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (isWithinBounds(x, y) && _canvasData[x - 1, y - 1] == 0)
        //        {
        //            _canvasData[x - 1, y - 1] = colourKey;
        //            if (!FillNeighbours(x, y, colourKey))
        //                return false;
        //        }
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        //// Return true is successful and false on error
        //public bool AddRectangle(int x, int y, int width, int height)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (!UndertakeSanityCheck(x, y))
        //            return false;

        //        if (!AddHorizontalLine(x, y, width))
        //            return false;

        //        if (!AddVerticalLine(x, y, height))
        //            return false;

        //        if (!AddVerticalLine(x + width - 1, y, height))
        //            return false;

        //        if (!AddHorizontalLine(x, y + height - 1, width))
        //            return false;

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        //// Return true is successful and false on error
        //public bool AddHorizontalLine(int x, int y, int length)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (!UndertakeSanityCheck(x, y))
        //            return false;

        //        if ((x + length - 1) > _width)
        //        {
        //            _errorString = "Length of line too long.";
        //            return false;
        //        }

        //        for (int i = 0; i < length; i++)
        //            _canvasData[x - 1 + i, y - 1] = 1;
                
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        //// Return true is successful and false on error
        //public bool AddVerticalLine(int x, int y, int length)
        //{
        //    _errorString = "";

        //    try
        //    {
        //        if (!UndertakeSanityCheck(x, y))
        //            return false;

        //        if ((y + length - 1) > _height)
        //        {
        //            _errorString = "Length of line too long.";
        //            return false;
        //        }

        //        for (int j = 0; j < length; j++)
        //        {
        //            _canvasData[x - 1, y - 1 + j] = 1;
        //        }

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        _errorString = "An error ocurred: " + ex.Message;
        //        return false;
        //    }
        //}

        // Returns an empty string on error and sets _errorString;
        protected string RenderToString(string newLineChar)
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
                for (int i = 0; i < GetCanvasWidth() + 2; i++)
                    displayString.Append(_topAndBottomEdgeChar);

                displayString.Append(newLineChar);

                for (int j = 0; j < GetCanvasHeight(); j++)
                {
                    displayString.Append(_leftAndRightEdgeChar);

                    for (int i = 0; i < GetCanvasWidth(); i++)
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
                for (int i = 0; i < GetCanvasWidth() + 2; i++)
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