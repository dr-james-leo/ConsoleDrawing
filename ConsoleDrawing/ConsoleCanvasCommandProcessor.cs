﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawing
{
    // Reads inputs from the console and passes them onto the canvas for processing.
    // If the user types Q then quits the program
    // If the user type ? then displays the support commands
    public class ConsoleCanvasCommandProcessor
    {
        protected Canvas _canvas;

        public ConsoleCanvasCommandProcessor()
        {
            _canvas = new Canvas();
        }

        public void ProcessInputs()
        {
            while (true)
            {
                Console.Write("\nenter command: ");

                string inputString = Console.ReadLine();

                if (!ProcessInputLine(inputString))
                    break;               
            }
        }

        // Returns true to continue and false to stop
        private bool ProcessInputLine(string fullCommand)
        {            
            try
            {
                fullCommand = fullCommand.Trim();

                if (fullCommand.Length == 0)
                {
                    Console.WriteLine("Please enter a command.");
                    return true;
                }

                char mainCommand = char.ToUpper(fullCommand.First<char>());

                switch (mainCommand)
                {
                    case 'Q':
                        return false;

                    case '?':
                        Console.WriteLine(_canvas.GetUsage("\n"));
                        break;

                    default:
                        _canvas.ExecuteCommand(fullCommand);                       
                        Console.WriteLine(_canvas.ToString("\n"));
                        break;
                }
            }
            catch(CommandException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown error ocurred: " + ex.Message);
            }

            return true;
        }
    }
}
