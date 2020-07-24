using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConsoleCommands : MonoBehaviour
{
    
    /*
     Notes for future updates:
    edit the create command to use the new method to roll stats if no stats are put in.
    allow edit command to randomly select a chakra nature and/or specialization
    try to make the create command accept just a name and randomly pick things for the rest!
    Make more changes to the help command so you can get in dept details on all commands.

     */

    //Game Objects
    public Text errorMessage;
    public InputField commandEntry;
    public Text output;
    public AudioSource typingSource;
    public AudioClip typeClip;
    public AudioSource sfx;
    public GameObject commandPromptContainer;
    public GameObject notesContainer;

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
        if (isFocused && !notesContainer.activeInHierarchy)
        {
            errorMessage.text = "";
            sfx.Play();
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
                    addOutput("Command failed: Unknown error!");
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
                    //name, specialization, exp, chakra affinity, chakra nature levels, str, int, dex, con, wis, charis
                    string input = commandEntry.text.Remove(0, 7);
                    string[] entries = input.Split(' ');
                    if (entries.Length < 11)
                    {
                        errorMessage.text = "You did not enter enough to create a character.  Please try again.";
                    }
                    else if (entries.Length == 11)
                    {
                        string[] chakraLevels = { "", "", "", "", "" };
                        if (entries[4].Contains(","))
                        {
                            chakraLevels = (entries[4].Split(','));
                        }
                        else
                        {
                            chakraLevels[0] = (entries[4]);
                        }

                        for (int i = 0; i < chakraLevels.Length; i++) //assigns the levels to the respective chakra nature.
                        {
                            if (chakraLevels[i].ToLower().StartsWith("fire"))
                            {
                                player.chakraLevels[0] = Int32.Parse(chakraLevels[i].Remove(0, 5));
                            }
                            else if (chakraLevels[i].ToLower().StartsWith("water"))
                            {
                                player.chakraLevels[1] = Int32.Parse(chakraLevels[i].Remove(0, 6));


                                print((chakraLevels[i]));
                                print(Int32.Parse(chakraLevels[i].Remove(0, 6)));
                                print(player.chakraLevels[1]);


                            }
                            else if (chakraLevels[i].ToLower().StartsWith("air"))
                            {
                                player.chakraLevels[2] = Int32.Parse(chakraLevels[i].Remove(0, 4));
                            }
                            else if (chakraLevels[i].ToLower().StartsWith("earth"))
                            {
                                player.chakraLevels[3] = Int32.Parse(chakraLevels[i].Remove(0, 6));
                            }
                            else if (chakraLevels[i].ToLower().StartsWith("lightning"))
                            {
                                player.chakraLevels[4] = Int32.Parse(chakraLevels[i].Remove(0, 10));
                            }
                        }

                        bool failed = true;
                        for (int i = 0; i < entries.Length; i++)
                        {
                            failed = entryCheck(entries[i]);
                        }

                        if (!failed)
                        {
                            player.playerName = entries[0]; player.specialization = entries[1]; player.exp = Int32.Parse(entries[2]); player.chakraAffinity = entries[3];
                            player.strength = Int32.Parse(entries[5]); player.intelligence = Int32.Parse(entries[6]); player.dexterity = Int32.Parse(entries[7]);
                            player.constitution = Int32.Parse(entries[8]); player.wisdom = Int32.Parse(entries[9]); player.charisma = Int32.Parse(entries[10]);

                            if (!load.load())
                            {
                                saveCharacter.save(true);
                                addOutput(player.playerName + " save successfully.");
                                homeScreen.SetActive(false);
                                loadScreen.SetActive(true);
                                if (!load.load())
                                {
                                    errorMessage.text = "Save Failed";
                                    addOutput("Save failed.  Try relaunching as administrator?");
                                }
                            }
                        }
                    }
                    else
                    {
                        errorMessage.text = "You gave too many entries for the command.  Please try again.";
                    }
                }
                else
                {
                    addOutput("You cannot create characters when you're in the middle of creating one...");
                    errorMessage.text = "You cannot create characters when you're in the middle of creating one...";
                }
            }

            //load
            else if (commandEntry.text.ToLower().StartsWith("load"))
            {
                //create a check to ensure the user cannot perform this command when in the middle of creating. Use a method for this that returns true or false.
                if (!antiCreateLoad.activeInHierarchy)
                {


                    string loadName = commandEntry.text.Remove(0, 5);

                    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
                    string playerLocation = Path.Combine(directory, loadName + ".save");
                    bool playerDetected = System.IO.File.Exists(playerLocation);

                    if (playerDetected)
                    {
                        player.playerName = loadName;
                        load.load();
                        addOutput(player.playerName + " loaded successfully.");
                        homeScreen.SetActive(false);
                        loadScreen.SetActive(true);
                    }
                    else
                    {
                        errorMessage.text = "Checked if the save went through... The player could not be found.";
                        addOutput($"Could not find character {player.playerName}\n");
                        addOutput(directory + exists(playerDetected) + "\n\nDoes the toolkit have admin permissions?");
                    }
                }
                else
                {
                    addOutput("You cannot create or characters when you're in the middle of creating one...");
                    errorMessage.text = "You cannot create or characters when you're in the middle of creating one...";
                }
            }

            //list
            else if (commandEntry.text.ToLower().StartsWith("list"))
            {
                output.text = "All save files that could be located:";
                DirectoryInfo directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves"));
                FileInfo[] Files = directory.GetFiles("*.save"); //Getting save files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    if (str == "")
                        str = file.Name;
                    else
                        str += $"\n {file.Name}";
                }
                addOutput(str);
            }

            //delete
            else if (commandEntry.text.ToLower().StartsWith("delete"))
            {
                string deleteCharacter = commandEntry.text.Remove(0, 7);
                string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
                string playerLocation = Path.Combine(directory, deleteCharacter + ".save");
                string playerNotes = Path.Combine(directory, deleteCharacter + ".notes");
                string playerTraits = Path.Combine(directory, deleteCharacter + ".traits");

                if (player.playerName.ToLower() == deleteCharacter.ToLower())
                {
                    addOutput("You cannot delete a character that is currently loaded.");
                }
                else
                {
                    if (System.IO.File.Exists(playerLocation))
                    {
                        File.Delete(playerLocation);
                        File.Delete(playerNotes);
                        File.Delete(playerTraits);
                        addOutput($"Now deleting {deleteCharacter}");
                    }
                    else
                    {
                        addOutput("The file you want to delete already does not exist.");
                    }
                }
            }

            //kill
            else if (commandEntry.text.ToLower().StartsWith("kill") || commandEntry.text.ToLower().StartsWith("exterminate"))
            {
                int audioChosen = Random.Range(10, 50) / 10;
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

            //Directory
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
                            {
                                output.text = ($"The save file is located at:\n\n {saveFile}");
                            }
                            else
                            {
                                errorMessage.text = "There was an error displaying the directory...";
                                output.text = "Player " + directoryName + ".save" + exists(playerFound) + "\n\nPlayer Name: " + directoryName + "\n\nDirectory:   " + saveLocation + exists(Directory.Exists(saveLocation)) + "\n\nDoes the toolkit have admin permissions?";
                            }
                        }
                        else
                        {
                            errorMessage.text = "Error displaying directory!";
                            addOutput("You must provide a save to check the directory for.");
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

                if (commandEntry.text.Length == 4)
                {
                    output.text = ("Commands are as follows:\n" +
                    "Roll <sides> <amount> (Rolls die based on the sides the specified times.\n\n" +
                    "Godmode (Enables godmode)\n\n" +
                    "Create <Name> <specialization> <EXP> <Chakra Affinity> <Chakra Nature levels (Fire:level,Water:level,Air:level,Earth:level,Lighting:level)> <Strength> <Intelligence> <Dexterity> <Constitution> <Wisdom> <Charisma>\n\n" +
                    "Load <Name> (quickly loads the name)\n\n" +
                    "Kill <target>\n\n" +
                    "Directory <Name> (Displays the directory of the name entered.)\n\n" +
                    "Delete <character>\n\n" +
                    "list (Lists all saved characters)\n\n" +
                    "help image (instructions on how to upload your own image)");
                }
                else if (commandEntry.text.Remove(0, 5).ToLower() == "image")
                {
                    output.text = ("To add an image for your character, enter the command: directory <name> and go to that directory.\n\n" +
                        "If the image you have is not already in a jpg format, please convert it as such and rename the image to your character name.\n\n" +
                        "After that, place the image in the directory.  You may now reload your character with the new image!");
                }
                else
                {
                    output.text = ("Command not recognized.  Performing help command...\n\n" +
                    "Commands are as follows:\n" +
                    "Roll <sides> <amount> (Rolls die based on the sides the specified times.\n\n" +
                    "Godmode (Enables godmode)\n\n" +
                    "Create <Name> <specialization> <EXP> <Chakra Affinity> <Chakra Nature levels (Fire:level,Water:level,Air:level,Earth:level,Lighting:level)> <Strength> <Intelligence> <Dexterity> <Constitution> <Wisdom> <Charisma>\n\n" +
                    "Load <Name> (quickly loads the name)\n\n" +
                    "Kill <target>\n\n" +
                    "Directory <Name> (Displays the directory of the name entered.)\n\n" +
                    "Delete <character>\n\n" +
                    "help image (instructions on how to upload your own image)");
                }
            }

            //Testing scrolling
            else if (commandEntry.text.ToLower().StartsWith("test"))
            {
                for (int i = 0; i <= 100; i++)
                {
                    addOutput("This is a test!");
                }
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

    public void addOutput(string toOutput)
    {
        if (output.text == "")
        {
            output.text = toOutput;
        }
        else
        {
            output.text += $"\n\n-----------------------------------------------------------------------\n\n{toOutput}";
        }
    }

    bool entryCheck(string entry)
    {
        if (entry != "")
            return false;
        else
            return true;
    }
}
