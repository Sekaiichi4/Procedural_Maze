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
        visited = true;

        Debug.Log("Visited cell with position: " + xPos + "," + yPos);

        //TODO: Change this to a method that decides which sprite to assign to it.
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maze/1111v");
    }

    void Update()
    {

    }
}
