using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleDrawing;

namespace ConsoleDrawingTest
{
    [TestClass]
    public class CanvasConsoleTests
    {
        [TestMethod]
        public void TestCreatingCanvas()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);
            
            // Check can create a canvas
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("C 4 3"));
            Assert.AreEqual("------\n|    |\n|    |\n|    |\n------", canvas.RenderToString("\n"));

            // Now check can't create canvas with bad parameters
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C a 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C 4 b"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C a b"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C -1 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C 0 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C 20 0"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C 4 3 3"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("C 100000 5"));

            Assert.AreEqual(0, consoleCommandProcessor.ProcessInputLine("Q"));
        }

        [TestMethod]
        public void TestAddingLine()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("C 4 3"));
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("L 1 1 4 1"));
            Assert.AreEqual("------\n|xxxx|\n|    |\n|    |\n------", canvas.RenderToString("\n"));
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("L 1 1 1 3"));
            Assert.AreEqual("------\n|xxxx|\n|x   |\n|x   |\n------", canvas.RenderToString("\n"));

            // Now check can't add line with bad parameters
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L 1 2 2 3"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L -2 3 4 6"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L c 1 4 1"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L 1 1 4 1 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L 1 2 3 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("L 1 1 10 1"));
        }

        [TestMethod]
        public void TestAddingRectangle()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("C 4 4"));
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("R 2 2 3 3"));
            Assert.AreEqual("------\n|    |\n| xx |\n| xx |\n|    |\n------", canvas.RenderToString("\n"));

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("R 2 2 5 5"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("R -2 2 3 3"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("R 0 1 4 1"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("R 1 1 4 1 4"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("R 1 2 6 4"));
        }

        [TestMethod]
        public void TestFillingCanvas()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("C 4 4"));
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("R 2 2 3 3"));
            Assert.AreEqual(1, consoleCommandProcessor.ProcessInputLine("B 1 1 W"));
            Assert.AreEqual("------\n|WWWW|\n|WxxW|\n|WxxW|\n|WWWW|\n------", canvas.RenderToString("\n"));

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("B 1 1 WW"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("B -2 1 W"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("B 0 1 W"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("B 1 1 W 2"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("B W 1 1"));
        }

        [TestMethod]
        public void TestInvalidCommands()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("c 4 3"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("l 1 1 4 1"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("q"));
            Assert.AreEqual(-1, consoleCommandProcessor.ProcessInputLine("F 1 1 1 1"));
        }


    }
}
