using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class displayPlayer : MonoBehaviour
{
    //Variables

    //GameObjects
    public PlayerStats player = new PlayerStats();
    public Text display;
    public Text errorMessage;

    public gainExp expCheck = new gainExp();

    void Start()
    {
        errorMessage.text = "";
    }

    void Update()
    {
        display.text = "Name:              " + player.playerName + "\n" +
                        "Exp:              " + player.exp + "\n" +
                        "Level:            " + player.playerLevel + "\n" +
                        "Specialization:   " + player.specialization + "\n" +
                        "Strength:         " + player.strength + "\n" +
                        "Intelligence:     " + player.intelligence + "\n" +
                        "Dexterity:        " + player.dexterity + "\n" +
                        "Constitution:     " + player.constitution + "\n" +
                        "Wisdom:           " + player.wisdom + "\n" +
                        "Charisma:         " + player.charisma + "\n" + 
                        "Chakra Affinity:  " + player.chakraAffinity + "\n" +
                        "Chakra Natures: "
            ;
        for (int i = 0; i <= 4; i++)
        {
            if (player.chakraNatures[i] != "")
            {
                if (i < 3)
                    display.text += "\n" + player.chakraNatures[i] + " Level: " + player.chakraLevels[i] + " ";
                else
                    display.text += "\n" + player.chakraNatures[i] + " Level: " + player.chakraLevels[i];
            }
        }
            
    }
}
