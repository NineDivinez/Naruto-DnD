using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChange : MonoBehaviour
{
    public GameObject lastScreen;
    public GameObject rulesButton;

    public GameObject firstPosition;
    public GameObject secondPosition;

    // Update is called once per frame
    void Update()
    {
        if (!lastScreen.activeInHierarchy)
            rulesButton.transform.position = firstPosition.transform.position;
        else if (lastScreen.activeInHierarchy)
            rulesButton.transform.position = secondPosition.transform.position;
    }
}
