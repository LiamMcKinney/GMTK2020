using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenComputer : Interactable
{
    public bool repaired;
    // Start is called before the first frame update
    void Start()
    {
        repaired = false;
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
        repaired = true;
    }
}
