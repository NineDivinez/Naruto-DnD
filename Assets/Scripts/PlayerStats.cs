using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //Variables
    public string playerName = "";
    public string specialization = "";
    public int specializationLevel;
    public int playerLevel;
    public string hitDice = "";
    public string[] chakraNatures = { };
    public string chakraAffinity = "";
    public int[] chakraLevels = { 0, 0, 0, 0, 0 };
    public int exp;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int constitution;
    public int wisdom;
    public int charisma;

    public override bool Equals(object obj)
    {
        return obj is PlayerStats stats &&
               base.Equals(obj) &&
               EqualityComparer<int[]>.Default.Equals(chakraLevels, stats.chakraLevels);
    }
}
