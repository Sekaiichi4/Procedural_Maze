using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public MazeManager mm;
    public int xPos, yPos;
    public float walkTime;

    private float walkCooldown = 0f;
    private Animator animator;
    private int direction;

    private void Start()
    {
        xPos = 0;
        yPos = 0;

        animator = GetComponent<Animator>();

        direction = 2;
    }

    //Reset the position of the player to the cell on position 0, 0
    public void ResetPosition()
    {
        xPos = 0;
        yPos = 0;
        transform.position = mm.grid[xPos, yPos].transform.position;
    }

    private void FixedUpdate()
    {
        //If the character is still walking, lower its cooldown, else be ready for the next input or animation change.
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

                    animator.Play("Walk_Up");
                    direction = 0;
                }
            } //RIGHT
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (!mm.gridCells[xPos, yPos].walls[1])
                {
                    xPos++;
                    LeanTween.moveLocalX(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.x, walkTime);
                    walkCooldown = walkTime;

                    animator.Play("Walk_Right");
                    direction = 1;
                }
            } //DOWN
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (!mm.gridCells[xPos, yPos].walls[2])
                {
                    yPos++;
                    LeanTween.moveLocalY(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.y, walkTime);
                    walkCooldown = walkTime;

                    animator.Play("Walk_Down");
                    direction = 2;
                }
            } //LEFT
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (!mm.gridCells[xPos, yPos].walls[3])
                {
                    xPos--;
                    LeanTween.moveLocalX(this.gameObject, mm.grid[xPos, yPos].transform.localPosition.x, walkTime);
                    walkCooldown = walkTime;

                    animator.Play("Walk_Left");
                    direction = 3;
                }
            }
            else
            {
                switch (direction)
                {
                    case 0:
                        animator.Play("Idle_Back");
                        break;
                    case 1:
                        animator.Play("Idle_Right");
                        break;
                    case 2:
                        animator.Play("Idle_Front");
                        break;
                    case 3:
                        animator.Play("Idle_Left");
                        break;
                }
            }
        }
    }
}
