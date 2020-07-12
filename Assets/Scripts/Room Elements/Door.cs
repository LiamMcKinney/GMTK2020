using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomEndButton room;

    SpriteRenderer sprite;
    BoxCollider2D coll;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        animator.SetBool("Open", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        //sprite.enabled = true;
        coll.enabled = true;
        animator.SetBool("Open", false);
    }

    public void Open()
    {
        //sprite.enabled = false;
        coll.enabled = false;
        animator.SetBool("Open", true);
    }
}
