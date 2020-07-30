/*
Test.cs
 
Testing class to test every possible function with good and bad data
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Test
    {
        public Test()
        {
            TestGrid();
            TestCell();
        }
        public void TestGrid()
        {

            //Grid Test A: Good data - simple, should always succeed
            Console.WriteLine("Grid Test A");
            try
            {
                Grid testingGridA = new Grid(5, 5);
                testingGridA.PopulateRandom();
                testingGridA.Display();
                testingGridA.PopulateInterim();
                testingGridA.NextGeneration();
                Console.WriteLine();
                testingGridA.Display();
                Console.WriteLine("Grid Test A Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Grid Test A Failed");
            }

            //Grid Test B: Good data - Zero dimensions - shouldn't throw and error but wouldn't display anything
            Console.WriteLine("Grid Test B");
            try
            {
                Grid testingGridB = new Grid(0, 0);
                testingGridB.PopulateRandom();
                testingGridB.Display();
                testingGridB.PopulateInterim();
                testingGridB.NextGeneration();
                testingGridB.Display();
                Console.WriteLine("Grid Test B Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Grid Test B Failed");
            }

            //Grid Test C: Bad data - Negative dimensions - should fail
            Console.WriteLine("Grid Test C");
            try
            {
                Grid testingGridC = new Grid(-2,-4);
                testingGridC.PopulateRandom();
                testingGridC.Display();
                testingGridC.PopulateInterim();
                testingGridC.NextGeneration();
                testingGridC.Display();
                Console.WriteLine("Grid Test C Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Grid Test C Failed");
            }

            //Grid Test D: Good data - large dimensions - should succeed albeit probably will be rather slow
            Console.WriteLine("Grid Test D");
            try
            {
                Grid testingGridD= new Grid(250, 250);
                testingGridD.PopulateRandom();
                testingGridD.Display();
                testingGridD.PopulateInterim();
                testingGridD.NextGeneration();
                testingGridD.Display();
                Console.WriteLine("Grid Test D Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Grid Test D Failed");
            }
        }
        public void TestCell()
        {
            //Cell Test A: Good Data
            Console.WriteLine("Cell Test A");
            try
            {
                Cell[,] testingArrayA = new Cell[8, 8];
                //Populate testingArrayA completely with living cells
                for (int i = 0; i< 8; i++)
                {
                    for (int j = 0; j<8; j++)
                    {
                        testingArrayA[i, j] = new Cell(1, i, j);
                    }
                }
                //Set a specific cell to dead
                testingArrayA[2, 2].SetStatus(0);
                //Should print 0
                Console.WriteLine(testingArrayA[2, 2].GetStatus());
                //Populate neighbours array for that cell, get and print new state (should be 0)
                testingArrayA[2, 2].PopulateNeighbours(testingArrayA);
                Console.WriteLine(testingArrayA[2, 2].GetStatus());
                //Print coordinates (should be 2 and 2)
                Console.WriteLine(testingArrayA[2, 2].GetRowLocation());
                Console.WriteLine(testingArrayA[2, 2].GetColumnLocation());
                //Testing what state the ApplyRules function will return
                //Should return a 0, because at this point the cell is dead and all surrounding neighbours are alive
                Console.WriteLine(testingArrayA[2, 2].CheckNeighbours());
                Console.WriteLine("Cell Test A Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Cell Test A Failed");
            }
            //Cell Test B: Bad Data - same test as before, but without actually putting cells in the cell array - should fail
            {
                try
                {
                    Cell[,] testingArrayB = new Cell[8, 8];
                    testingArrayB[2, 2].SetStatus(0);
                    Console.WriteLine(testingArrayB[2, 2].GetStatus());
                    testingArrayB[2, 2].PopulateNeighbours(testingArrayB);
                    Console.WriteLine(testingArrayB[2, 2].GetStatus());
                    Console.WriteLine(testingArrayB[2, 2].GetRowLocation());
                    Console.WriteLine(testingArrayB[2, 2].GetColumnLocation());
                    Console.WriteLine(testingArrayB[2, 2].CheckNeighbours());
                    Console.WriteLine("Cell Test B Successful");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Cell Test B Failed");
                }
            }
            //Cell Test C: Testing the ApplyRules function
            {
                try
                {
                    Cell liveCell = new Cell(1, 0, 5);
                    Cell deadCell = new Cell(0, 4, 7);
                    //Testing every possible amount of neighbours for the alive cell
                    //Should print 00110000
                    Console.Write(liveCell.ApplyRules(0));
                    Console.Write(liveCell.ApplyRules(1));
                    Console.Write(liveCell.ApplyRules(2));
                    Console.Write(liveCell.ApplyRules(3));
                    Console.Write(liveCell.ApplyRules(4));
                    Console.Write(liveCell.ApplyRules(5));
                    Console.Write(liveCell.ApplyRules(6));
                    Console.Write(liveCell.ApplyRules(7));
                    Console.Write(liveCell.ApplyRules(8));
                    Console.WriteLine();
                    //Testing every possible amount of neighbours for the dead cell
                    //Should print 000100000
                    Console.Write(deadCell.ApplyRules(0));
                    Console.Write(deadCell.ApplyRules(1));
                    Console.Write(deadCell.ApplyRules(2));
                    Console.Write(deadCell.ApplyRules(3));
                    Console.Write(deadCell.ApplyRules(4));
                    Console.Write(deadCell.ApplyRules(5));
                    Console.Write(deadCell.ApplyRules(6));
                    Console.Write(deadCell.ApplyRules(7));
                    Console.Write(deadCell.ApplyRules(8));
                    Console.WriteLine();
                    /*Testing passing in large positive and large negative numbers
                    These are not technically valid values for # of living neighbours, because logically you can have no more than 8 and no
                    less than 0 living neighbours
                    Regardless, these should all technically still work, as the function is set to return the original state of the cell
                    if it can't find any rules to apply, and some rules can still apply when you pass very large positive/negatives in
                    */
                    /*
                     Should return 00
                     This is because the overpopulation rule means that a live cell with >3 neighbours dies, and 10000000 is >3
                     Same applies because the underpopulation rule means that a live cell with <2 neighbours dies, and -10000000 is <3
                    */
                    Console.Write(liveCell.ApplyRules(10000000));
                    Console.Write(liveCell.ApplyRules(-10000900));
                    Console.WriteLine();
                    //Should return 00, because the only way the state of a dead cell changes is if the # of live neighbours is exactly 3
                    Console.Write(liveCell.ApplyRules(10000000));
                    Console.Write(liveCell.ApplyRules(-10000000));
                    Console.WriteLine();
                    Console.WriteLine("Cell Test C Successful");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Cell Test C Failed");
                }
                
            }
        }
    }
}
