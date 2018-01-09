using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    public class FillCanvasCommand: CanvasCommand
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
                if (!_canvas.HasCanvasBeenCreated())
                {
                    _errorString = "Please create a canvas first.";
                    return false;
                }

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

                if (!Fill(x, y, colour))
                {
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

        // Fills the canvas with the specified colour from position x,y
        // Return true is successful and false on error
        public bool Fill(int x, int y, string colour)
        {
            _errorString = "";

            try
            {
                if (!UndertakeSanityCheck(x, y))
                    return false;

                int colourKey;
                Hashtable colourMap = _canvas.GetColourMap();

                // Don't want to map the same colour twice so check if we have already used it
                if (colourMap.ContainsValue(colour))
                {
                    colourKey = colourMap.Keys.OfType<int>().FirstOrDefault(a => (string)colourMap[a] == colour);
                }
                else
                {
                    colourKey = 2 + colourMap.Count;
                    colourMap.Add(colourKey, colour);
                }

                if (!FillCell(x, y, colourKey))
                    return false;

                return true;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        // Ensures the fill routine doesn't mpve outside the canvas size
        private bool isWithinBounds(int x, int y)
        {
            if (x < 1 || x > _canvas.GetCanvasWidth() || y < 1 || y > _canvas.GetCanvasHeight())
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
                int[,] canvasData = _canvas.GetCanvasData();

                if (isWithinBounds(x, y) && canvasData[x - 1, y - 1] == 0)
                {
                    canvasData[x - 1, y - 1] = colourKey;
                    if (!FillNeighbours(x, y, colourKey))
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
