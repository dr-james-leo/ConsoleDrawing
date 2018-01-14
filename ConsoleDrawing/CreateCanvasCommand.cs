using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleDrawing
{
    // Used to create a canvas of a specified height and width
    public class CreateCanvasCommand : CanvasCommand
    {
        public override char SupportedCommand
        {
            get { return 'C'; }
        }

        public override string Usage
        {
            get { return "C w h - Create a new canvas of width w and height h"; }
        }

        public override int NumberOfParameters
        {
            get { return 3; }
        }

        public override void ProcessCommand(string fullCommand)
        {
            string[] elements = GetParameters(fullCommand);

            int requestedWidth;
            if (!int.TryParse(elements[1], out requestedWidth))
                throw new CommandException("First parameter must be an integer specifying the width.");
           
            if (requestedWidth < 1)
                throw new CommandException("Width of canvas must be at least 1.");

            int requestedHeight;
            if (!int.TryParse(elements[2], out requestedHeight))
                throw new CommandException("Second parameter must be an integer specifying the height.");
            
            if (requestedHeight < 1)
                throw new CommandException("Height of canvas must be at least 1.");            

            if (requestedHeight > _canvas.MaxHeight)
                throw new CommandException("Maximum height of " + _canvas.MaxHeight + " exceeded.");
            
            if (requestedWidth > _canvas.MaxWidth)
                throw new CommandException("Maximum width of " + _canvas.MaxWidth + " exceeded.");
           
            _canvas.RefreshCanvasData(requestedWidth, requestedHeight);            
        }
    }
}
