﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenComputer : Interactable
{
    public SpriteRenderer icon;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isRepaired = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit() { }
    public override void OnBow() { }
    public override void OnBomb() { }
    public override void OnHardRepair() { }

    public override void OnSoftRepair()
    {
        isRepaired = true;
        animator.SetTrigger("Fixed");
        icon.enabled = false;
    }
}
