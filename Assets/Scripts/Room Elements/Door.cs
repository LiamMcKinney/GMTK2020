using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomEndButton room;

    SpriteRenderer sprite;
    BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        sprite.enabled = true;
        coll.enabled = true;
    }

    public void Open()
    {
        sprite.enabled = false;
        coll.enabled = false;
    }
}
