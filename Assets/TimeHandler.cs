using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class TimeHandler : MonoBehaviour
{
    #region singleton
    public static TimeHandler instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton


    public List<float> timer = new List<float>();
    public List<float> originalWait = new List<float>();
    public List<float> originalWaitMax = new List<float>();
    public List<int> currentTime = new List<int>();
    public List<bool> finished = new List<bool>();


    //Timers
    public int newTimer(float toWaitMin, bool start, float toWaitMax = 0)
    {
        float timeToWait = 0;
        if (toWaitMax > 0)
        {
            timeToWait = Random.Range(toWaitMin, toWaitMax);
        }
        else
        {
            timeToWait = toWaitMin;
        }

        if (!start)
        {
            timer.Add(0);
        }
        else
        {
            timer.Add(timeToWait + Time.time);
        }

        originalWait.Add(toWaitMin);
        originalWaitMax.Add(toWaitMax);
        finished.Add(false);
        int newId = timer.Count - 1;
        print($"New timer created.  Id is: {newId}");

        return newId;
    }

    public void restartTimer(int id)
    {
        float timeToWait = originalWait[id];

        if (originalWaitMax[id] > 0)
        {
            timeToWait = Random.Range(originalWait[id], originalWaitMax[id]);
            print($"New time to wait: {timeToWait}");
        }

        timer[id] = timeToWait + Time.time;
        finished[id] = false;
    }

    public bool waiting(int id, bool restart)
    {
        if (timer[id] == 0)//Timer was told not to start yet and wait to be called.
        {
            timer[id] = originalWait[id] + Time.time;
        }

        if (timer[id] <= Time.time) //Timer has finished and is to be marked so.
        {
            finished[id] = true;
        }

        //Behaviors once finished.
        if (finished[id] && restart) //Timer was told to start and has since finished.  Needs to be reset.
        {
            restartTimer(id);
            return true;
        }
        else if (finished[id] && restart == false) //Timer has finished and is not supposed to restart!
        {
            print($"Timer {id} is done!  Returning true by default.  Was this intended?");
            return true;
        }

        return false; //All other truths are that the timer has yet to finish.  Return false!
    }

    public string checkTimer(int id, bool overTime)
    {
        int timeLeft = 0;
        if (!finished[id] && !overTime)
        {
            timeLeft = (int)(timer[id] - Time.time);
        }
        else if (overTime)
        {
            timeLeft = (int)(timer[id] - Time.time);
        }

        return $"Time remaining: {timeLeft}";
    }
}
