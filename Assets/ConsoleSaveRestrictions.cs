using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleSaveRestrictions : MonoBehaviour
{
    public GameObject specialization;
    public GameObject chakraNature;
    public GameObject stats;
    public GameObject restricted;
    void Update()
    {
        if (specialization.activeInHierarchy)
            restricted.SetActive(true);
        else if (chakraNature.activeInHierarchy)
            restricted.SetActive(true);
        else if (stats.activeInHierarchy)
            restricted.SetActive(true);
        else
            restricted.SetActive(false);
    }
}
