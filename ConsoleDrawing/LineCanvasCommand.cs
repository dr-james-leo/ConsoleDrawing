using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    // Draws vertical and horizontal lines
    public class LineCanvasCommand : CanvasCommand
    {
        protected const int _minLength = 2; // A line must be at least 2 pixels long
        
        public override char SupportedCommand
        {
            get { return 'L'; }
        }

        public override string Usage
        {
            get { return "L x1 y1 x2 y2 - Add a new line for (x1, y1) to (x2, y2)"; }
        }

        public override int NumberOfParameters
        {
            get{ return 5; }
        }

        private void CheckLineLongEnough(int length)
        {
            if (length < _minLength)
                throw new CommandException("Line must be at least " + _minLength + " long.");               
        }

        protected void AddHorizontalLine(int x, int y, int length, int colourKey)
        {           
            CheckXAndYWithinBounds(x, y);
            CheckLineLongEnough(length);
                
            if ((x + length - 1) > _canvas.CanvasWidth)
                throw new CommandException("Length of line too long.");
           
            int[,] canvasData = _canvas.CanvasData;

            for (int i = 0; i < length; i++)
                canvasData[x - 1 + i, y - 1] = colourKey;         
        }

        protected void AddVerticalLine(int x, int y, int length, int colourKey)
        {
            CheckXAndYWithinBounds(x, y);
            CheckLineLongEnough(length);

            if ((y + length - 1) > _canvas.CanvasHeight)
                throw new CommandException("Length of line too long.");

            int[,] canvasData = _canvas.CanvasData;

            for (int j = 0; j < length; j++)
                canvasData[x - 1, y - 1 + j] = colourKey;      
        }

        public override void ProcessCommand(string fullCommand)
        {          
            if (!_canvas.HasCanvasBeenCreated())
                throw new CommandException("Please create a canvas first.");
                  
            string[] elements = GetParameters(fullCommand);                

            int x1;
            if (!int.TryParse(elements[1], out x1))
                throw new CommandException("First parameter must be an integer specifying x1.");
           
            int y1;
            if (!int.TryParse(elements[2], out y1))
                throw new CommandException("Second parameter must be an integer specifying y1.");
          
            int x2;
            if (!int.TryParse(elements[3], out x2))
                throw new CommandException("Third parameter must be an integer specifying x2.");
           
            int y2;
            if (!int.TryParse(elements[4], out y2))
                throw new CommandException("Fourth parameter must be an integer specifying y2.");
           
            int colourKey = _canvas.GetColourKeyFor(_canvas.LineChar);

            int length;
            if (y1 == y2)
            {
                int x = x1;
                if (x1 > x2)
                    x = x2;

                length = Math.Abs(x2 - x1) + 1;

                AddHorizontalLine(x, y1, length, colourKey);                 
            }
            else
            {
                if (x1 == x2)
                {
                    int y = y1;
                    if (y1 > y2)
                        y = y2;
                    length = Math.Abs(y2 - y1) + 1;

                    AddVerticalLine(x1, y, length, colourKey);
                }
                else
                {
                    throw new CommandException("Either x1 must equal x2 for a vertical line or y1 must equal y2 for a horizontal line.");
                }
            }                 
        }
    }
}
