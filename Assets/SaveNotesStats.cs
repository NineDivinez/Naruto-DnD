using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveNotesStats : MonoBehaviour
{
    public InputField notes;
    public InputField age;
    public InputField hairColor;
    public InputField eyeColor;
    public InputField height;
    public InputField weight;
    public InputField village;
    public Text main;

    int hitDie;
    int numberOfHitDie;
    int movementSpeed;
    int hitpointDie;

    int strMod;
    int intMod;
    int dexMod;
    int wisMod;
    int conMod;
    int charMod;

    public string[] format = 
        { 
            "Hit dice (Unarmed):                             ",
            "Movement Speed:                                    ",
            "Hitpoint Die:                                           ",
            "Strength modifier:                          ",
            "Int modifier:                                          ",
            "Dexterity modifier:                        ",
            "Willpower modifier:                  ",
            "Constitution modifier:            ",
            "Charisma modifier:                         " ,
            "Age:                              " ,
            "Hair Color:                       " ,
            "Eye Color:                        " ,
            "Height:                           " ,
            "Weight:                           " ,
            "Village:                          " };

    public PlayerStats player = new PlayerStats();

    public void specialistSpecific()
    {
        switch (player.specialization.ToLower())
        {
            case "taijutsu": //Tough hero

                hitpointDie = 10;
                movementSpeed = 30;
                hitDie = 4;
                numberOfHitDie = 1;

                break;

            case "summoner": //charismatic hero

                hitpointDie = 6;
                movementSpeed = 30;
                hitDie = 3;
                numberOfHitDie = 1;

                break;

            case "medic": //dedicated hero

                hitpointDie = 6;
                movementSpeed = 40;
                hitDie = 3;
                numberOfHitDie = 1;

                break;

            case "genjutsu": //fast hero

                hitpointDie = 8;
                movementSpeed = 30;
                hitDie = 3;
                numberOfHitDie = 1;

                break;

            case "ninjutsu": //strong hero

                hitpointDie = 8;
                movementSpeed = 30;
                hitDie = 3;
                numberOfHitDie = 1;

                break;
        }
    }

    public void mods()
    {
        strMod = calc(player.strength);
        intMod = calc(player.intelligence);
        dexMod = calc(player.dexterity);
        wisMod = calc(player.wisdom);
        conMod = calc(player.constitution);
        charMod = calc(player.charisma);
    }
    public int calc(int stat)
    {
        int modifier = (stat - 10)/2;
        return modifier;
    }

    void LateUpdate()
    {
        mods();
        specialistSpecific();
        displayAll();
    }

    public void displayAll()
    {
        string specData = 
            $"{format[0] + numberOfHitDie}D{hitDie}\n" +
            $"{format[1] + movementSpeed}\n" +
            $"{format[2] + hitpointDie}\n\n" +
            $"{format[3] + strMod}\n" +
            $"{format[4] + intMod}\n" +
            $"{format[5] + dexMod}\n" +
            $"{format[6] + wisMod}\n" +
            $"{format[7] + conMod}\n" +
            $"{format[8] + charMod}";
        main.text = specData; //Displays the top part of the text.

        string uniqueTraitsLocation = player.playerName + ".traits";
        string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
        fileCheck(Path.Combine(saveLocation, uniqueTraitsLocation));
        

        string line;

        System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(saveLocation, uniqueTraitsLocation));
        while ((line = file.ReadLine()) != null)
        {
            if (line.StartsWith("Age:"))
            {
                age.text = (line.Remove(0, format[9].Length));
            }
            else if (line.StartsWith("Hair Color:"))
            {
                hairColor.text = (line.Remove(0, format[10].Length));
            }
            else if (line.StartsWith("Eye Color:"))
            {
                eyeColor.text = (line.Remove(0, format[11].Length));
            }
            else if (line.StartsWith("Height:"))
            {
                height.text = (line.Remove(0, format[12].Length));
            }
            else if (line.StartsWith("Weight:"))
            {
                weight.text = (line.Remove(0, format[13].Length));
            }
            else if (line.StartsWith("Village:"))
            {
                village.text = (line.Remove(0, format[14].Length));
            }
        }

        string notesSave = player.playerName + ".notes";
        fileCheck(Path.Combine(saveLocation, notesSave));

        System.IO.StreamReader noteReader = new System.IO.StreamReader(Path.Combine(saveLocation, notesSave));
        string notesLines = "";
        while((line = noteReader.ReadLine()) != null)
        {
            notesLines += line + "\n";
        }
        notes.text = notesLines;
    }    

    public void saveAll()
    {
        string notesSave = player.playerName + ".notes";
        string saveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Divinity10/NarutoDnD/Game Saves");
        string[] notesData = { notes.text };
        System.IO.File.WriteAllLines(Path.Combine(saveLocation, notesSave), notesData);

        string uniqueTraitsLocation = player.playerName + ".traits";
        string[] traitSave = { format[9] + age.text, format[10] + hairColor.text, format[11] + eyeColor.text, format[12] + height.text, format[13] + weight.text, format[14] + village.text };
        System.IO.File.WriteAllLines(Path.Combine(saveLocation, uniqueTraitsLocation), traitSave);
    }

    public void fileCheck(string file)
    {
        if (!File.Exists(file))
        {
            System.IO.File.WriteAllLines(file, format);
        }
    }
}
