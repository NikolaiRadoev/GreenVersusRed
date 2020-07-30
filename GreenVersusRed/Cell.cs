using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVersusRed
{
    public class Cell : ICell, ICloneable
    {
        public int Row { get; }
        public int Col { get; }
        public CellColor Color { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="row">row of the grid</param>
        /// <param name="col">column of the grid</param>
        /// <param name="color">green</param>
        public Cell(int row, int col, CellColor color = CellColor.Green)
        {
            Row = row;
            Col = col;
            Color = color;
        }

        public override string ToString()
        {
            return $"{Row}:{Col} {Color}";
        }
        /// <summary>
        /// Method that clone Cell
        /// </summary>
        /// <returns>cloning</returns>
        public object Clone()
        {
            return new Cell(Row, Col, Color);
        }
    }
}
