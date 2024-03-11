using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;


public class Astar
{
    Cell[,] maze;
    SimplePriorityQueue<AStarCell> openSet;
    List<AStarCell> closedSet;

    Vector2 end;

    public Vector2[] GeneratePath(Cell[,] maze, Vector2 start, Vector2 end)
    {
        this.maze = maze;
        this.end = end;

        openSet.Enqueue(maze[(int)start.x, (int)start.y].ToAstar(), 0);
        do
        {
            AStarCell current = openSet.Dequeue();
            closedSet.Add(current);
            if (current.Position == end)
            {
                List<Vector2> path = new List<Vector2>();
                while (current.Parent != null)
                {
                    path.Add(current.Position);
                    current = current.Parent;
                }
                path.Reverse();
                return path.ToArray();
            }

        }
        while (openSet.Count > 0);
       
        return null;
    }

    public void ExpandCell(AStarCell cell)
    {
        foreach (AStarCell successor in GetNeighbors(cell))
        {
            if (closedSet.Contains(successor))
                continue;

            float tentative_g = cell.GCost + CostToCell(cell, successor);
            if (openSet.Contains(successor) && tentative_g >= successor.GCost)
                continue;

            successor.Parent = cell;
            successor.GCost = tentative_g;
            successor.HCost = Vector2.Distance(successor.Position, end);
            if(openSet.Contains(successor))
            {
                openSet.UpdatePriority(successor, successor.FCost);
            }
            else
            {
                openSet.Enqueue(successor, successor.FCost);
            }
        }
    }

    private float CostToCell(AStarCell from, AStarCell to)
    {
        Vector2 top = new Vector2(0, 1);
        Vector2 bottom = new Vector2(0, -1);
        Vector2 left = new Vector2(-1, 0);
        Vector2 right = new Vector2(1, 0);

        if(from.Position + top == to.Position && from.ConnectedTop)
        {
            return 1;
        }
        if (from.Position + bottom == to.Position && from.ConnectedBottom)
        {
            return 1;
        }
        if (from.Position + right == to.Position && from.ConnectedRight)
        {
            return 1;
        }
        if (from.Position + left == to.Position && from.ConnectedLeft)
        {
            return 1;
        }
        return float.MaxValue;
    }

    private AStarCell[] GetNeighbors(AStarCell cell)
    {
        List<AStarCell> neighbors = new List<AStarCell>(4);

        Vector2[] positions = new Vector2[]
        {
            new Vector2(cell.Position.x, cell.Position.y + 1),
            new Vector2(cell.Position.x, cell.Position.y - 1),
            new Vector2(cell.Position.x + 1, cell.Position.y),
            new Vector2(cell.Position.x - 1, cell.Position.y)
        };

        for (int i = 0; i < positions.Length; i++)
        {
            neighbors[i] = TryGetCell(positions[i]);
        }

        neighbors.RemoveAll(x => x == null);

        return neighbors.ToArray();
    }

    private AStarCell TryGetCell(Vector2 pos)
    {
        if(pos.x > -1 && pos.x < maze.GetLength(0) && pos.y > -1 && pos.y < maze.GetLength(1))
        {
            return (AStarCell)maze[(int)pos.x, (int)pos.y];
        }
        return null;
    }

}
