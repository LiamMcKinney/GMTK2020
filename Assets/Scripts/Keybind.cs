using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybind : MonoBehaviour
{
    BoxCollider2D coll;
    bool dragging;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
    }

    private void OnMouseDown()
    {
        if (coll.OverlapPoint(Input.mousePosition))
        {
            dragging = true;
        }
    }

    private void OnMouseUp()
    {
        
    }
}
