using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArea : MonoBehaviour
{
    public string message;
    public int priority;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBehavior>() != null)
        {
            TutorialTextBox.instance.ShowText(message, priority);
        }
    }

    string GetMessage()
    {
        return message;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBehavior>() != null)
        {
            TutorialTextBox.instance.Hide();
        }
    }
}
