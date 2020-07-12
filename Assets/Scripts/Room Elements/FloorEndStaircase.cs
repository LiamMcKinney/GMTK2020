﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorEndStaircase : MonoBehaviour
{
    public Scene nextFloor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerBehavior>() != null)
        {
            SceneManager.LoadScene(nextFloor.name);
        }
    }
}