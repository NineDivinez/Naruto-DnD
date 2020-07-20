using UnityEngine;

public class OpenPDF : MonoBehaviour
{
    public void openPDF()
    {
        Application.OpenURL(System.Environment.CurrentDirectory + "/PlayerHandbook.pdf");
    }
}
