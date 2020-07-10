using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit()
    {
        Destroy(gameObject);
    }
    
    public override void OnSoftRepair()
    {

    }

    public override void OnHardRepair()
    {

    }

    public override void OnBomb()
    {

    }

    public override void OnBow()
    {

    }
}
