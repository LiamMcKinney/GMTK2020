using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isRepaired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void OnHit();
    public abstract void OnHardRepair();
    public abstract void OnSoftRepair();
    public abstract void OnBomb();
    public abstract void OnBow();
}
