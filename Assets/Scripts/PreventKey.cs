using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class PreventKey : MonoBehaviour
{
    //Game Objects
    public InputField[] fields;

    void OnGUI()
    {
        for (int i = 0; i < fields.Length; i++)
            fields[i].text = Regex.Replace(fields[i].text, @"[^a-zA-Z0-9, ]", "");
    }
}
