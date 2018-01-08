﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleDrawing
{
    public class LineCanvasCommand : CanvasCommand
    {
        public LineCanvasCommand(Canvas iCanvas) : base(iCanvas)
        {
        }

        public override char SupportedCommand()
        {
            return 'L';
        }

        public override string GetUsage()
        {
            return "L x1 y1 x2 y2 - Add a new line for (x1, y1) to (x2, y2)";
        }

        public override int GetNumberOfParameters()
        {
            return 5;
        }

        // Return true is successful and false on error
        public override bool ProcessCommand(string fullCommand)
        {
            _errorString = "";

            try
            {
                if (!isNumberOfParametersOK(fullCommand))
                    return false;

                string pattern = @"\s+";
                string[] elements = Regex.Split(fullCommand, pattern);

                int x1;
                if (!int.TryParse(elements[1], out x1))
                {
                    _errorString = "First parameter must be an integer specifying x1.";
                    return false;
                }

                int y1;
                if (!int.TryParse(elements[2], out y1))
                {
                    _errorString = "Second parameter must be an integer specifying y1.";
                    return false;
                }

                int x2;
                if (!int.TryParse(elements[3], out x2))
                {
                    _errorString = "First parameter must be an integer specifying x2.";
                    return false;
                }

                int y2;
                if (!int.TryParse(elements[4], out y2))
                {
                    _errorString = "Second parameter must be an integer specifying y2.";
                    return false;
                }

                int length;
                if (y1 == y2)
                {
                    int x = x1;
                    if (x1 > x2)
                        x = x2;
                    length = Math.Abs(x2 - x1) + 1;
                    if (!canvas.AddHorizontalLine(x, y1, length))
                    {
                        _errorString = canvas.Error;
                        return false;
                    }
                }
                else
                {
                    if (x1 == x2)
                    {
                        int y = y1;
                        if (y1 > y2)
                            y = y2;
                        length = Math.Abs(y2 - y1) + 1;
                        if (!canvas.AddVerticalLine(x1, y, length))
                        {
                            _errorString = canvas.Error;
                            return false;
                        }
                    }
                    else
                    {
                        _errorString = "Either x1 must equal x2 for a vertical line or y1 must equal y2 for a horizontal line.";
                        return false;
                    }
                }

                if (!canvas.Display())
                {
                    _errorString = canvas.Error;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _errorString = "An error ocurred: " + ex.Message;
                return false;
            }
        }

    }
}