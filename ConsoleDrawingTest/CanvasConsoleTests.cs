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
            TestCanvas canvas = new TestCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);
            
            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("C 4 3"));
            Assert.AreEqual("------|    ||    ||    |------", canvas.DisplayAsString);

            // Now check can't create canvas with bad parameters
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C a 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C 4 b"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C a b"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C -1 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C 0 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C 20 0"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C 4 3 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("C 100000 5"));

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Stop, consoleCommandProcessor.ProcessInputLine("Q"));
        }

        [TestMethod]
        public void TestAddingLine()
        {
            TestCanvas canvas = new TestCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("C 4 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("L 1 1 4 1"));
            Assert.AreEqual("------|xxxx||    ||    |------", canvas.DisplayAsString);
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("L 1 1 1 3"));
            Assert.AreEqual("------|xxxx||x   ||x   |------", canvas.DisplayAsString);

            // Now check can't add line with bad parameters
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L 1 2 2 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L -2 3 4 6"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L c 1 4 1"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L 1 1 4 1 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L 1 2 3 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("L 1 1 10 1"));
        }

        [TestMethod]
        public void TestAddingRectangle()
        {
            TestCanvas canvas = new TestCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("C 4 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("R 2 2 3 3"));
            Assert.AreEqual("------|    || xx || xx ||    |------", canvas.DisplayAsString);

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("R 2 2 5 5"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("R -2 2 3 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("R 0 1 4 1"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("R 1 1 4 1 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("R 1 2 6 4"));
        }

        [TestMethod]
        public void TestFillingCanvas()
        {
            TestCanvas canvas = new TestCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("C 4 4"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("R 2 2 3 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("B 1 1 W"));
            Assert.AreEqual("------|WWWW||WxxW||WxxW||WWWW|------", canvas.DisplayAsString);

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("B 1 1 WW"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("B -2 1 W"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("B 0 1 W"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("B 1 1 W 2"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("B W 1 1"));
        }

        [TestMethod]
        public void TestInvalidCommands()
        {
            ConsoleCanvas canvas = new ConsoleCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("c 4 3"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("l 1 1 4 1"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("q"));
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Error, consoleCommandProcessor.ProcessInputLine("F 1 1 1 1"));
        }

        [TestMethod]
        public void EndToEndTest()
        {
            TestCanvas canvas = new TestCanvas();
            ConsoleCanvasCommandProcessor consoleCommandProcessor = new ConsoleCanvasCommandProcessor(canvas);

            // Check can create a canvas
            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("C 20 4"));
            Assert.AreEqual("----------------------|                    ||                    ||                    ||                    |----------------------", canvas.DisplayAsString);

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("L 1 2 6 2"));
            Assert.AreEqual("----------------------|                    ||xxxxxx              ||                    ||                    |----------------------", canvas.DisplayAsString);

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("L 6 3 6 4"));
            Assert.AreEqual("----------------------|                    ||xxxxxx              ||     x              ||     x              |----------------------", canvas.DisplayAsString);

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("R 16 1 20 3"));
            Assert.AreEqual("----------------------|               xxxxx||xxxxxx         x   x||     x         xxxxx||     x              |----------------------", canvas.DisplayAsString);

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.OK, consoleCommandProcessor.ProcessInputLine("B 10 3 o"));
            Assert.AreEqual("----------------------|oooooooooooooooxxxxx||xxxxxxooooooooox   x||     xoooooooooxxxxx||     xoooooooooooooo|----------------------", canvas.DisplayAsString);

            Assert.AreEqual(CanvasCommandProcessor.ReturnCodes.Stop, consoleCommandProcessor.ProcessInputLine("Q"));
        }
    }
}
