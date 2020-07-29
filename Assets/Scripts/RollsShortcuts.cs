using UnityEngine;

public class RollsShortcuts : MonoBehaviour
{
    public AudioSource btnSFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            btnSFX.Play();
            RollForStats.instance.rollDice();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            btnSFX.Play();
            RollForStats.instance.saveRolls();
        }
    }
}
