using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectJob : MonoBehaviour
{
    public static SelectJob instance = null;

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
    public PlayerStats player = new PlayerStats();
    public Dropdown jobSelector;
    public GameObject[] descriptions;
    public GameObject jobObjectContainer;
    public GameObject chakraNaturContainer;
    public Text errorMessage;

    void Start()
    {
        errorMessage.text = "";
    }

    public void select()
    {
        switch (jobSelector.value +1)
        {
            case 1:
                player.specialization = "Summoner";
                break;
            case 2:
                player.specialization = "Taijutsu";
                break;
            case 3:
                player.specialization = "Medical";
                break;
            case 4:
                player.specialization = "Genjutsu";
                break;
            case 5:
                player.specialization = "Ninjutsu";
                break;
        }
        jobObjectContainer.SetActive(false);
        chakraNaturContainer.SetActive(true);
    }

    public void valueChange()
    {
        int newValue = jobSelector.value;
        for (int i = 0; i <= descriptions.Length -1; i++)
        {
            descriptions[i].SetActive(false);
        }
        descriptions[newValue].SetActive(true);
    }
}
