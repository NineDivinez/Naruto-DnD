using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStats : MonoBehaviour
{
    public PlayerStats player = new PlayerStats();
    
    public void removeLoaded()
    {
        player.playerName = ""; player.specialization = ""; player.exp = 0; player.chakraAffinity = "";
        player.strength = 0; player.intelligence = 0; player.dexterity = 0; player.wisdom = 0; player.charisma = 0;
        RollForStats.instance.rollsRemaining = 3; RollForStats.instance.rolled = false;

        for (int i = 0; i < 5; i++)
        {
            player.chakraLevels[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            player.chakraNatures[i] = "";
        }
    }
}
