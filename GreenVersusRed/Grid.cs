using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVersusRed
{
    public class Grid
    {
        private readonly int x;
        private readonly int y;
        private readonly Cell[,] grid;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="grid"></param>
        public Grid(int x, int y, Cell[,] grid)
        {
            this.x = x;
            this.y = y;
            this.grid = grid;
        }
        /// <summary>
        /// Method that return the count of the selected color
        /// </summary>
        /// <param name="target">used to get target</param>
        /// <param name="color">used to get color</param>
        /// <param name="targetGeneration">used to get the count of the nextGeneration</param>
        /// <returns></returns>
        public int CountTargetGenerationsByColor(ICell target, CellColor color, int targetGeneration)
        {
            var count = 0;

            for (int i = 0; i <= targetGeneration; i++)
            {
                if (grid[target.Row, target.Col].Color == color)
                {
                    count += 1;
                }

                ProcessNextGeneration();
            }

            return count;
        }

        /// <summary>
        /// Method that calculate nextGeneration
        /// </summary>
        private void ProcessNextGeneration()
        {
            var generationSnapshot = CreateGenerationSnapshot();

            for (int row = 0; row < y; row++)
            {
                for (int col = 0; col < x; col++)
                {
                    var cell = generationSnapshot[row, col];
                    var neighboursCount = GetGreenNeighboursCountFromSnapshot(cell, generationSnapshot);

                    switch (cell.Color)
                    {
                        case CellColor.Red when (neighboursCount == 3 || neighboursCount == 6):
                            grid[row, col].Color = CellColor.Green;
                            continue;
                        case CellColor.Green
                            when (neighboursCount != 2 && neighboursCount != 3 && neighboursCount != 6):
                            grid[row, col].Color = CellColor.Red;
                            continue;
                    }
                }
            }
        }

        /// <summary>
        /// Method that clone
        /// </summary>
        /// <returns>cloning</returns>
        private Cell[,] CreateGenerationSnapshot()
        {
            var snapshot = new Cell[y, x];

            for (int row = 0; row < y; row++)
            {
                for (int col = 0; col < x; col++)
                {
                    var cell = (Cell)grid[row, col].Clone();
                    snapshot[row, col] = cell;
                }
            }

            return snapshot;
        }

        /// <summary>
        /// Method that calculate neighbours
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="snapshot">the cloning</param>
        /// <returns>the count of the neighbours</returns>
        private int GetGreenNeighboursCountFromSnapshot(Cell cell, Cell[,] snapshot)
        {
            var neighbours = 0;

            if (cell.Row - 1 >= 0 && cell.Col - 1 >= 0 && snapshot[cell.Row - 1, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Row - 1 >= 0 && snapshot[cell.Row - 1, cell.Col].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Row - 1 >= 0 && cell.Col + 1 < x &&
                snapshot[cell.Row - 1, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Col - 1 >= 0 && snapshot[cell.Row, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Col + 1 < x && snapshot[cell.Row, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Row + 1 < y && cell.Col - 1 >= 0 &&
                snapshot[cell.Row + 1, cell.Col - 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Row + 1 < y && snapshot[cell.Row + 1, cell.Col].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            if (cell.Row + 1 < y && cell.Col + 1 < x &&
                snapshot[cell.Row + 1, cell.Col + 1].Color == CellColor.Green)
            {
                neighbours += 1;
            }

            return neighbours;
        }
    }
}
