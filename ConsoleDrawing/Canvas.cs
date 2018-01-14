using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    // If an error occurs then throws exceptions of type CommandException
    public class Canvas
    {
        private int[,] _canvasData; // Holds the data specifying the drawings on the canvas
        private Dictionary<int, char> _colourList; // maps colours to integers for draawing on the canvas
        private Dictionary<char, CanvasCommand> _commandList = new Dictionary<char, CanvasCommand>(); // list of supported commands
        
        // Parameters with defaults
        private char _spaceChar = ' ';
        private char _lineChar = 'x';
        private char _topAndBottomEdgeChar = '-';
        private char _leftAndRightEdgeChar = '|';
        private int _maxWidth = 1000;
        private int _maxHeight = 1000;

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

        public char SpaceChar
        {
            get { return _spaceChar; }
        }

        public char LineChar
        {
            get { return _lineChar; }
        }

        // Looks for all Classes in the current assembly which inherit from CanvasCommand, instantiates them and adds them to the list of supported commands
        public void LoadSupportedCommands()
        {        
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(CanvasCommand)))
                {
                    CanvasCommand canvasCommand = (CanvasCommand)Activator.CreateInstance(type);
                    canvasCommand.SetCanvas(this);
                    _commandList.Add(canvasCommand.SupportedCommand, canvasCommand);
                }
            }
        }

        // Either creates a canvas for the first time or resets all the data
        public void RefreshCanvasData(int requiredWidth, int requiredHeight)
        {
            _canvasData = new int[requiredWidth, requiredHeight];
            _colourList = new Dictionary<int, char>();
            _colourList.Add(0, _spaceChar); // Add in the spaces
        }

        public int GetColourKeyFor(char colour)
        {
            int colourKey;

            // Don't want to map the same colour twice so check if we have already used it
            if (_colourList.ContainsValue(colour))
            {
                colourKey = _colourList.Keys.OfType<int>().FirstOrDefault(a => (_colourList[a] == colour));
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
        
        // Important method because additional commands can be supported by including new subclasses of CanvasCommand
        public string GetUsage(string newLineChar)
        {
            StringBuilder usageString = new StringBuilder("");

            foreach(char key in _commandList.Keys)
            {
                CanvasCommand canvasCommand = _commandList[key];
                usageString.Append(newLineChar + canvasCommand.Usage);
            }

            return usageString.ToString();
        }
        
        public void ExecuteCommand(string fullCommand)
        {
            char mainCommand = char.ToUpper(fullCommand.First<char>());

            if (_commandList.ContainsKey(mainCommand))                
            {
                CanvasCommand canvasCommand = _commandList[mainCommand];
                    
                canvasCommand.ProcessCommand(fullCommand);                                     
            }
            else
                throw new CommandException(fullCommand + " is an unrecognised command.");
        }

        public string ToString(string newLineChar)
        {
            if (!HasCanvasBeenCreated())
                throw new CommandException("Please create a canvas first.");

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
                    char colour = _colourList[_canvasData[i, j]];
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
    }
}