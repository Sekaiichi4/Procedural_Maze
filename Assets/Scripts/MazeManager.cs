using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeManager : MonoBehaviour
{
    public int gridRows, gridColumns;
    public TextMeshProUGUI widthCounter, heightCounter;
    public GameObject cellObject;

    private float cellSize = 2.56f;
    private int cellSizeInPixels = 256;
    private float scaleRatio = 1;

    public GameObject[,] grid;
    public Cell[,] gridCells;
    private Cell currentCell;

    /// <summary>
    /// A stack of visited cells that lead back to the start point.
    /// </summary>
    private Stack<Cell> visitedCells;

    private void Start()
    {
        //Set the UI controls value to the grid's values.
        UpdateUIControls();

        //Scale the Maze to the current screensize so it all is visible.
        ScaleToScreen();

        //Create the basic grid for the maze.
        CreateGrid();

        //Generate the maze on top of the current grid.
        CreateMaze();
    }

    public void RecreateMaze()
    {
        //Wipe the previous grid gameobjects in the scene.
        WipeGrid();

        //Scale the Maze to the current screensize so it all is visible.
        ScaleToScreen();

        //Create the basic grid for the maze.
        CreateGrid();

        //Generate the maze on top of the current grid.
        CreateMaze();
    }

    //Increment the Rows of the grid.
    public void IncreaseRows()
    {
        if (gridRows < 99)
        {
            gridRows++;

            UpdateUIControls();
        }
    }

    //Decrement the Rows of the grid.
    public void DecreaseRows()
    {
        if (gridRows > 1)
        {
            gridRows--;

            UpdateUIControls();
        }
    }

    //Increment the Columns of the grid.
    public void IncreaseColumns()
    {
        if (gridColumns < 99)
        {
            gridColumns++;

            UpdateUIControls();
        }
    }

    //Decrement the Columns of the grid.
    public void DecreaseColumns()
    {
        if (gridColumns > 1)
        {
            gridColumns--;

            UpdateUIControls();
        }
    }

    //Create the grid to draw the maze on.
    private void CreateGrid()
    {
        //Transform this parent grid gameobject so that the cells are centred in the screen. 
        transform.position = new Vector3((-gridRows) * (cellSize * scaleRatio / 2) + (cellSize * scaleRatio / 2), (gridColumns) * (cellSize * scaleRatio / 2) - (cellSize * scaleRatio / 2), gameObject.transform.position.z);

        //Two dimensional array for the gameobjects.
        grid = new GameObject[gridRows, gridColumns];

        //Two dimensional array for the Cell instances.
        gridCells = new Cell[gridRows, gridColumns];

        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                GameObject mCell = grid[x, y] = Instantiate(cellObject, gameObject.transform);

                gridCells[x, y] = mCell.GetComponent<Cell>();

                gridCells[x, y].InitCell(x, y, cellSize);
            }
        }
    }

    //Remove the previous grid's cell gameobjects.
    private void WipeGrid()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                GameObject.Destroy(grid[x, y]);
            }
        }
    }

    //Draw the maze on the grid.
    private void CreateMaze()
    {
        //Clear/Init the visited cells stack.
        visitedCells = new Stack<Cell>();

        //Start at the first cell.
        gridCells[0, 0].Visit();
        currentCell = gridCells[0, 0];
        visitedCells.Push(currentCell);

        NextCell();
    }

    //Proceed to the next cell to visit during the maze creating process.
    private void NextCell()
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

                NextCell();
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
            // Debug.LogWarning("Maze already created.");
        }

    }

    //Disable the walls between the current and next cell that is going to be visited.
    private void CrushWalls(Cell nextCell)
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

    //Return a list of neighbouring cells to the current one, that have not yet been visited.
    private List<Cell> ReturnAvailableNeighboursFor(int _xPos, int _yPos)
    {
        List<Cell> neighbours = new List<Cell>();

        //Check Top
        if (_yPos > 0)
        {
            if (!gridCells[_xPos, _yPos - 1].visited)
            {
                gridCells[_xPos, _yPos - 1].posAsNeighbour = 0;
                neighbours.Add(gridCells[_xPos, _yPos - 1]);
            }
        }

        //Check Right
        if (_xPos < gridRows - 1)
        {
            if (!gridCells[_xPos + 1, _yPos].visited)
            {
                gridCells[_xPos + 1, _yPos].posAsNeighbour = 1;

                neighbours.Add(gridCells[_xPos + 1, _yPos]);
            }
        }

        //Check Bottom
        if (_yPos < gridColumns - 1)
        {
            if (!gridCells[_xPos, _yPos + 1].visited)
            {
                gridCells[_xPos, _yPos + 1].posAsNeighbour = 2;

                neighbours.Add(gridCells[_xPos, _yPos + 1]);
            }
        }

        //Check Left
        if (_xPos > 0)
        {
            if (!gridCells[_xPos - 1, _yPos].visited)
            {
                gridCells[_xPos - 1, _yPos].posAsNeighbour = 3;

                neighbours.Add(gridCells[_xPos - 1, _yPos]);
            }
        }

        return neighbours;
    }

    //Update the MazeControls UI if they are assigned.
    private void UpdateUIControls()
    {
        //If the WidthCounter isn't null (in the case that one doesn't want to use the UI), update the counter.
        if (widthCounter)
        {
            widthCounter.text = gridRows.ToString();
        }

        //If the HeightCounter isn't null (in the case that one doesn't want to use the UI), update the counter.
        if (heightCounter)
        {
            heightCounter.text = gridColumns.ToString();
        }
    }

    //Scale the grid/maze to the screen to make sure there is minimum white space.
    private void ScaleToScreen()
    {
        Vector2 aspectRatio = GetAspectRatio(Screen.width, Screen.height);

        float HorizontalScaleRatio = (float)((1080 / aspectRatio.y) * aspectRatio.x) / (float)(cellSizeInPixels * gridRows);
        float VerticalScaleRatio = (float)1080 / (float)(cellSizeInPixels * gridColumns);

        if (HorizontalScaleRatio < VerticalScaleRatio)
        {
            scaleRatio = HorizontalScaleRatio;
            transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
        }
        else
        {
            scaleRatio = VerticalScaleRatio;
            transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
        }
    }

    //Get the aspect ratio of the current screen.
    private static Vector2 GetAspectRatio(int x, int y)
    {
        float f = (float)x / (float)y;
        int i = 0;
        while (true)
        {
            i++;
            if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                break;
        }
        return new Vector2((float)System.Math.Round(f * i, 2), i);
    }
}
