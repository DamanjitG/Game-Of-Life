/*
Cell.cs

This class is for a cell, which is the core element of the game of life. The game is based off of all the cells and their states.
Each cell has a neighbours array, which is an array of integers. It is intended to hold the states of the 8 neighbours of the cell.
If a cell is on the edge or corner of the grid and thus has <8 neighbours, the non existent neighbours essentially count as dead ones.
The constructor takes in 3 ints. The first is for the state of the cell, and the second and third are the coordinates of the cell
within the grid. It is important to note that in this program, a cell cannot effectively exist outside of a grid.
SetStatus sets the status of the cell to the inputted integer.
GetStatus, GetRowLocation, and GetColumnLocation are getter functions which return the respective value for the cell.
GetNeighbourStatus returns the status of a specific neighbour.
PopulateNeighbours uses the GetNeighbourStatus to populate the neighbours array with the statuses of the cell's neighbours.
ApplyRules takes in an int for the current state of a cell, and an int for the number of living neighbours it has, 
and uses that information to apply the rules of the game of life and determine what the new state of the cell should be.
CheckNeighbours checks the neighbours array to calculate the amount of living neighbours the cell has, then passes that along with the 
current state of the cell into the ApplyRules function to determine the state of the cell in the next generation.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        //Holds the state of the cell, should always be 1(alive) or 0(dead)
        private int state;
        //Int array which will hold the status of the neighbours of the cell
        private int[] neighbours = new int[8];
        //Coordinates of the cell in the grid
        private int xLocation;
        private int yLocation;
        /*
         The constructor takes in 3 inputs; one for state, one for row location, and one for column location
         It assigns the values of these inputs to their respective variables
         It then populates the neighbours array for the cell with 0s as temporary placeholders
        */
        public Cell(int stateInput,int rowInput, int columnInput)
        {
            this.state = stateInput;
            this.xLocation = rowInput;
            this.yLocation = columnInput;
            for (int i = 0; i < 8; i++)
            {
                this.neighbours[i] = 0;
            }
        }
        //Function to set status
        public void SetStatus(int i)
        {
            this.state = i;
        }
        //Function to return status
        public int GetStatus()
        {
            return this.state;
        }
        //Function to return row location
        public int GetRowLocation()
        {
            return this.xLocation;
        }
        //Function to return column location
        public int GetColumnLocation()
        {
            return this.yLocation;
        }
        /*
         Function to take in a cell array, as well as a specific offsets in the form of ints
         By using the stored coordinates of the cell in the array, and combining that with the offset values,
         we can look at specific neighbours of the cell, and return the neighbour's status
         For example, passing in (cellArray, 1, 1) would look at the coordinates of the cell, then go up 1 and right 1,
         returning the value of the up-right diagonal neighbour
        */
        public int GetNeighbourStatus(Cell[,] cellArray, int xOffset, int yOffset)
        {
            int status = 0;
            try
            {
                status = cellArray[this.GetRowLocation() + xOffset, this.GetColumnLocation() + yOffset].GetStatus();
            }
            catch { }
            return status;
        }
        //Function uses the GetNeighbourStatus method to fully populate the neighbours array with statuses of every neighbour
        public void PopulateNeighbours(Cell[,] currentArray)
        {
            this.neighbours[0] = this.GetNeighbourStatus(currentArray, -1, -1);
            this.neighbours[1] = this.GetNeighbourStatus(currentArray, -1, 0);
            this.neighbours[2] = this.GetNeighbourStatus(currentArray, -1, 1);
            this.neighbours[3] = this.GetNeighbourStatus(currentArray, 0, -1);
            this.neighbours[4] = this.GetNeighbourStatus(currentArray, 0, 1);
            this.neighbours[5] = this.GetNeighbourStatus(currentArray, 1, -1);
            this.neighbours[6] = this.GetNeighbourStatus(currentArray, 1, 0);
            this.neighbours[7] = this.GetNeighbourStatus(currentArray, 1, 1);
        }
        /*
         Function takes in # of living neighbours of a cell
         Using these values, the rules of the game of life are applied, and the new state is returned
        */
        public int ApplyRules(int livingNeighbours)
        {
            int newState = this.state;
            if (this.state == 1 && livingNeighbours < 2)
            {
                newState = 0;
            }
            if (this.state == 1 && (livingNeighbours == 2 || livingNeighbours == 3))
            {
                newState = 1;
            }
            if (this.state == 1 && livingNeighbours > 3)
            {
                newState = 0;
            }
            if (this.state == 0 && livingNeighbours == 3)
            {
                newState = 1;
            }
            return newState;
        }
        /*
         This function is used to determine the appropriate state for the cell in the next generation
         The new state is set to the current state by default
         Function uses int liveCounter to count living neighbours
         A for loop looks through all the neighbours of the cell, and increases liveCounter by 1
         when it find a living neighbour
         The function then passes the number of live neighbours into the ApplyRules function to return the appropriate new state
        */
        public int CheckNeighbours()
        {
            int liveCounter = 0;
            for (int i = 0; i < 8; i++)
            {
                if (this.neighbours[i] == 1)
                {
                    liveCounter += 1;
                }
            }
            return this.ApplyRules(liveCounter);
        }
    }
}
