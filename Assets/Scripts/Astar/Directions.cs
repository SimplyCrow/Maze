using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PathConstants
{
    public static Vector2[] Directions = new Vector2[4]
    {
            new Vector2(-1, 0), //left
            new Vector2(1, 0),  //right
            new Vector2(0, 1),  //up
            new Vector2(0, -1)  //down
    };
}

