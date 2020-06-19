using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConsoleCommands : MonoBehaviour
{
    //Game Objects
    public Text errorMessage;
    public InputField commandEntry;
    public Text output;
    public AudioSource typingSource;
    public AudioClip typeClip;
    public AudioSource sfx;
    public GameObject commandPromptContainer;

    public GameObject antiCreateLoad;

    public GameObject loadScreen;
    public GameObject homeScreen;

    public AudioSource turret1;
    public AudioSource turret2;
    public AudioSource turret3;
    public AudioSource turret4;
    public AudioSource turret5;

    public LoadPlayer load = new LoadPlayer();
    public PlayerStats player = new PlayerStats();
    public Saving saveCharacter = new Saving();

    //Variables
    public bool isFocused;

    //Make reference to scripts for creation, loading, and each section so the script knows where the user is

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            commandEntered();
            sfx.Play();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
            commandPromptContainer.SetActive(!commandPromptContainer.activeInHierarchy);

        else if (!(Input.GetKeyDown(KeyCode.BackQuote)))
        {
            if (Input.anyKey)
            {
                if (!Input.GetKeyDown(KeyCode.Return))
                {
                    if (commandPromptContainer.activeInHierarchy)
                        commandEntry.ActivateInputField();
                }

                else if (Input.GetKey(KeyCode.Return))
                {
                    commandEntered();
                    sfx.Play();
                }
            }
            if (commandEntry.isFocused == true && Input.anyKeyDown)
            {
                typingSource.PlayOneShot(typeClip, 1f);
            }
        }


        if (commandEntry.isFocused == true && !isFocused)
            isFocused = true;
        else if (commandEntry.isFocused == false && isFocused)
            isFocused = false;
    }

public void commandEntered()
    {
        errorMessage.text = "";

        if (isFocused)
        {
            //Roll
            if (commandEntry.text.ToLower().StartsWith("roll"))
            {
                string[] entries = commandEntry.text.Split(' ');
                int dice = Int32.Parse(entries[1]);
                int times = 1;
                int mods = 0;

                string format = "Rolled:\n";

                if (entries.Length >= 3)
                {
                    times = Int32.Parse(entries[2]);
                    if (entries.Length == 4)
                    {
                        mods = Int32.Parse(entries[3]);
                    }
                }

                if (dice != null)
                {
                    int[] rolled = new int[times];
                    int timesRan = 0;
                    for (int i = 0; i <= times - 1; i++)
                        rolled[i] = Random.Range(1, dice) + mods;

                    for (int i = 0; i < rolled.Length; i++)
                    {
                        if (i < rolled.Length - 1)
                        {
                            format += rolled[i].ToString() + ",   ";
                        }
                        else
                        {
                            format += rolled[i].ToString();
                        }
                    }

                    output.text = format;
                }
                else
                {
                    errorMessage.text = "Command failed.";
                }
            }

            //creation
            else if (commandEntry.text.ToLower().StartsWith("create"))
            {
                //create a check to ensure the user cannot perform this command when in the middle of creating. Use a method for this that returns true or false.
                //create test tester 9000 TestingPowers 1,0,0,0,0 10 10 10 10 10 10
                if (!antiCreateLoad.activeInHierarchy)
                {
                    int numberToCreate = 1;
                    //name, specialization, exp, chakra affinity, chakra nature levels, str, int, dex, con, wis, charis
                    string input = commandEntry.text.Remove(0, 7);
                    string[] entries = input.Split(' ');
                    if (entries.Length < 11)
                    {
                        errorMessage.text = "You did not enter enough to create a character.  Please try again.";
                    }
                    else if (entries.Length == 11)
                    {
                        string[] chakraLevels = (entries[5].Split(',')); //Still need to convert this and assign them to their respective chakra nature

                        for (int i = 0; i < chakraLevels.Length; i++)
                        {
                            player.chakraLevels[i] = Int32.Parse(chakraLevels[i]);
                        }

                        //assign the chakra natures based on the levels.
                        bool failed = true;
                        for (int i = 0; i < entries.Length; i++)
                        {
                            failed = entryCheck(entries[i]);
                        }

                        if (!failed)
                        {
                            player.playerName = entries[0]; player.specialization = entries[1]; player.exp = Int32.Parse(entries[2]); player.chakraAffinity = entries[3];
                            player.strength = Int32.Parse(entries[5]); player.intelligence = Int32.Parse(entries[6]); player.dexterity = Int32.Parse(entries[7]);
                            player.wisdom = Int32.Parse(entries[8]); player.charisma = Int32.Parse(entries[9]);

                            if (!load.load())
                            {
                                saveCharacter.save(true);
                                output.text = (player.playerName + " save successfully.");
                                homeScreen.SetActive(false);
                                loadScreen.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        errorMessage.text = "You entered too many entries for the command.  Please try again.";
                    }
                }
            }

            //load
            else if (commandEntry.text.ToLower().StartsWith("load"))
            {
                //create a check to ensure the user cannot perform this command when in the middle of creating. Use a method for this that returns true or false.
                if (!antiCreateLoad.activeInHierarchy)
                {
                    player.playerName = commandEntry.text.Remove(0, 5);
                    if (load.load())
                    {
                        output.text = (player.playerName + " loaded successfully.");
                        homeScreen.SetActive(false);
                        loadScreen.SetActive(true);
                    }
                }
            }

            //kill
            else if (commandEntry.text.ToLower().StartsWith("kill") || commandEntry.text.ToLower().StartsWith("exterminate"))
            {
                int audioChosen = Random.Range(1, 5);

                switch (audioChosen)
                {
                    case 1:
                        turret1.Play();
                        output.text = "Target aquired...";
                        break;

                    case 2:
                        turret2.Play();
                        output.text = "There you are...";
                        break;

                    case 3:
                        turret3.Play();
                        output.text = "Helloooo!";
                        break;

                    case 4:
                        turret4.Play();
                        output.text = "Target Lost...";
                        break;

                    case 5:
                        turret5.Play();
                        output.text = "Searching...";
                        break;
                }
                commandEntry.text = "";
            }

            //help
            else if (commandEntry.text.ToLower().StartsWith("help"))
            {
                //name, specialization, exp, chakra affinity, chakra nature levels, str, int, dex, con, wis, charis
                output.text = ("Commands are as follows:\n" +
                    "Roll\n" +
                    "Create <Name> <specialization> <EXP> <Chakra Affinity> <Chakra Nature levels (Fire,Water,Air,Earth,Lighting)> <Strength> <Intelligence> <Dexterity> <Constitution> <Wisdom> <Charisma>\n" +
                    "Load <player name>\n" +
                    "Kill");
            }

            //Invalid
            else
            {
                errorMessage.text = ("Error! Invalid command.  For a list of commands, please enter help.");
            }
        }
    }

    bool entryCheck(string entry)
    {
        if (entry != "")
            return false;
        else
            return true;
    }
                        /*
                        //create shortcut
                        else if (userInput.StartsWith("create") || userInput.StartsWith("Create"))
                        {
                            bool specificBuild = userInput.Any(c => char.IsDigit(c));
                            if (specificBuild)
                            {
                                string[] entries = userInput.Split(' ');

                                if (entries.Length == 11)
                                {
                                    Console.WriteLine("You have entered:");
                                    foreach (string entered in entries)
                                    {
                                        Console.WriteLine(entered);
                                    }
                                    create(entries[1], Int32.Parse(entries[2]), entries[3], entries[4], Int32.Parse(entries[5]), Int32.Parse(entries[6]), Int32.Parse(entries[7]), Int32.Parse(entries[8]), Int32.Parse(entries[9]), Int32.Parse(entries[10]));
                                }
                                else if (entries.Length< 11)
                                {
                                    Console.WriteLine("You did not enter enough entries.");
                                }
                                else if (entries.Length > 11)
                                {
                                    Console.WriteLine("You entered too many entries.");
                                }

                                consoleClear();
                            }
                            else
                            {
                                string creationName = userInput.Remove(0, 7);
create(creationName);
                            }
                        }

                        //Load Player
                        else if (userInput.StartsWith("load") || userInput.StartsWith("Load"))
                        {
                            string loadingName = userInput.Remove(0, 5);
Console.WriteLine("Now searching for {0}...", loadingName);
                            string directory = "../../Saves/";
string playerLocation = directory + "/" + loadingName + ".save";
bool detected = File.Exists(playerLocation);
                            if (detected)
                            {
                                readAll(playerLocation);
Console.WriteLine("Player data found!");
                                consoleClear();
                            }
                            else
                            {
                                Console.WriteLine("Format error! Please try again!");
                            }
                        }
                        */
}
