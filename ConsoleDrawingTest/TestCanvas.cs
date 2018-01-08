using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDrawing;

namespace ConsoleDrawingTest
{
    public class TestCanvas : Canvas
    {
        private string _displayAsString;

        public TestCanvas() : base()
        {
        }

        public TestCanvas(int maxWidth, int maxHeight) : base(maxWidth, maxHeight)
        {
        }

        public TestCanvas(char spaceChar, char lineChar, char topAndBottomEdgeChar, char leftAndRightEdgeChar, int maxWidth, int maxHeight) : base(spaceChar, lineChar, topAndBottomEdgeChar, leftAndRightEdgeChar, maxWidth, maxHeight)
        {
        }

        public string DisplayAsString
        {
            get { return _displayAsString; }
        }

        public override bool Display()
        {
            _displayAsString = RenderToString("");
            if (_displayAsString.Length == 0)
                return false;

            return true;
        }
    }
}
