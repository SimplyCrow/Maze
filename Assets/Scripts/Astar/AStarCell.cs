using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AStarCell : Cell
{
    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost { get => GCost + HCost; }
    public AStarCell Parent { get; set; }
    public bool isClosedSet { get; set; }

    public bool isInOpenSet { get; set; }

    public AStarCell()
    {
        GCost = float.MaxValue;
        HCost = float.MaxValue;
        Parent = null;
        isClosedSet = false;
        isInOpenSet = false;
    }

    public AStarCell[,] FromCell(Cell[,] maze)
    {
        AStarCell[,] aStarCells = new AStarCell[maze.GetLength(0), maze.GetLength(1)];
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                aStarCells[i, j] = (AStarCell)maze[i, j];
            }
        }
        return aStarCells;
    }

}

