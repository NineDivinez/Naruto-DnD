using UnityEngine;
using UnityEngine.UI;

public class DropdownShortcuts : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject currentScreen;
    public AudioSource btnSFX;
    public Text errorMsg;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dropdown.Select();
            dropdown.value = dropdown.value +1;
            btnSFX.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dropdown.Select();
            dropdown.value = dropdown.value -1;
            btnSFX.Play();
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
                ChakraNatureSelector.instance.selected();
            }
            else
            {
                errorMsg.text = "My cabages!";
            }
        }
    }
}
