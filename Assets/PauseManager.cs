using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool paused;//technically not pausing, just unlocking the cursor

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //print(paused);
            if (paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                paused = true;
            }
        }
    }
}
