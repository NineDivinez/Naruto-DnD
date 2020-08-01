using UnityEngine;
using UnityEngine.UI;

public class DropdownShortcuts : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject currentScreen;
    public AudioSource btnSFX;
    public Text errorMsg;

    float delay = 0.5f;
    int timer;

    private void Start()
    {
        timer = TimeHandler.instance.newTimer(delay, true);
    }

    void Update()
    {
        //Initial press
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dropdown.Select();
            dropdown.value = dropdown.value + 1;
            btnSFX.Play();
            TimeHandler.instance.waiting(timer, true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dropdown.Select();
            dropdown.value = dropdown.value - 1;
            btnSFX.Play();
            TimeHandler.instance.waiting(timer, true);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (TimeHandler.instance.waiting(timer, true))
            {
                dropdown.Select();
                dropdown.value = dropdown.value + 1;
                btnSFX.Play();
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (TimeHandler.instance.waiting(timer, true))
            {
                dropdown.Select();
                dropdown.value = dropdown.value - 1;
                btnSFX.Play();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentScreen.name.Contains("Specializations"))
            {
                btnSFX.Play();
                SelectJob.instance.select();
            }
            else if (currentScreen.name.Contains("Chakra Natures"))
            {
                btnSFX.Play();
                ChakraNatureSelector.instance.finalized();
            }
            else
            {
                errorMsg.text = "My cabages!";
            }
        }
    }
}
