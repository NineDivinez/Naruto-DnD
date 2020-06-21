using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RollForStats : MonoBehaviour
{
    //Variables
    public List<int> rolls = new List<int>();
    public List<int> used = new List<int>();
    public bool rolled = false;
    public int rollsRemaining = 3;

    //Game Objects
    public PlayerStats player = new PlayerStats();
    public Text print;
    public Text errorMessage;
    public Text remaining;

    public Dropdown str;
    public Dropdown intel;
    public Dropdown dex;
    public Dropdown con;
    public Dropdown wis;
    public Dropdown charis;

    public GameObject stats;
    public GameObject loadScreen;

    void Start()
    {
        str.options.Add(new Dropdown.OptionData() { text = "Choose roll" });
        intel.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        dex.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        con.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        wis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        charis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;

        str.value = 0;
        intel.value = 0;
        dex.value = 0;
        con.value = 0;
        wis.value = 0;
        charis.value = 0;

        for (int i = 0; i <= 5; i++)
        {
            used.Add(0);
        }
        print.text ="Rolls\n" +
"First roll:        " + "\n" +
"Second roll:       " + "\n" +
"Third roll:        " + "\n" +
"Fourth roll:       " + "\n" +
"Fifth roll:        " + "\n" +
"Sixth roll:        ";
        errorMessage.text = "";
    }

    public void rollDice()
    {
        errorMessage.text = "";
        if (!rolled)
        {
            if (rollsRemaining > 0)
            {
                if (rollsRemaining >= 3)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        int newRoll = Random.Range(4, 18);
                        rolls.Add(newRoll);
                    }
                    print.text = "Rolls\n" +
            "First roll:        " + rolls[0] + "\n" +
            "Second roll:       " + rolls[1] + "\n" +
            "Third roll:        " + rolls[2] + "\n" +
            "Fourth roll:       " + rolls[3] + "\n" +
            "Fifth roll:        " + rolls[4] + "\n" +
            "Sixth roll:        " + rolls[5];

                    for (int i = 0; i <= 5; i++)
                    {
                        str.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() });
                        intel.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        dex.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        con.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        wis.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        charis.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                    }
                }
                else if (rollsRemaining < 3)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        int newRoll = Random.Range(4, 18);
                        rolls[i] = newRoll;
                    }

                    str.options.Clear();
                    intel.options.Clear();
                    dex.options.Clear();
                    con.options.Clear();
                    wis.options.Clear();
                    charis.options.Clear();

                    str.options.Add(new Dropdown.OptionData() { text = "Choose roll" });
                    intel.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
                    dex.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
                    con.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
                    wis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
                    charis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;

                    for (int i = 0; i <= 5; i++)
                    {
                        print("Updating Dice Rolls");
                        str.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() });
                        intel.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        dex.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        con.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        wis.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() }); ;
                        charis.options.Add(new Dropdown.OptionData() { text = rolls[i].ToString() });
                    }

                    str.value = 0;
                    intel.value = 0;
                    dex.value = 0;
                    con.value = 0;
                    wis.value = 0;
                    charis.value = 0;

                    print.text = "Rolls\n" +
            "First roll:        " + rolls[0] + "\n" +
            "Second roll:       " + rolls[1] + "\n" +
            "Third roll:        " + rolls[2] + "\n" +
            "Fourth roll:       " + rolls[3] + "\n" +
            "Fifth roll:        " + rolls[4] + "\n" +
            "Sixth roll:        " + rolls[5];
                }
                
                rollsRemaining -= 1;
                remaining.text = "Rerolls remaining: " + rollsRemaining.ToString();
                if (rollsRemaining <= 0)
                {
                    rolled = true;
                }
            }
        }
        else
        {
            errorMessage.text = "You have already performed all your rolls!";
        }
    }

    public void usedRoll()
    {
        errorMessage.text = "";
        int[] entries = { 0, 0, 0, 0, 0, 0};
        entries[0] = str.value;
        entries[1] = intel.value;
        entries[2] = dex.value;
        entries[3] = con.value;
        entries[4] = wis.value;
        entries[5] = charis.value;
        for (int i = 0; i <= 5; i++)
        {
            for (int x = 0; x <= 5; x++)
            {
                if (entries[x] == entries[i])
                {
                    if (x != i)
                    {
                        if (entries[x] != 0 && entries[i] != 0)
                        {

                            errorMessage.text = "You cannot have two skills with the same roll!";
                        }
                    }
                }
            }
        }
    }

    bool theSame(int entryOne, int entryTwo)
    {
        if (rolls[entryOne] != rolls[entryTwo])
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void saveRolls()
    {
        if (str.options[str.value].text != "Choose roll")
        {
            if (intel.options[intel.value].text != "Choose roll")
            {
                if (dex.options[dex.value].text != "Choose roll")
                {
                    if (con.options[con.value].text != "Choose roll")
                    {
                        if (wis.options[wis.value].text != "Choose roll")
                        {
                            if (charis.options[charis.value].text != "Choose roll")
                            {
                                if (errorMessage.text == "")
                                {
                                    player.strength = int.Parse(str.options[str.value].text);
                                    player.intelligence = int.Parse(intel.options[intel.value].text);
                                    player.dexterity = int.Parse(dex.options[dex.value].text);
                                    player.constitution = int.Parse(con.options[con.value].text);
                                    player.wisdom = int.Parse(wis.options[wis.value].text);
                                    player.charisma = int.Parse(charis.options[charis.value].text);
                                    stats.SetActive(false);
                                    loadScreen.SetActive(true);
                                }
                                else
                                {
                                    errorMessage.text = "You cannot proceed, either two rolls are set as the same or you have not rolled!";
                                }
                            }
                            else
                                invalidInput();
                        }
                        else
                            invalidInput();
                    }
                    else
                        invalidInput();
                }
                else
                    invalidInput();
            }
            else
                invalidInput();
        }
        else
            invalidInput();
    }

    void invalidInput()
    {
        errorMessage.text = "Invalid selection";
    }

    public void cancel()
    {
        rollsRemaining = 3;
        remaining.text = "Rolls Remaining: " + rollsRemaining.ToString();

        print.text = "Rolls\n" +
"First roll:        " + "\n" +
"Second roll:       " + "\n" +
"Third roll:        " + "\n" +
"Fourth roll:       " + "\n" +
"Fifth roll:        " + "\n" +
"Sixth roll:        ";

        str.options.Clear();
        intel.options.Clear();
        dex.options.Clear();
        con.options.Clear();
        wis.options.Clear();
        charis.options.Clear();

        str.options.Add(new Dropdown.OptionData() { text = "Choose roll" });
        intel.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        dex.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        con.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        wis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
        charis.options.Add(new Dropdown.OptionData() { text = "Choose roll" }); ;
    }
}
