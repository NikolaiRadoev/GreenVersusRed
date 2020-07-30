using System;
using System.Linq;

namespace GreenVersusRed
{
    class Program
    {
        static void Main(string[] args)
        {
            //display the UI
            Console.WriteLine("Green vs. Red");
            Console.WriteLine("Enter height and width separarted by ','");
            var xyInput = Console.ReadLine();
            Console.WriteLine("Enter the values of row without separation");

            var xy = xyInput?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var x = xy[0];
            var y = xy[1];
            Cell[,] gen0 = new Cell[x, y];

            for (int row = 0; row < y; row++)
            {
                Console.WriteLine($"Enter the row {row + 1}");
                var rowString = Console.ReadLine();

                var cellsInput = rowString?.ToCharArray()
                    .Select(value => Enum.Parse<CellColor>(value.ToString())).ToArray();

                for (int col = 0; col < cellsInput?.Length; col++)
                {

                    var color = cellsInput[col];

                    if (color != CellColor.Green && color != CellColor.Red)
                    {
                        throw new FormatException($"Invalid color for cell {row}:{col}");
                    }

                    gen0[row, col] = new Cell(row, col, color);
                }
            }
            
            Console.WriteLine("Enter position(x & y) and number of turns separarted by ','");
            var targetInput = Console.ReadLine();

            var targetNumbers = targetInput?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var target = new Cell(targetNumbers[1], targetNumbers[0]);
            
            var targetGeneration = targetNumbers[2];
            var grid = new Grid(x, y, gen0);
            var targetGenerations = grid.CountTargetGenerationsByColor(target, CellColor.Green, targetGeneration);

            Console.WriteLine($"Expected result is: {targetGenerations}");
        }
    }
}
