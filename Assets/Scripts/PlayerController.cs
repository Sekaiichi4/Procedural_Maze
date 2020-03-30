using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public MazeManager mm;
    public int xPos, yPos;

    private void Start()
    {
        xPos = 0;
        yPos = 0;
    }

    private void Update()
    {
        //UP
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (!mm.gridCells[xPos, yPos].walls[0])
            {
                yPos--;
                transform.position = mm.grid[xPos, yPos].transform.position;
            }
        } //RIGHT
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (!mm.gridCells[xPos, yPos].walls[1])
            {
                xPos++;
                transform.position = mm.grid[xPos, yPos].transform.position;
            }
        } //DOWN
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!mm.gridCells[xPos, yPos].walls[2])
            {
                yPos++;
                transform.position = mm.grid[xPos, yPos].transform.position;
            }
        } //LEFT
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (!mm.gridCells[xPos, yPos].walls[3])
            {
                xPos--;
                transform.position = mm.grid[xPos, yPos].transform.position;
            }
        }
    }
}
