using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Dropdown resolutionChoices;
    public Text currentRes;

    private void Awake()
    {
        Screen.SetResolution(800, 600, false);
    }

    public void LateUpdate()
    {
        string resToString = $"Current Resolution:\n{Screen.width.ToString()} X {Screen.height.ToString()}";
        currentRes.text = resToString;
    }

    public void resolutionConfirmed()
    {
        switch (resolutionChoices.value)
        {
            case 0:
                Screen.SetResolution(800, 600, false);
                break;
            case 1:
                Screen.SetResolution(960, 720, false);
                break;
            case 2:
                Screen.SetResolution(1024, 768, false);
                break;
            case 3:
                Screen.SetResolution(1280, 960, false);
                break;
        }
    }
}
