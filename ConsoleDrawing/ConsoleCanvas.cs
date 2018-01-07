using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    public class ConsoleCanvas: Canvas
    {
        public ConsoleCanvas() : base()
        {
        }

        public ConsoleCanvas(int maxWidth, int maxHeight) : base(maxWidth, maxHeight)
        {
        }

        public ConsoleCanvas(char spaceChar, char lineChar, char topAndBottomEdgeChar, char leftAndRightEdgeChar, int maxWidth, int maxHeight) : base(spaceChar, lineChar, topAndBottomEdgeChar, leftAndRightEdgeChar, maxWidth, maxHeight)
        {
        }

        public override int Display()
        {
            string canvasAsString = RenderToString("\n");
            if (canvasAsString.Length == 0)
                return -1;

            Console.WriteLine(canvasAsString);
            return 1;
        }
    }
}
