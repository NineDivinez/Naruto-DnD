using UnityEngine;
using UnityEngine.UI;

public class TabFocus : MonoBehaviour
{
    public InputField[] fields;
    int currentField = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < fields.Length; i++)
            {
                currentField = i;
                if (i +1 > 5)
                    currentField = 0;
                if (fields[currentField].isFocused)
                {
                    fields[currentField + 1].ActivateInputField();
                    break;
                }
                else if (currentField == 0 && i == 5)
                {
                    if (fields[i].isFocused)
                    {
                        fields[currentField].ActivateInputField();
                        break;
                    }
                }
            }
        }
    }
}
