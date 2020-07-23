using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class CharacterName : MonoBehaviour
{
    //Game Objects
    public InputField input;
    public InputField consoleCommands;
    public GameObject questionOne;
    public GameObject createOptions;
    public GameObject loadOptions;
    public GameObject loadSave;
    public PlayerStats player = new PlayerStats();
    public Text errorMessage;
    public LoadPlayer loadPlayer;
    public AudioSource sfx;
    public GameObject commandPromptContainer;
    public GameObject notesContainer;

    //Variables
    public bool mainIsFocused;

    void Start()
    {
        errorMessage.text = "";
    }

    void Update()
    {
        if (!(Input.GetKeyDown(KeyCode.BackQuote)))
        {
            if (Input.anyKey)
            {
                if (!Input.GetKeyDown(KeyCode.Return))
                {
                    if (consoleCommands.isFocused == false)
                    {
                        if (!commandPromptContainer.activeInHierarchy)
                            input.ActivateInputField();
                    }
                }

                else if (Input.GetKey(KeyCode.Return))
                {
                    enterPressed();
                }
            }
        }

        if (Input.anyKey)
        {
            if (!Input.GetKeyDown(KeyCode.Return))
            {
                if (consoleCommands.isFocused == false)
                {
                    input.ActivateInputField();
                }
            }

            else if (Input.GetKey(KeyCode.Return))
            {
                enterPressed();
            }
        }

        if (consoleCommands.isFocused == true && mainIsFocused)
            mainIsFocused = false;
        else if (input.isFocused == true && !mainIsFocused)
            mainIsFocused = true;
    }

    void enterPressed()
    {
        if (!commandPromptContainer.activeInHierarchy && !notesContainer.activeInHierarchy)
        {
            if (input.text.ToLower() == "dave")
            {
                errorMessage.text = "I'm sorry dave, I'm afraid I can't do that.";
                print("I'm sorry dave, I'm afraid I can't do that.");
            }
            else
            {
                player.playerName = input.text;
                bool playerFound = loadPlayer.load();
                print(playerFound);
                userInput(playerFound);
            }
            sfx.Play();
        }
    }

    public void userInput(bool load)
    {
        errorMessage.text = "";
        if (input.text != "")
        {
            if (input.text.ToLower() == "dave")
            {
                errorMessage.text = "I'm sorry dave, I'm afraid I can't do that.";
                print("I'm sorry dave, I'm afraid I can't do that.");
            }
            else
            {
                print("Character name set to: " + input.text);
                player.playerName = input.text;
                player.exp = 1;
                loadPlayer = loadSave.GetComponent<LoadPlayer>();

                if (load)
                {
                    bool playerFound = loadPlayer.load();
                    if (playerFound)
                    {
                        questionOne.SetActive(false);
                        loadOptions.SetActive(true);
                    }
                    else
                    {
                        errorMessage.text = "Player file not found!";
                    }
                }
                else
                {
                    if (!loadPlayer.load())
                    {
                        questionOne.SetActive(false);
                        createOptions.SetActive(true);
                    }
                    else
                    {
                        errorMessage.text = "Player already exists!";
                    }
                }
            }
        }
        else
        {
            errorMessage.text = ("Error, nothing was put in!");
        }
    }
}
