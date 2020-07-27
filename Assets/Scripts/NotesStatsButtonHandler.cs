using UnityEngine;
using UnityEngine.UI;

public class NotesStatsButtonHandler : MonoBehaviour
{
    public Button notesButton;
    public Button statsButton;

    private void Start()
    {
        notesButton.interactable = (false);
    }

    public void notesButtonPress()
    {
        notesButton.interactable = (false);
        statsButton.interactable = (true);
    }

    public void statsButtonPress()
    {
        notesButton.interactable = (true);
        statsButton.interactable = (false);
    }
}
