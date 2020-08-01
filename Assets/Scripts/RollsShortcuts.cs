using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RollsShortcuts : MonoBehaviour
{
    public AudioSource btnSFX;

    public Dropdown[] dice = new Dropdown[6];
    public int current = 0;

    int timerUp;
    int timerDown;
    float delay = 0.3f;

    private void Start()
    {
        timerUp = TimeHandler.instance.newTimer(delay, true);
        timerDown = TimeHandler.instance.newTimer(delay, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            btnSFX.Play();
            dice[current].value = dice[current].value - 1;
            TimeHandler.instance.waiting(timerUp, true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            btnSFX.Play();
            dice[current].value = dice[current].value + 1;
            TimeHandler.instance.waiting(timerDown, true);
        }

        dice[current].Select();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            btnSFX.Play();
            RollForStats.instance.rollDice();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (TimeHandler.instance.waiting(timerUp, true))
            {
                btnSFX.Play();
                dice[current].value = dice[current].value - 1;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (TimeHandler.instance.waiting(timerDown, true))
            {
                btnSFX.Play();
                dice[current].value = dice[current].value + 1;
            }
        }

        //test
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dice[current].value = dice[current].value + 1;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                current--;
            else
                current++;

            if (current >= 6)
                current = 0;
            else if (current < 0)
            {
                current = 5;
            }
            btnSFX.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            btnSFX.Play();
            RollForStats.instance.saveRolls();
        }
    }
}
