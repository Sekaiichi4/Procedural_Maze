using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeControlsUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToggleVisibility()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
