using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextBox : MonoBehaviour
{
    public static TutorialTextBox instance;
    public Text tutorialText;
    int currentPriority;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text, int priority)
    {
        if (priority > currentPriority)
        {
            gameObject.SetActive(true);
            tutorialText.text = text;
            currentPriority = priority;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentPriority = 0;
    }
}
