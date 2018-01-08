Console Drawing Program
=======================

This program is used to drawing lines and rectangles on the console.

Run from the console by executing the command "ConsoleDrawing.exe".

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

Below is a sample run of the program:

enter command: C 20 4
----------------------
|                    |
|                    |
|                    |
|                    |
----------------------

enter command: L 1 2 6 2
----------------------
|                    |
|xxxxxx              |
|                    |
|                    |
----------------------

enter command: L 6 3 6 4
----------------------
|                    |
|xxxxxx              |
|     x              |
|     x              |
----------------------

enter command: R 16 1 20 3
----------------------
|               xxxxx|
|xxxxxx         x   x|
|     x         xxxxx|
|     x              |
----------------------

enter command: B 10 3 o
----------------------
|oooooooooooooooxxxxx|
|xxxxxxooooooooox   x|
|     xoooooooooxxxxx|
|     xoooooooooooooo|
----------------------


Design
======

The program is comprised of 2 main classes.  

CanvasCommandProcessor is used to process the command entered by the user to create a canvas, draw lines, rectangles and
fill in spaces.  It is an abstract class and needs to be subclassed so that ProcessInputs() can be implemented.
The implementation of ProcessInputs() will depend on the interaction with the user. 
ConsoleCanvasCommandProcessor is used for interacting with a user via the console.

The other class is Canvas which is used for managing the canvas the user is drawing.  Again it is an abstract class
because the Display() method depends on the view being used.  ConsoleCanvas is a subclass for displaying on a console.

When creating an instance of ConsoleCanvasCommandProcessor, an instance of Canvas need to be injected.  This gives
flexibility in how the canvas is displayed to the user.  For this program, it is displayed directly on the console but
even though the input is via a console, Display() could be implemented for writing out to a file, for example.

For testing purposes, TestCanvas is subclassed off of Canvas to display the string that is created for rendering 
purposes.  This string can be tested against the expected string to ensure the Canvas class is working correctly.


Large canvas size?
Command history - could be big
Random mode running commands
Complex shapes
Min size
Clear canvas

