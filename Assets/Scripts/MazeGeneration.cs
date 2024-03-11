using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
public class MazeGeneration : MonoBehaviour
{
    public TMP_InputField x;
    public TMP_InputField y;

    public int width = 10;
    public int height = 10;
    public bool generate = true;
    public Vector2 start;
    public Vector2 end;
    public bool randomStart = true;
    public bool fromEndToEnd = true;
    public GameObject wall;
    public GameObject pathPrefab;



    private Cell[,] Cells;

    // Update is called once per frame
    void Update()
    {
        if (generate)
            Generate();
    }

    public void OnGenerateButtonClicked()
    { 
        width = (x.text.Length == 0 || int.Parse(x.text) == 0) ? 10 : int.Parse(x.text);
        height = (y.text.Length == 0 || int.Parse(y.text) == 0) ? 10 : int.Parse(y.text);

        generate = true;
    }

    private void Generate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if (randomStart)
        {
            start = new Vector2(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height));
        }
        if (fromEndToEnd)
        {
            start = new Vector2(0, 0);
            end = new Vector2(width - 1, height - 1);
        }
        Maze Maze = new Maze();
        Cells = Maze.Generate(width, height, start);
        foreach (var cell in Cells)
        {
            if (!cell.ConnectedTop)
            {
                GameObject g = Instantiate(wall, cell.Position + new Vector2(0, 0.5f), Quaternion.identity);
                g.transform.localRotation = Quaternion.Euler(0, 0, 90);
                g.transform.parent = transform;
            }
            if (!cell.ConnectedRight)
            {
                GameObject g = Instantiate(wall, cell.Position + new Vector2(0.5f, 0), Quaternion.identity);
                g.transform.parent = transform;
            }
            if (!cell.ConnectedBottom)
            {
                GameObject g = Instantiate(wall, cell.Position + new Vector2(0, -0.5f), Quaternion.identity);
                g.transform.localRotation = Quaternion.Euler(0, 0, 90);
                g.transform.parent = transform;
            }
            if (!cell.ConnectedLeft)
            {
                GameObject g = Instantiate(wall, cell.Position + new Vector2(-0.5f, 0), Quaternion.identity);
                g.transform.parent = transform;
            }
        }
        generate = false;
        GeneratePath();
    }

    public void GeneratePath()
    {
        var astar = new AstarObj();
        Vector2[] path;
        path = astar.GeneratePath(Cells, start, end);
        for (int i = 0; i < path.Length; i++)
        {
            GameObject gameObject = Instantiate(pathPrefab, path[i], Quaternion.identity);
            gameObject.transform.parent = transform;
            if (i == path.Length - 1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                Vector2 dir = path[i + 1] - path[i];
                if (dir == new Vector2(0, 1))
                {
                    gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else if (dir == new Vector2(1, 0))
                {
                    gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
                else if (dir == new Vector2(-1, 0))
                {
                    gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
                else if (dir == new Vector2(0, -1))
                {
                    gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
        GameObject g = Instantiate(pathPrefab, start, Quaternion.identity);
        g.transform.parent = transform;
        g.GetComponent<SpriteRenderer>().color = Color.yellow;

    }
}

