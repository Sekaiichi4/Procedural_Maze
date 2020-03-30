using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public MazeManager mm;
    public int xPos, yPos;
    public float walkTime;

    private float walkCooldown = 0f;

    private void Start()
    {
        xPos = 0;
        yPos = 0;
    }

    public void ResetPosition()
    {
        xPos = 0;
        yPos = 0;
        transform.position = mm.grid[xPos, yPos].transform.position;
    }

    private void FixedUpdate()
    {
        if (walkCooldown > 0)
        {
            walkCooldown -= Time.deltaTime;
        }
        else
        {
            //UP
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (!mm.gridCells[xPos, yPos].walls[0])
                {
                    yPos--;
                    LeanTween.moveLocalY(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.y, walkTime);
                    walkCooldown = walkTime;
                    // transform.position = mm.grid[xPos, yPos].transform.position;
                }
            } //RIGHT
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (!mm.gridCells[xPos, yPos].walls[1])
                {
                    xPos++;
                    LeanTween.moveLocalX(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.x, walkTime);
                    walkCooldown = walkTime;
                    // transform.position = mm.grid[xPos, yPos].transform.position;
                }
            } //DOWN
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (!mm.gridCells[xPos, yPos].walls[2])
                {
                    yPos++;
                    LeanTween.moveLocalY(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.y, walkTime);
                    walkCooldown = walkTime;
                    // transform.position = mm.grid[xPos, yPos].transform.position;
                }
            } //LEFT
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (!mm.gridCells[xPos, yPos].walls[3])
                {
                    xPos--;
                    LeanTween.moveLocalX(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.x, walkTime);
                    walkCooldown = walkTime;
                    //transform.position = mm.grid[xPos, yPos].transform.position;
                }
            }
        }
    }
}
