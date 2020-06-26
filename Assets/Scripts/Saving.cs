using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Random = UnityEngine.Random;

public class Saving : MonoBehaviour
{
    //Game Objects
    public PlayerStats player = new PlayerStats();
    public RemoveStats clear = new RemoveStats();

    public Text errorMessage;
    public LoadPlayer load;

    //Variables
    public static string[] formats = {
            "Name:           ",
            "Level:          ",
            "Specialization: ",
            "Chakra Nature:  ",
            "Strength:       ",
            "Intelligence:   ",
            "Dexterity:      ",
            "Constitution:   ",
            "Wisdom:         ",
            "Charisma:       ",
            "Chakra Affinity: "};

    void Start()
    {
        memeMan();
    }

    public void save(bool justSave)
    {
        errorMessage.text = "";
        string saveDirectory;
        bool playerFound = false;

        if (player.playerName != "")
        {
            string saveFile = player.playerName + ".save";
            string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");


            if (!Directory.Exists(saveLocation))
            {
                System.IO.Directory.CreateDirectory(saveLocation);
            }
            errorMessage.text = ("Saving character...");
            saveDirectory = Path.Combine(saveLocation, saveFile);

            string[] format = { formats[0] + player.playerName, formats[1] + player.exp, formats[2] + player.specialization, formats[3] + player.chakraLevels[0] + "," + player.chakraLevels[1] + "," + player.chakraLevels[2] + "," + player.chakraLevels[3] + "," + player.chakraLevels[4], formats[4] + player.strength, formats[5] + player.intelligence, formats[6] + player.dexterity, formats[7] + player.constitution, formats[8] + player.wisdom, formats[9] + player.charisma, formats[10] + player.chakraAffinity };
            System.IO.File.WriteAllLines(saveDirectory, format);

            load = load.GetComponent<LoadPlayer>();
            playerFound = load.load();
        }
        else if (player.playerName == "" && !justSave)
        {
            Application.Quit();
        }

        
        if (playerFound && !justSave)
        {
            save(false);
            Application.Quit();
        }
        else if (justSave && playerFound)
        {
            errorMessage.text = ("Player successfully saved");
        }
        else
        {
            errorMessage.text = "There was an error saving the character....";
        }
    }

    void memeMan()
    {
        string saveDirectory;
        string saveFile = "MysteryCharacter.save";
        string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");


        if (!Directory.Exists(saveLocation))
        {
            System.IO.Directory.CreateDirectory(saveLocation);
        }
        print("Saving character...");
        saveDirectory = Path.Combine(saveLocation, saveFile);

        string[] format = new string[11];

        switch (Random.Range(1, 6))
        {
            case 1:
                format[0] = formats[0] + "Gunthorian";                                          //Name
                format[1] = formats[1] + 9001;                                                  //exp
                format[2] = formats[2] + "The Mighty";                                          //Specialization
                format[3] = formats[3] + 300 + "," + 300 + "," + 300 + "," + 300 + "," + 300;   //Chakra Nature Levels
                format[4] = formats[4] + 25;                                                    //Strength
                format[5] = formats[5] + 300;                                                   //Intelligence
                format[6] = formats[6] + 25;                                                    //Dexterity
                format[7] = formats[7] + 25;                                                    //Constitution
                format[8] = formats[8] + 25;                                                    //Wisdom
                format[9] = formats[9] + 300;                                                   //Charisma
                format[10] = formats[10] + "All of them";                                       //Chakra Affinity
                break;

            case 2:
                format[0] = formats[0] + "Caboose";                                             //Name
                format[1] = formats[1] + -600000;                                               //exp
                format[2] = formats[2] + "Tha smartiest... Yup.";                               //Specialization
                format[3] = formats[3] + 0 + "," + 10 + "," + 0 + "," + 0 + "," + 0;            //Chakra Nature Levels
                format[4] = formats[4] + 400;                                                   //Strength
                format[5] = formats[5] + -400;                                                  //Intelligence
                format[6] = formats[6] + 25;                                                    //Dexterity
                format[7] = formats[7] + 25;                                                    //Constitution
                format[8] = formats[8] + 1;                                                     //Wisdom
                format[9] = formats[9] + -400;                                                  //Charisma
                format[10] = formats[10] + "Yes";                                               //Chakra Affinity
                break;

            case 3:
                format[0] = formats[0] + "Meme Man";                                             //Name
                format[1] = formats[1] + 999999999;                                              //exp
                format[2] = formats[2] + "∞";                                                    //Specialization
                format[3] = formats[3] + 999 + "," + 999 + "," + 999 + "," + 999 + "," + 999;    //Chakra Nature Levels
                format[4] = formats[4] + 999;                                                    //Strength
                format[5] = formats[5] + 999;                                                    //Intelligence
                format[6] = formats[6] + 999;                                                    //Dexterity
                format[7] = formats[7] + 999;                                                    //Constitution
                format[8] = formats[8] + 999;                                                    //Wisdom
                format[9] = formats[9] + 999;                                                    //Charisma
                format[10] = formats[10] + "The Universe";                                       //Chakra Affinity
                break;

            case 4:
                format[0] = formats[0] + "Tehpei";                                              //Name
                format[1] = formats[1] + 165000;                                                //exp
                format[2] = formats[2] + "Archer";                                              //Specialization
                format[3] = formats[3] + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0;             //Chakra Nature Levels
                format[4] = formats[4] + 10;                                                    //Strength
                format[5] = formats[5] + 15;                                                    //Intelligence
                format[6] = formats[6] + 20;                                                    //Dexterity
                format[7] = formats[7] + 16;                                                    //Constitution
                format[8] = formats[8] + 15;                                                    //Wisdom
                format[9] = formats[9] + 13;                                                    //Charisma
                format[10] = formats[10] + "";                                                  //Chakra Affinity
                break;

            case 5:
                format[0] = formats[0] + "Xilinisha";                                           //Name
                format[1] = formats[1] + 225000;                                                //exp
                format[2] = formats[2] + "Paladin";                                             //Specialization
                format[3] = formats[3] + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0;             //Chakra Nature Levels
                format[4] = formats[4] + 20;                                                    //Strength
                format[5] = formats[5] + 18;                                                    //Intelligence
                format[6] = formats[6] + 14;                                                    //Dexterity
                format[7] = formats[7] + 15;                                                    //Constitution
                format[8] = formats[8] + 17;                                                    //Wisdom
                format[9] = formats[9] + 14;                                                    //Charisma
                format[10] = formats[10] + "";                                                  //Chakra Affinity
                break;

            case 6:
                format[0] = formats[0] + "Empty";                                               //Name
                format[1] = formats[1] + 0;                                                     //exp
                format[2] = formats[2] + "Being Blank";                                         //Specialization
                format[3] = formats[3] + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0;             //Chakra Nature Levels
                format[4] = formats[4] + 0;                                                     //Strength
                format[5] = formats[5] + 0;                                                     //Intelligence
                format[6] = formats[6] + 0;                                                     //Dexterity
                format[7] = formats[7] + 0;                                                     //Constitution
                format[8] = formats[8] + 0;                                                     //Wisdom
                format[9] = formats[9] + 0;                                                     //Charisma
                format[10] = formats[10] + "The blankest";                                      //Chakra Affinity
                break;
        }

        System.IO.File.WriteAllLines(saveDirectory, format);
        print(load.load()); //Tries to tell me if it saved right
        clear.removeLoaded(); //Clears the stats that were loaded as a result of the test.
    }
}
