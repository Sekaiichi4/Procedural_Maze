using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeControlsUI : MonoBehaviour
{
    // Toggles the visibility of this gameobject.
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
