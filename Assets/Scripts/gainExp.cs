﻿using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class gainExp : MonoBehaviour
{
    //GameObjects
    public InputField userInput;
    public Text errorMessage;

    public PlayerStats player = new PlayerStats();

    public GameObject levelUp;
    public GameObject loadScreen;
    public AudioSource sfx;
    public GameObject commandPromptContainer;
    
    //Variables
    public int startingLevel = 0;
    public int newLevel;
    public int[] requiredXP = { 0, 300 , 900, 2700, 6500, 1400, 23000, 34000, 48000, 64000, 85000, 100000, 120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 };
    public bool firstLoad = true;

    void Update()
    {
        if (Input.anyKey)
        {
            if (!commandPromptContainer.activeInHierarchy)
            {
                if (!Input.GetKeyDown(KeyCode.Return))
                {
                    userInput.ActivateInputField();
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    checkLevel(true);
                    sfx.Play();
                }
            }
        }

        checkLevel(false);
    }

    public void checkLevel(bool button)
    {
        if (button)
        {
            try
            {
                player.exp += Int32.Parse(userInput.text);
            }
            catch (FormatException)
            {
                if (userInput.text != "")
                    errorMessage.text = "Invalid.  Please enter a number value.";
            }
        }
        
        if (player.exp > requiredXP[19])
            player.exp = requiredXP[19];

        for (int i = 0; i <= 19; i++)
        {
            if (i != 19)
            {
                if (requiredXP[i] <= player.exp && requiredXP[i + 1] > player.exp)
                {
                    newLevel = i + 1;
                }
            }
            else if (requiredXP[i] <= player.exp)
            {
                newLevel = i + 1;
            }
        }
        player.exp = player.exp;
        player.playerLevel = newLevel;

        if (player.playerLevel > startingLevel)
        {
            print("Player level: " + player.playerLevel + "\n" + "Starting level:" + startingLevel);
            levelUpPlayer(button);
        }
    }

    public void levelUpPlayer(bool button)
    {
        if (firstLoad && button)
        {
            firstLoad = false;
        }
        else
        {
            if (startingLevel < 3)
            {
                if (player.playerLevel >= 3) //Checks if the player has reached level 3 as a result of the level up.
                {
                    //The player can now customize their specialization further by picking new feats unique to their character.
                    //This is the first level they can try to learn a new chakra nature. Bring up a screen asking if they wish to try to learn a new natue.
                }
            }

            if (player.playerLevel % 3 == 0) //Checks if the level is divisible by 3 to allow the player to choose if they want to either learn a new chakra nature or level up an existing one other than the primary
            {
                //Bring up a screen asking if they wish to try to learn a new natue.
            }
        }
        

        //The last thing to happen:
        startingLevel = player.playerLevel;
        //compares previous level to new level.  If changed, update the previous level to the new and check if the player gains anything new.
        //if true, then give player their options!
    }

    void learnNewNature()
    {

    }
}
