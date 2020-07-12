using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public float tutorialCompletion;
    public float floor1Completion;
    public float floor2Completion;
    int levelCounter;

    private static WinScreen instance;
    public FloorEndStaircase stairs;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        levelCounter = 0;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            stairs.winScreen = instance;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputScore(float score)
    {
        switch (levelCounter)
        {
            case 0:
                tutorialCompletion = score * 100;
                break;
            case 1:
                floor1Completion = score * 100;
                break;
            case 2:
                floor2Completion = score * 100;
                break;
        }
        levelCounter++;
    }
}
