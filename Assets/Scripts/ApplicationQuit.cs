using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{
    void Update()
    {
        QuitApplication();
    }

    void QuitApplication()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            Application.Quit(0);
        }
    }

}
