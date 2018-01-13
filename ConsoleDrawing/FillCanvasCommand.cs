using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    //[CanvasCommand]
    public class FillCanvasCommand: CanvasCommand
    {
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
        public override void ProcessCommand(string fullCommand)
        {          
            if (!_canvas.HasCanvasBeenCreated())
                throw new CommandException("Please create a canvas first.");
               
            string[] elements = GetParameters(fullCommand);
           
            int x;
            if (!int.TryParse(elements[1], out x))
                throw new CommandException("First parameter must be an integer specifying x.");
            
            int y;
            if (!int.TryParse(elements[2], out y))
                throw new CommandException("Second parameter must be an integer specifying y.");

            CheckXAndYWithinBounds(x, y);
 
            char colour;
            if (elements[3].Length > 1)
                throw new CommandException("Third parameter must be a single character specifying the colour.");

            colour = elements[3][0];

            // set up some private attributes to speed up execution
            _colourKey = _canvas.GetColourKeyFor(colour);
            _spaceColourKey = _canvas.GetColourKeyFor(_canvas.SpaceChar);
            _canvasWidth = _canvas.CanvasWidth;
            _canvasHeight = _canvas.CanvasHeight;
            _canvasData = _canvas.CanvasData;

            FillCell(x, y);    
        }

        // Used to fill the 4 neighbours of a cell, above, below, left and right
        private void FillNeighbours(int x, int y)
        {
            FillCell(x + 1, y);
            FillCell(x - 1, y);
            FillCell(x, y + 1);
            FillCell(x, y - 1);        
        }

        // Fills a cell with the specified colour and then tries to fill neighbours
        private void FillCell(int x, int y)
        {
            if (x >= 1 && x <= _canvasWidth && y >= 1 && y <= _canvasHeight && _canvasData[x - 1, y - 1] == _spaceColourKey)
            {
                _canvasData[x - 1, y - 1] = _colourKey;
                FillNeighbours(x, y);                     
            }          
        }
    }
}
