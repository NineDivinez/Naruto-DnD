using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    //Variables


    //Game Objects
    public PlayerStats player = new PlayerStats();

    public GameObject newFeatScreen;
    public GameObject newNatureScreen;
    public GameObject newJutsuScreen;

    void newChakraNature()
    {

    }

    /// <summary>
    /// A whole new feeeatttttt!
    /// </summary>
    void newFeat()
    {
        switch (player.specialization)
        {
            case "Summoner":
                //Feats available
                //

                break;
            case "Taijutsu":
                //Feats available

                break;
            case "Medic":
                //Feats available

                break;
            case "Genjutsu":
                //Feats available

                break;

            case "Ninjutsu":
                //Feats available

                break;

            default:
                print("Test failed");
                break;
        }
    }
    public void newJutsu()
    {
        //just display something stating the player can choose however many new feats they are allowed.
    }
}
