using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public int gridWidth, gridHeight;
    public GameObject cellObject;

    private float cellSize = 2.56f;
    private GameObject[,] grid;
    private Cell[,] gridCells;
    private Cell currentCell;

    /// <summary>
    /// A stack of visited cells that lead back to the start point.
    /// </summary>
    private Stack<Cell> visitedCells;

    void Start()
    {
        //Transform the parent grid gameobject so that the cells are centred in the screen. 
        transform.position = new Vector3((-gridWidth) * (cellSize / 2) + (cellSize / 2), (gridHeight) * (cellSize / 2) - (cellSize / 2), gameObject.transform.position.z);

        //Create the basic grid for the maze.
        CreateGrid();

        visitedCells = new Stack<Cell>();

        //Start at the first cell.
        gridCells[0, 0].Visit();
        currentCell = gridCells[0, 0];
        visitedCells.Push(currentCell);
    }

    void CreateGrid()
    {
        //Two dimensional array for the gameobjects.
        grid = new GameObject[gridWidth, gridHeight];

        //Two dimensional array for the Cell instances.
        gridCells = new Cell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject mCell = grid[x, y] = Instantiate(cellObject, gameObject.transform);

                gridCells[x, y] = mCell.GetComponent<Cell>();

                gridCells[x, y].InitCell(x, y, cellSize);
            }
        }
    }

    void NextCell()
    {
        if (visitedCells.Count > 0)
        {
            //Get all available neighbours.
            List<Cell> neighbours = ReturnAvailableNeighboursFor(currentCell.xPos, currentCell.yPos);

            if (neighbours.Count > 0)
            {
                //select a random neighbour.
                int randomIndex = Random.Range(0, neighbours.Count);
                Cell mCell = neighbours[randomIndex];

                //Let the walls be crushed between de current and next cell.
                CrushWalls(mCell);
                currentCell.ResetSprite();

                gridCells[mCell.xPos, mCell.yPos].Visit();
                currentCell = gridCells[mCell.xPos, mCell.yPos];
                visitedCells.Push(currentCell);
            }
            else
            {
                Cell mCell = visitedCells.Peek();
                currentCell = gridCells[mCell.xPos, mCell.yPos];
                visitedCells.Pop();
                NextCell();
            }
        }
        else
        {
            Debug.LogWarning("Maze already created.");
        }

    }

    void CrushWalls(Cell nextCell)
    {
        switch (nextCell.posAsNeighbour)
        {
            case 0: //Top
                gridCells[currentCell.xPos, currentCell.yPos].walls[0] = false;
                gridCells[nextCell.xPos, nextCell.yPos].walls[2] = false;
                break;

            case 1: //Right
                gridCells[currentCell.xPos, currentCell.yPos].walls[1] = false;
                gridCells[nextCell.xPos, nextCell.yPos].walls[3] = false;
                break;

            case 2: //Bottom
                gridCells[currentCell.xPos, currentCell.yPos].walls[2] = false;
                gridCells[nextCell.xPos, nextCell.yPos].walls[0] = false;
                break;

            case 3: //Left
                gridCells[currentCell.xPos, currentCell.yPos].walls[3] = false;
                gridCells[nextCell.xPos, nextCell.yPos].walls[1] = false;
                break;
        }
    }

    List<Cell> ReturnAvailableNeighboursFor(int _xPos, int _yPos)
    {
        List<Cell> neighbours = new List<Cell>();

        //Check Top
        if (_yPos > 0)
        {
            if (!gridCells[_xPos, _yPos - 1].visited)
            {
                gridCells[_xPos, _yPos - 1].posAsNeighbour = 0;
                neighbours.Add(gridCells[_xPos, _yPos - 1]);
                Debug.LogFormat("Added to neighbours: {0} , {1}", _xPos, _yPos - 1);
            }
        }

        //Check Right
        if (_xPos < gridWidth - 1)
        {
            if (!gridCells[_xPos + 1, _yPos].visited)
            {
                gridCells[_xPos + 1, _yPos].posAsNeighbour = 1;

                neighbours.Add(gridCells[_xPos + 1, _yPos]);
                Debug.LogFormat("Added to neighbours: {0} , {1}", _xPos + 1, _yPos);
            }
        }

        //Check Bottom
        if (_yPos < gridHeight - 1)
        {
            if (!gridCells[_xPos, _yPos + 1].visited)
            {
                gridCells[_xPos, _yPos + 1].posAsNeighbour = 2;

                neighbours.Add(gridCells[_xPos, _yPos + 1]);
                Debug.LogFormat("Added to neighbours: {0} , {1}", _xPos, _yPos + 1);
            }
        }

        //Check Left
        if (_xPos > 0)
        {
            if (!gridCells[_xPos - 1, _yPos].visited)
            {
                gridCells[_xPos - 1, _yPos].posAsNeighbour = 3;

                neighbours.Add(gridCells[_xPos - 1, _yPos]);
                Debug.LogFormat("Added to neighbours: {0} , {1}", _xPos - 1, _yPos);
            }
        }

        return neighbours;
    }

    void Update()
    {
        //Visits the next cell if Space is pressed down.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCell();
        }
    }
}
