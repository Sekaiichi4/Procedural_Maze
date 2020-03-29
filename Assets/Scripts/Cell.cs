using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int xPos, yPos;
    /// <summary>
    /// Top, Right, Bottom, Left
    /// </summary>
    public bool[] walls = { true, true, true, true };
    public bool visited = false;
    /// <summary>
    /// 0 = Top, 1 = Right, 2 = Bottom, 3 = Left
    /// </summary>
    public int posAsNeighbour;

    void Start()
    {

    }

    public void InitCell(int _x, int _y, float _gridSize)
    {
        xPos = _x;
        yPos = _y;

        //We want to initialize the new location of the cell in the grid from the top left till the bottom right.
        transform.localPosition = new Vector3(_x * _gridSize, (-_y) * _gridSize, gameObject.transform.position.z);
    }

    public void Visit()
    {
        if (!visited)
        {
            visited = true;

            // Debug.Log("Visited cell with position: " + xPos + "," + yPos);

            //TODO: Change this to a method that decides which sprite to assign to it.
            ResetSprite();
        }
    }

    public void ResetSprite()
    {
        if (!walls[0]) //TOP BROKEN
        {
            if (!walls[1]) //AND RIGHT
            {
                if (!walls[2]) //AND AND BOTTOM
                {
                    if (!walls[3]) //AND AND AND LEFT
                    {
                        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0000");
                    }
                    else //AND AND AND NOTHING ELSE
                    {
                        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0001");
                    }
                }
                else if (!walls[3]) //AND AND LEFT
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0010");
                }
                else //AND AND NOTHING ELSE
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0011");
                }
            }
            else if (!walls[2]) //AND BOTTOM
            {
                if (!walls[3]) //AND AND LEFT
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0100");
                }
                else //AND AND NOTHING ELSE
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0101");
                }
            }
            else if (!walls[3]) //AND LEFT
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0110");
            }
            else //AND NOTHING ELSE
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/0111");
            }
        }
        else if (!walls[1]) //RIGHT BROKEN
        {
            if (!walls[2]) //AND BOTTOM
            {
                if (!walls[3]) //AND AND LEFT
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1000");
                }
                else //AND AND NOTHING ELSE
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1001");
                }
            }
            else if (!walls[3]) //AND LEFT
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1010");
            }
            else //AND NOTHING ELSE
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1011");
            }
        }
        else if (!walls[2]) //BOTTOM BROKEN
        {
            if (!walls[3]) //AND LEFT
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1100");
            }
            else //AND NOTHING ELSE
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1101");
            }
        }
        else if (!walls[3]) //LEFT BROKEN
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1110");
        }
        else //NOTHING IS BROKEN
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1111v");
        }
    }

    void Update()
    {

    }
}
