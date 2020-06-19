using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;

public class Saving : MonoBehaviour
{
    //Game Objects
    public PlayerStats player = new PlayerStats();

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

    public void save(bool justSave)
    {
        errorMessage.text = "";
        string saveDirectory;
        string saveFile = player.playerName + ".save";
        if (!Directory.Exists("Game Saves"))
        {
            System.IO.Directory.CreateDirectory("Game Saves");
        }
        print("Saving character...");
        saveDirectory = Path.Combine("Game Saves", saveFile);

        string[] format = { formats[0] + player.playerName, formats[1] + player.exp, formats[2] + player.specialization, formats[3] + player.chakraLevels[0] + "," + player.chakraLevels[1] + "," + player.chakraLevels[2] + "," + player.chakraLevels[3] + "," + player.chakraLevels[4], formats[4] + player.strength, formats[5] + player.intelligence, formats[6] + player.dexterity, formats[7] + player.constitution, formats[8] + player.wisdom, formats[9] + player.charisma, formats[10] + player.chakraAffinity };
        System.IO.File.WriteAllLines(saveDirectory, format);
        
        load = load.GetComponent<LoadPlayer>();
        bool playerFound = load.load();

        if (playerFound && !justSave)
        {
            Application.Quit();
        }
        else if (justSave && playerFound)
        {
            errorMessage.text = ("Player successfully saved");
        }
        else
        {
            errorMessage.text = "There was an error saving the game....";
        }
    }
}
