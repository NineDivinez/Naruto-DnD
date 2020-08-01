using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    public GameObject moveMe;

    int timer;
    private void Start()
    {
        timer = TimeHandler.instance.newTimer(10, true, 20);
    }

    void Update()
    {
        string timeRemaining = TimeHandler.instance.checkTimer(timer, true).Remove(0, 16);
        if (Int32.Parse(timeRemaining) <= 3 && Int32.Parse(timeRemaining) > 0)
        {
            print($"Moving object in {timeRemaining}");
        }

        if (Int32.Parse(timeRemaining) <= -3)
        {
            TimeHandler.instance.waiting(timer, true);
            moveMe.transform.position = new Vector3(0, 0, 0);
        }
        else if (TimeHandler.instance.waiting(timer, false))
        {
            moveMe.transform.position = new Vector3(10, 10, 10);
        }   
    }
}
