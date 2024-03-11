using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public class Maze
{
    private int width;
    private int height;
    private Cell[,] maze;

    public Cell[,] Generate(int width, int height, Vector2 start)
    {
        Stack<Vector2> stack = new Stack<Vector2>();
        maze = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = new Cell(new Vector2(i, j));
            }
        }

        maze[(int)start.x, (int)start.y].isVisited = true;
        stack.Push(start);

        Vector2 bounds = new Vector2(width, height);

        while (stack.Count > 0)
        {
            Vector2 current = stack.Peek();

            List<Vector2> neighbors = new List<Vector2>(PathConstants.Directions.Length);
            for (int i = 0; i < PathConstants.Directions.Length; i++)
            {
                Vector2 proposedNeighbor = current + PathConstants.Directions[i];
                if (proposedNeighbor.x < 0 || proposedNeighbor.x >= bounds.x ||
                    proposedNeighbor.y < 0 || proposedNeighbor.y >= bounds.y)
                    continue;

                if (maze[(int)proposedNeighbor.x, (int)proposedNeighbor.y].isVisited)
                    continue;

                neighbors.Add(proposedNeighbor);
            }
           

            
            if(neighbors.Count > 0)
            {
                Vector2 neighbor = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                stack.Push(neighbor);

                Vector2 direction = neighbor - current;

                if (direction == PathConstants.Directions[0])
                {
                    maze[(int)current.x, (int)current.y].ConnectedLeft = true;
                    maze[(int)neighbor.x, (int)neighbor.y].ConnectedRight = true;                   
                }
                else if (direction == PathConstants.Directions[1])
                {
                    maze[(int)current.x, (int)current.y].ConnectedRight = true;
                    maze[(int)neighbor.x, (int)neighbor.y].ConnectedLeft = true;
                }
                else if (direction == PathConstants.Directions[2])
                {
                    maze[(int)current.x, (int)current.y].ConnectedTop = true;
                    maze[(int)neighbor.x, (int)neighbor.y].ConnectedBottom = true;   
                }
                else if (direction == PathConstants.Directions[3])
                {
                    maze[(int)current.x, (int)current.y].ConnectedBottom = true;
                    maze[(int)neighbor.x, (int)neighbor.y].ConnectedTop = true;
                }
                maze[(int)neighbor.x, (int)neighbor.y].isVisited = true;
            }
            else
                stack.Pop();
        }
        return maze;

    }

}

