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
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            // Check can create a canvas
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("C 4 3"));
            Assert.AreEqual("------|    ||    ||    |------", testConsoleCommandProcessor.GetString());

            // Now check can't create canvas with bad parameters
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C a 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C 4 b"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C a b"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C -1 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C 0 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C 20 0"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C 4 3 3"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("C 100000 5"));
        }

        [TestMethod]
        public void TestAddingLine()
        {
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            // Check can create a canvas
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("C 4 3"));
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("L 1 1 4 1"));
            Assert.AreEqual("------|xxxx||    ||    |------", testConsoleCommandProcessor.GetString());
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("L 1 1 1 3"));
            Assert.AreEqual("------|xxxx||x   ||x   |------", testConsoleCommandProcessor.GetString());

            // Now check can't add line with bad parameters
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L 1 2 2 3"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L -2 3 4 6"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L c 1 4 1"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L 1 1 4 1 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L 1 2 3 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("L 1 1 10 1"));
        }

        [TestMethod]
        public void TestAddingRectangle()
        {
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            // Check can create a canvas
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("C 4 4"));
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("R 2 2 3 3"));
            Assert.AreEqual("------|    || xx || xx ||    |------", testConsoleCommandProcessor.GetString());

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("R 2 2 5 5"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("R -2 2 3 3"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("R 0 1 4 1"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("R 1 1 4 1 4"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("R 1 2 6 4"));
        }

        [TestMethod]
        public void TestFillingCanvas()
        {
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            // Check can create a canvas
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("C 4 4"));
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("R 2 2 3 3"));
            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("B 1 1 W"));
            Assert.AreEqual("------|WWWW||WxxW||WxxW||WWWW|------", testConsoleCommandProcessor.GetString());

            // Now check can't add rectangle with bad parameters
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("B 1 1 WW"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("B -2 1 W"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("B 0 1 W"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("B 1 1 W 2"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("B W 1 1"));
        }

        [TestMethod]
        public void TestInvalidCommands()
        {
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            // Check can create a canvas
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("t 4 3"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("p 1 1 4 1"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("W"));
            Assert.AreEqual(false, testConsoleCommandProcessor.TestInputLine("F 1 1 1 1"));
        }

        [TestMethod]
        public void EndToEndTest()
        {
            TestConsoleCommandProcessor testConsoleCommandProcessor = new TestConsoleCommandProcessor();

            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("C 20 4"));
            Assert.AreEqual("----------------------|                    ||                    ||                    ||                    |----------------------", testConsoleCommandProcessor.GetString());

            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("L 1 2 6 2"));
            Assert.AreEqual("----------------------|                    ||xxxxxx              ||                    ||                    |----------------------", testConsoleCommandProcessor.GetString());

            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("L 6 3 6 4"));
            Assert.AreEqual("----------------------|                    ||xxxxxx              ||     x              ||     x              |----------------------", testConsoleCommandProcessor.GetString());

            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("R 16 1 20 3"));
            Assert.AreEqual("----------------------|               xxxxx||xxxxxx         x   x||     x         xxxxx||     x              |----------------------", testConsoleCommandProcessor.GetString());

            Assert.AreEqual(true, testConsoleCommandProcessor.TestInputLine("B 10 3 o"));
            Assert.AreEqual("----------------------|oooooooooooooooxxxxx||xxxxxxooooooooox   x||     xoooooooooxxxxx||     xoooooooooooooo|----------------------", testConsoleCommandProcessor.GetString());

        }
    }
}
