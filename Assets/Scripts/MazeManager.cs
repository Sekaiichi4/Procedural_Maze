using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public GameObject cellObject;

    private float cellSize = 2.56f;
    private GameObject[,] grid;
    private Cell currentCell;

    void Start()
    {
        //Transform the parent grid gameobject so that the cells are centered in the screen. 
        transform.position = new Vector3((-gridWidth) * (cellSize / 2) + (cellSize / 2), (gridHeight) * (cellSize / 2) - (cellSize / 2), gameObject.transform.position.z);

        //Create the basic grid for the maze.
        CreateGrid();

        //Start at the first cell.
        grid[0, 0].GetComponent<Cell>().VisitCell();
    }

    void CreateGrid()
    {
        grid = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject mCell = grid[x, y] = Instantiate(cellObject, gameObject.transform);

                mCell.GetComponent<Cell>().InitCell(x, y, cellSize);
            }
        }
    }

    void Update()
    {

    }
}
