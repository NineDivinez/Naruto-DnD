using UnityEngine;
using UnityEngine.UI;

public class NewWindowControlLimiter : MonoBehaviour
{
    public static NewWindowControlLimiter instance = null;

    public void Awake()
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

    public Button[] buttonsToDisable;
    public InputField[] inputFieldsToDisable;
    public GameObject[] gameobjectsToDisable;

    public void initialize()
    {
        disableObjects("", "Stats/Notes Window");
    }
    public void close()
    {
        renableObjects("Stats/Notes Window");
    }

    public void disableObjects(string specific = "", string leave = "")
    {
        if (specific == "")
        {
            for (int i = 0; i < buttonsToDisable.Length; i++)
            {
                if (!buttonsToDisable[i].name.Contains(leave))
                {
                    buttonsToDisable[i].interactable = false;
                    print($"Now disabling {buttonsToDisable[i]}");
                }
            }

            for (int i = 0; i < inputFieldsToDisable.Length; i++)
            {
                if (!inputFieldsToDisable[i].name.Contains(leave))
                {
                    inputFieldsToDisable[i].interactable = false;
                    print($"Now disabling {inputFieldsToDisable[i]}");
                }
            }

            for (int i = 0; i < gameobjectsToDisable.Length; i++)
            {
                if (!gameobjectsToDisable[i].name.Contains(leave))
                {
                    gameobjectsToDisable[i].SetActive(false);
                    print($"Now disabling {gameobjectsToDisable[i]}");
                }
            }
        }
        
        else
        {
            for (int i = 0; i < buttonsToDisable.Length; i++)
            {
                if (buttonsToDisable[i].name.Contains(specific))
                {
                    buttonsToDisable[i].interactable = false;
                    print($"Now disabling {buttonsToDisable[i]}");
                }
            }

            for (int i = 0; i < inputFieldsToDisable.Length; i++)
            {
                if (inputFieldsToDisable[i].name.Contains(specific))
                {
                    inputFieldsToDisable[i].interactable = false;
                    print($"Now disabling {inputFieldsToDisable[i]}");
                }
            }

            for (int i = 0; i < gameobjectsToDisable.Length; i++)
            {
                if (gameobjectsToDisable[i].name.Contains(specific))
                {
                    gameobjectsToDisable[i].SetActive(false);
                    print($"Now disabling {gameobjectsToDisable[i]}");
                }
            }
        }
    }
    
    public void renableObjects(string leave = "")
    {
        for (int i = 0; i < buttonsToDisable.Length; i++)
        {
            if (!buttonsToDisable[i].name.Contains(leave))
            {
                buttonsToDisable[i].interactable = true;
                print($"Now enabling {buttonsToDisable[i]}");
            }
        }

        for (int i = 0; i < inputFieldsToDisable.Length; i++)
        {
            if (!inputFieldsToDisable[i].name.Contains(leave))
            {
                inputFieldsToDisable[i].interactable = true;
                print($"Now enabling {inputFieldsToDisable[i]}");
            }
        }

        for (int i = 0; i < gameobjectsToDisable.Length; i++)
        {
            if (!gameobjectsToDisable[i].name.Contains(leave))
            {
                gameobjectsToDisable[i].SetActive(true);
                print($"Now enabling {gameobjectsToDisable[i]}");
            }
        }
    }
}
