using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    public class RectangleCanvasCommand : LineCanvasCommand
    {      
        public override char SupportedCommand
        {
            get { return 'R'; }
        }

        public override string Usage
        {
            get { return "R x1 y1 x2 y2 - Add a rectangle whose upper left corner is (x1, y1) and lower right corner is (x2, y2)"; }
        }

        public override int NumberOfParameters
        {
            get { return 5; }
        }

        // Return true is successful and false on error
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

            if (x2 < x1)
                throw new CommandException("x2 must be greater than x1.");          

            if (y2 < y1)
                throw new CommandException("y2 must be greater than y1.");
           
            int colourKey = _canvas.GetColourKeyFor('x');

            int rectangleWidth = x2 - x1 + 1;
            int rectangleHeight = y2 - y1 + 1;
            AddRectangle(x1, y1, rectangleWidth, rectangleHeight, colourKey);            
        }

        // Return true is successful and false on error
        public void AddRectangle(int x, int y, int width, int height, int colourKey)
        {          
            AddHorizontalLine(x, y, width, colourKey);
            AddVerticalLine(x, y, height, colourKey);
            AddVerticalLine(x + width - 1, y, height, colourKey);
            AddHorizontalLine(x, y + height - 1, width, colourKey);  
        }

    }
}
