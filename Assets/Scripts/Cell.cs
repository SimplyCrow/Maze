using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell : FastPriorityQueueNode
{
    public Vector2 Position { get; set; }
    public bool ConnectedTop { get; set; }
    public bool ConnectedRight { get; set; }
    public bool ConnectedBottom { get; set; }
    public bool ConnectedLeft { get; set; }

    public bool isVisited {get; set; }

    public Vector2 parentPosition { get; set; }
    
    public Cell()
    {
        ConnectedTop = false;
        ConnectedRight = false;
        ConnectedBottom = false;
        ConnectedLeft = false;
        isVisited = false;
    }
    public Cell(Vector2 position, bool connectedTop = false, bool connectedRight = false, bool connectedBottom = false, bool connectedLeft = false)
    {
        Position = position;
        ConnectedTop = connectedTop;
        ConnectedRight = connectedRight;
        ConnectedBottom = connectedBottom;
        ConnectedLeft = connectedLeft;
        isVisited = false;
    }
    public AStarCell ToAstar()
    {
        return new AStarCell()
        {
            Position = Position,
            ConnectedTop = ConnectedTop,
            ConnectedRight = ConnectedRight,
            ConnectedBottom = ConnectedBottom,
            ConnectedLeft = ConnectedLeft,
            isVisited = isVisited
        };
    }
}
