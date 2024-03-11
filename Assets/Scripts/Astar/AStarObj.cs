using Priority_Queue;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class AstarObj
{
    AStarCell[,] maze;
    FastPriorityQueue<AStarCell> openSet;
    List<AStarCell> closedSet;

    public Vector2[] GeneratePath(Cell[,] map, Vector2 start, Vector2 end)
    {
        maze = new AStarCell[map.GetLength(0), map.GetLength(1)];
        for (int k = 0; k < map.GetLength(0); k++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                maze[k, j] = map[k, j].ToAstar();
                maze[k, j].HCost = Vector2.Distance(maze[k, j].Position, end);
            }
        }

        openSet = new FastPriorityQueue<AStarCell>(map.GetLength(0) * map.GetLength(1));

        openSet.Enqueue(maze[(int)start.x, (int)start.y], 0);
        
        Vector2 bounds = new Vector2(map.GetLength(0), map.GetLength(1));

        AStarCell node = new AStarCell();

        while(openSet.Count > 0)
        {
            node = openSet.Dequeue();

            node.isClosedSet = true;

            float g = node.GCost + 1;

            if(end == node.Position) break;

            Vector2 proposed;

            for (int i = 0; i < PathConstants.Directions.Length; i++)
            {
                var direction = PathConstants.Directions[i];

                proposed = node.Position + direction;


                if(proposed.x < 0 || proposed.x >= bounds.x || 
                   proposed.y < 0 || proposed.y >= bounds.y)
                   continue;
                AStarCell neighbour = maze[(int)proposed.x, (int)proposed.y];

                switch (i)
                {
                    case 0:
                        if(!neighbour.ConnectedRight && !node.ConnectedLeft) 
                            continue;
                        break;
                    case 1:
                        if (!neighbour.ConnectedLeft && !node.ConnectedRight)
                            continue;
                        break;
                    case 2:
                        if (!neighbour.ConnectedBottom && !node.ConnectedTop)
                            continue;
                        break;
                    case 3:
                        if (!neighbour.ConnectedTop && !node.ConnectedBottom)
                            continue;
                        break;
                }

                if(neighbour.isClosedSet) continue;


                if (!neighbour.isInOpenSet)
                {
                    neighbour.GCost = g;
                    neighbour.Parent = node;

                    neighbour.isInOpenSet = true;
                    openSet.Enqueue(neighbour, neighbour.FCost);
                }else
                {
                    neighbour.GCost = g;
                    neighbour.Parent = node;

                    openSet.UpdatePriority(neighbour, neighbour.FCost);
                }
            }
        }

        var path = new Stack<Vector2>();

        while(node != null)
        {
            path.Push(node.Position);
            node = node.Parent;
        }

        return path.ToArray();
    }
}
