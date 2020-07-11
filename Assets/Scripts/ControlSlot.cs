using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSlot : MonoBehaviour
{
    public string actionName;
    public Keybind button;

    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
