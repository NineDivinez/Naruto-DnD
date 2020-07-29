using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChakraNatureSelector : MonoBehaviour
{
    public static ChakraNatureSelector instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Game Objects
    public GameObject[] chakraNature = { };

    public string[] available = { "Fire", "Water", "Air", "Earth", "Lightning" };
    public Dropdown chakraNatureSelection;
    public Text errorMessage;
    public PlayerStats player = new PlayerStats();

    public GameObject chakraNatureContainer;
    public GameObject statsContainer;

    void Start()
    {
        errorMessage.text = "";
    }

    public void selected()
    {
        errorMessage.text = "";
        for (int i = 0; i <= 4; i++)
        {
            if (chakraNature[i].activeInHierarchy)
            {
                chakraNature[i].SetActive(false);
                break;
            }
        }

        chakraNature[chakraNatureSelection.value].SetActive(true);
    }

    public void finalized()
    {
        player.chakraNatures[chakraNatureSelection.value] = available[chakraNatureSelection.value];
        player.chakraAffinity = available[chakraNatureSelection.value];
        for (int i = 0; i <= 4; i++)
        {
            player.chakraLevels[i] = 0;
        }    
        player.chakraLevels[chakraNatureSelection.value] = 1;

        chakraNatureContainer.SetActive(false);
        statsContainer.SetActive(true);
    }
}
