using System.Collections;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    //Variables


    //Game Objects
    public PlayerStats player = new PlayerStats();


    void Start ()
    {
        switch (player.specialization)
        {
            case "Summoner":
                print("Test worked");
                break;
            case "Taijutsu":
                print("Test worked");
                break;
            case "Medic":
                print("Test worked");
                break;
            case "Genjutsu":
                print("Test worked");
                break;
            case "Ninjutsu":
                print("Test worked");
                break;
            default:
                print("Test failed");
                break;
        }
    }
}
