using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEscape : MonoBehaviour
{
    public GameObject commandPrompt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (commandPrompt.activeInHierarchy)
                commandPrompt.SetActive(false);
        }
    }
}
