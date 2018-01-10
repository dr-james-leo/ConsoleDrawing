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
        public FillCanvasCommand()
        {
        }

        private int _spaceColourKey;
        private int _canvasWidth;
        private int _canvasHeight;
        private int[,] _canvasData;
        private int _colourKey;

        public override char SupportedCommand
        {
            get{return 'B';}
        }

        public override string Usage
        {
            get { return "B x y c - Fill entire area connect to (x, y) with colour c"; }
        }

        public override int NumberOfParameters
        {
            get { return 4; }
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

                string[] elements = GetParameters(fullCommand);
                if (elements == null)
                    return false;

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

                char colour;
                if (elements[3].Length > 1)
                {
                    _errorString = "Third parameter must be a single character specifying the colour.";
                    return false;
                }
                else
                    colour = elements[3][0];

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
        public bool Fill(int x, int y, char colour)
        {
            _errorString = "";

            try
            {
                if (!areXAndYWithinBounds(x, y))
                    return false;
                
                // set up some private attributes to speed up execution
                _colourKey = _canvas.GetColourKeyFor(colour);
                _spaceColourKey = _canvas.GetColourKeyFor(_canvas.SpaceChar);
                _canvasWidth = _canvas.CanvasWidth;
                _canvasHeight = _canvas.CanvasHeight;
                _canvasData = _canvas.CanvasData;

                if (!FillCell(x, y))
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
        private bool FillNeighbours(int x, int y)
        {
            _errorString = "";

            try
            {
                if (!FillCell(x + 1, y))
                    return false;

                if (!FillCell(x - 1, y))
                    return false;

                if (!FillCell(x, y + 1))
                    return false;

                if (!FillCell(x, y - 1))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

        //// Ensures the fill routine doesn't mpve outside the canvas size
        //private bool isWithinBounds(int x, int y)
        //{
        //    if (x < 1 || x > _canvasWidth || y < 1 || y > _canvasHeight)
        //        return false;
        //    else
        //        return true;
        //}

        // Fills a cell with the specified colour and then tries to fill neighbours
        // Return true is successful and false on error
        private bool FillCell(int x, int y)
        {
            _errorString = "";

            try
            {
                if (areXAndYWithinBounds(x, y) && _canvasData[x - 1, y - 1] == _spaceColourKey)
                {
                    _canvasData[x - 1, y - 1] = _colourKey;
                    if (!FillNeighbours(x, y))
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
