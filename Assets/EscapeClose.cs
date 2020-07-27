using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeClose : MonoBehaviour
{
    public AudioSource btnSFX;

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NewWindowControlLimiter.instance.disableObjects("Stats/Notes Window");
            NewWindowControlLimiter.instance.renableObjects("Stats/Notes Window");
            btnSFX.Play();
        }
    }
}
