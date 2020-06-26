using System;
using System.IO;
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
    public AudioSource godMode;

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

                else if (Input.GetKeyUp(KeyCode.Return))
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

                if (dice != null || dice != 0)
                {
                    int[] rolled = new int[times];
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

                    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
                    string playerLocation = "";
                    playerLocation = Path.Combine(directory, player.playerName + ".save");
                    bool playerDetected = System.IO.File.Exists(playerLocation);

                    if (playerDetected)
                    {
                        output.text = (player.playerName + " loaded successfully.");
                        homeScreen.SetActive(false);
                        loadScreen.SetActive(true);
                    }
                    else
                    {
                        errorMessage.text = "The player could not be found.";
                        output.text = directory + exists(playerDetected);
                    }
                }
                else
                {
                    output.text = "This command cannot be performed at the current state.  Please exit character creation and try again.";
                }
            }

            //kill
            else if (commandEntry.text.ToLower().StartsWith("kill") || commandEntry.text.ToLower().StartsWith("exterminate"))
            {
                int audioChosen = Random.Range(10, 50) /10;
                print(audioChosen);

                switch (audioChosen)
                {
                    case 1:
                        turret1.Play();
                        output.text = "Target " + player.playerName + " aquired...";
                        break;

                    case 2:
                        turret2.Play();
                        output.text = "There you are " + player.playerName + "...";
                        break;

                    case 3:
                        turret3.Play();
                        output.text = "Helloooo " + player.playerName + "!";
                        break;

                    case 4:
                        turret4.Play();
                        output.text = "Target " + player.playerName + " Lost ...";
                        break;

                    case 5:
                        turret5.Play();
                        output.text = "Searching for " + player.playerName + "...";
                        break;
                }
                commandEntry.text = "";
            }

            //Godmode
            else if (commandEntry.text.ToLower().StartsWith("godmode"))
            {
                if (godMode.isPlaying)
                {
                    godMode.Stop();
                    output.text = "";
                }
                else
                {
                    godMode.Play();
                    output.text = "LOL you THOUGHT!";
                }
            }

            else if (commandEntry.text.ToLower().StartsWith("directory"))
            {
                if (commandEntry.text.Length > 9)
                {
                    string directoryName = commandEntry.text.Remove(0, 10);
                    string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10");
                    saveLocation = Path.Combine(saveLocation, "NarutoDnD");
                    saveLocation = Path.Combine(saveLocation, "Game Saves");
                    if (Directory.Exists(saveLocation))
                    {
                        errorMessage.text = "";
                        

                        if (directoryName != "")
                        {
                            string saveFile = Path.Combine(saveLocation, directoryName + ".save");
                            bool playerFound = System.IO.File.Exists(saveFile);
                            if (playerFound)
                                output.text = saveFile;
                            else
                            {
                                errorMessage.text = "There was an error displaying the directory...";
                                output.text = "Player " + directoryName + ".save" + exists(playerFound) + "\n\nPlayer Name: " + directoryName + "\n\nGame Save Location: " + saveLocation + exists(Directory.Exists(saveFile));
                            }
                        }
                        else
                        {
                            errorMessage.text = "Error displaying directory!";
                            output.text = "You must provide a save to check the directory for.";
                        }
                    }
                }
                else
                {
                    string directoryName = commandEntry.text.Remove(0, 9);
                    string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves" + "/" + directoryName + ".save");
                    string saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
                    output.text = directoryName + ".save" + exists(Directory.Exists(saveLocation)) + "\n" + saveFolder + exists(Directory.Exists(saveFolder));
                    errorMessage.text = "Command is invalid!";
                }
            }

            //help
            else if (commandEntry.text.ToLower().StartsWith("help"))
            {
                //name, specialization, exp, chakra affinity, chakra nature levels, str, int, dex, con, wis, charis
                output.text = ("Commands are as follows:\n" +
                    "Roll\n" +
                    "Godmode" +
                    "Create <Name> <specialization> <EXP> <Chakra Affinity> <Chakra Nature levels (Fire,Water,Air,Earth,Lighting)> <Strength> <Intelligence> <Dexterity> <Constitution> <Wisdom> <Charisma>\n" +
                    "Load <player name>\n" +
                    "Kill <target>\n" + 
                    "Directory (Displays the current save directory)");
            }

            //Invalid
            else
            {
                errorMessage.text = ("Error! Invalid command.  For a list of commands, please enter help.");
            }
        }
    }

    public string exists(bool passedBool) //converts true/false to yes or no
    {
        return passedBool ? " does exist." : " does not exist.";
    }

    bool entryCheck(string entry)
    {
        if (entry != "")
            return false;
        else
            return true;
    }
}
