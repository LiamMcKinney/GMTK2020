﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputUIManager : MonoBehaviour
{
    //public List<string> controlNames;
    //public List<InputField> controlBoxes;
    public List<ControlSlot> slotPositions;

    public int numControls;
    // Start is called before the first frame update
    void Start()
    {
        //for(int i)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeControl(string controlIndex)
    {

    }

    public void Hi(string hello)
    {
        print(hello);
    }
}
