using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit() { }
    public override void OnBow() { }
    public override void OnSoftRepair() { }
    public override void OnHardRepair() { }
    public override void OnBomb()
    {
        Destroy(gameObject);
    }
}
