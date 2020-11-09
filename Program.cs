/*
Program.cs

This is the main program class.
The user inputs the desired dimensions as well as number of generations for the simulation to run. The mainGrid is initialized
off of these inputs, and then randomly populated. While loops are combined with try catches to force correct inputs from the user.
A for loop sequentially runs the grid's PopulateInterim, NextGeneration, and Display functions in that order. The user is then prompted
to press enter to show the next generation, running the functions again. This repeats according to the inputted amount of simulations.
*/
using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dimensions
            int gridRows=0;
            int gridColumns=0;
            //Boolean used for input while loops
            bool badInput = true;
            //User inputs amount of rows for the grid, while loop + try catch ensure correct input
            while (badInput == true)
            {
                try
                {
                    Console.WriteLine("How many rows would you like the grid to have?");
                    gridRows = Convert.ToInt32(Console.ReadLine());
                    if (gridRows > 0)
                    {
                        badInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Your input was 0 or less. It must be positive.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Input a positive integer.");
                }
            }
            //User inputs amount of columns for the grid, while loop + try catch ensure correct input
            badInput = true;
            while (badInput == true)
            {
                try
                {
                    Console.WriteLine("How many columns would you like the grid to have?");
                    gridColumns = Convert.ToInt32(Console.ReadLine());
                    if (gridColumns > 0)
                    {
                        badInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Your input was 0 or less. It must be positive.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Input a positive integer.");
                }
            }
            //Initialize grid, populate with first generation, and display grid 
            Grid mainGrid = new Grid(gridRows,gridColumns);
            mainGrid.PopulateRandom();
            Console.WriteLine("Here is Generation 1:");
            Console.WriteLine();
            mainGrid.Display();
            Console.WriteLine();
            //Get desired amount of simulations, using while loop + try/catch to ensure correct input
            badInput = true;
            Console.WriteLine("Press enter to begin the simulation. Once the simulation begins, press esc to stop.");
            Console.ReadLine();
            Console.WriteLine("Simulating next generation...");
            //While loop to run the simulation until the user presses esc
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                mainGrid.PopulateInterim();
                mainGrid.NextGeneration();
                Console.Clear();
                mainGrid.Display();
                Console.WriteLine("");
                Console.WriteLine("Simulating next generation...");
                Thread.Sleep(750);
            }
            Console.WriteLine();
            Console.WriteLine("The simulation has ended. Press enter to exit.");
            Console.ReadLine();
            //Test test = new Test();
        }
    }
}
