using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenText : MonoBehaviour
{
    public WinScreen winScreen;
    public bool floor2Exists;
    public Text text;
    public bool endlessExists;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Congratulations, you win!\n Percentage of Tutorial Machines Repaired: " + winScreen.tutorialCompletion + "\n Percentage of 1st Floor Machines Repaired: " + winScreen.floor1Completion;
        if (floor2Exists)
        {
            text.text += "\n Percentage of 2nd Floor Machines Repaired: " + winScreen.floor2Completion;
        }
        if (endlessExists)
        {
            text.text += "\nConsider Trying Endless Mode";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
