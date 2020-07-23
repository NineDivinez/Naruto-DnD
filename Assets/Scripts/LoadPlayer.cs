using System;
using System.IO;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    //Game Objects
    public string[] available = { "Fire", "Water", "Wind", "Earth", "Lightning" };
    public PlayerStats player = new PlayerStats();

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

    public bool load()
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
        string playerLocation = "";
        playerLocation = Path.Combine(directory, player.playerName + ".save");
        bool detected = System.IO.File.Exists(playerLocation);

        //If directory is real, load the player.
        if (detected == true)
        {
            print("The user was successfully located!");
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(playerLocation);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Name:"))
                {
                    player.playerName = (line.Remove(0, formats[0].Length));
                }
                else if (line.StartsWith("Level:"))
                {
                    player.exp = int.Parse((line.Remove(0, formats[1].Length)));
                }
                else if (line.StartsWith("Specialization:"))
                {
                    player.specialization = line.Remove(0, formats[2].Length);
                }
                else if (line.StartsWith("Chakra Nature:")) //This needs to go through and disable all the chakra natures that are not being used
                {
                    string[] chakraArray = line.Remove(0, formats[3].Length).Split(',');
                    for (int i = 0; i <= 4; i++)
                    {   
                        player.chakraLevels[i] = int.Parse(chakraArray[i]);
                        if (player.chakraLevels[i] >= 1)
                        {
                            player.chakraNatures[i] = available[i];
                        }
                    }
                }
                else if (line.StartsWith("Strength:"))
                {
                    player.strength = int.Parse((line.Remove(0, formats[4].Length)));
                }
                else if (line.StartsWith("Intelligence:"))
                {
                    player.intelligence = int.Parse((line.Remove(0, formats[5].Length)));
                }
                else if (line.StartsWith("Dexterity:"))
                {
                    player.dexterity = int.Parse((line.Remove(0, formats[6].Length)));
                }
                else if (line.StartsWith("Constitution:"))
                {
                    player.constitution = int.Parse((line.Remove(0, formats[7].Length)));
                }
                else if (line.StartsWith("Wisdom:"))
                {
                    player.wisdom = int.Parse((line.Remove(0, formats[8].Length)));
                }
                else if (line.StartsWith("Charisma:"))
                {
                    player.charisma = int.Parse((line.Remove(0, formats[9].Length)));
                }
                else if (line.StartsWith("Chakra Affinity:"))
                {
                    player.chakraAffinity = (line.Remove(0, formats[10].Length));
                }
            }
        }
        else
        {
            print("Hmm, seems there was an error?");
            print(playerLocation);
            print(directory);
        }
        return detected;
    }
}
