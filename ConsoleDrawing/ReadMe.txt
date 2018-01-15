Console Drawing Program
=======================

This program is used for drawing lines and rectangles on the console.

Run from the console by executing the command "ConsoleDrawing.exe".

Type '?' to see the usage of available commands.

First create a canvas of the specified width w and height h using the command 'C w h'.

Note that for the following commands, x specifies the horizontal axis and goes from 1 to w where 1 is the left most point.
y specifies the vertical axis and goes from 1 to h where 1 is the top most point.

To create and draw a line from (x1, y1) to (x2, y2) execute the command 'L x1 y1 x2 y2'.
Note that only horizontal and vertical lines are supported so either x1 must equal x2 for vertical lines or 
y1 must equal y2 for horizontal lines.  If either of these conditions are not met then an error will be shown.

Use the command 'R x1 y1 x2 y2' to create and draw a rectangle from the point (x1, y1) to (x2, y2).
Note that x1, y1 specifies the upper left hand corner whereas x2, y2 specifies the lower right corner.

By default lines are drawn using the 'x' character.

The command 'B x y c' is used to fill areas starting at the point (x, y) with the character specified by c.             
              
The command 'Q' quits the program.


Design
======

The brief was to write a simple program that is extensible. Extensible because of the phrase "At this time, the functionality
of the program is quite limited but this might change in the future".  I therefore created the abstract base class CanvasCommand
from which classes which implement different commands inherit from.  
The 4 commands, 'C', 'L', 'R' and 'B' are implemented in CreateCanvasCommand, LineCanvasCommand, RectangleCanvasCommand
and FillCanvasCommand respectively.  

The Canvas class is used for managing the CanvasCommand classes and the canvas data.  If a new command is required to be implemented 
then a new class needs to be created inheriting from CanvasCommand.
No change to the existing code is required because all the CanvasCommand classes are loaded up dynamically at runtime in the method
LoadSupportedCommands().  The handy GetUsage() method can be run to find the supported commands.

It was decided to store the shapes directly on the canvas (which is actually a 2x2 array of integers) rather than storing them as shapes.
This simplifies the drawing and the algorithm for the filling.  This does mean some memory will need to be allocated even
though it isn't being used as the canvas will be mainly blank in the early stages.  This is not an issue For canvases that can be displayed
on the size of a console.  This may not be the best stragegy for very large canvases. There is therefore a trade off between simplicity and efficient 
use of memory. The focus for this program is simplicity.

Although this program is targeted for use as a console application, the only reference to the console is in ConsoleCanvasCommandProcessor.
This allows reusability of the canvas functionality in another environment like the web.

Tests
=====

For testing purposes, the TestConsoleCommandProcessor class is used to execute commands on Canvas and to display the canvas.