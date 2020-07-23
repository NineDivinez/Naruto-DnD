using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWindowControlLimiter : MonoBehaviour
{
    public Button[] buttonsToDisable;
    public InputField[] inputFieldsToDisable;
    public GameObject[] gameobjectsToDisable;
    public void disableObjects()
    {
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].interactable = false;
        }

        for (int i = 0; i < inputFieldsToDisable.Length; i++)
        {
            inputFieldsToDisable[i].interactable = false;
        }

        for (int i = 0; i < gameobjectsToDisable.Length; i++)
        {
            gameobjectsToDisable[i].SetActive(false);
        }
    }
    
    public void renableObjects()
    {
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].interactable = true;
        }

        for (int i = 0; i < inputFieldsToDisable.Length; i++)
        {
            inputFieldsToDisable[i].interactable = true;
        }

        for (int i = 0; i < gameobjectsToDisable.Length; i++)
        {
            gameobjectsToDisable[i].SetActive(true);
        }
    }
}
