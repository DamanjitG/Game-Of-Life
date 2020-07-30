/*
Grid.cs

Grid class. This class is for the grid, which is to "hold" the cells for the game of life.
One grid class actually contains two 2D arrays of cells; the cellArray and the interimArray.
The constructor takes in two integers, which become the dimensions of the cellArray and interimArray.
The PopulateRandom function populates every cell in the cellArray as being either alive or dead, then copies this over to the interimArray.
The Display function uses a nested for loop to write out and display the entire cellArray.
The NextGeneration function works off of the interimArray to change the state of every cell
in the cellArray to the appropriate state for the next generation.
The PopulateInterim function copies the cellArray into the interimArray.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Grid
    {
        //Dimension variables
        int rows;
        int columns;
        //Main and interim arrays
        Cell[,] cellArray;
        Cell[,] interimArray;
        //Random to use for random population of the grid
        Random rand = new Random();
        /*
         Constructor passes in dimensions r and c for rows and columns respectively
         Assigns the values of r and c to the rows and columns values
         Creates the cellArray and the interimArray using the dimension values
        */
        public Grid(int r, int c)
        {
            this.rows = r;
            this.columns = c;
            cellArray = new Cell[r, c];
            interimArray = new Cell[r, c];
        }
        /*
         Function to populate cellArray with random values, then copy those into the interimArray
         Uses a double for loop to run through the entire cellArray, generating a random state each time
         using Random.Next, then uses that state as well as the coordinates of the cell given by the for loop
         to create a cell in the index
        */
        public void PopulateRandom()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    int stateSetter = rand.Next(0, 2);
                    cellArray[i, j] = new Cell(stateSetter,i,j);
                    interimArray[i, j] = cellArray[i,j];
                }
            }
        }
        /*
         Function to display the grid
         Uses a nested for loop to run through the entire cellArray
         Uses the GetStatus function to output the status of each cell
         */
        public void Display()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0;j < this.columns; j++)
                {
                    Console.Write(cellArray[i, j].GetStatus()+" ");
                }
                Console.WriteLine();
            }
        }
        /*
         Function to create the next generation of the simulation
         First nested for loop populates the neighbours array of each cell in the interimArray, using the interimArray as a source
         The second nested for loop then sets the status of each cell in the cellArray using the CheckNeighbours function,
         passing in the corresponding cell in the interimArray
         This uses the interimArray as a "snapshot" of the previous generation, so that the program will manipulate each cell based on its
         neighbours in the previous generation
        */
        public void NextGeneration()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    this.interimArray[i, j].PopulateNeighbours(interimArray);
                }
            }
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    this.cellArray[i, j].SetStatus(this.interimArray[i, j].CheckNeighbours());
                }
            }
        }
        /*
         Function to populate the interimArray
         Uses a nested for loop to make every cell in the interimArray 
         equal to the cell with the same coordinates in the cellArray
        */
        public void PopulateInterim()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    this.interimArray[i, j] = this.cellArray[i, j];
                }
            }
        }
    }
}
