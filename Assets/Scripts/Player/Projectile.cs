using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string direction;
    public float speed;
    Collider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
       // direction = "Up";
        speed = 0.03f;
        hitbox = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case "Up":
                transform.position = new Vector3(0, speed, 0) + transform.position;
                break;
            case "Down":
                transform.position = new Vector3(0, -speed, 0) + transform.position;
                break;
            case "Right":
                transform.position = new Vector3(speed, 0, 0) + transform.position;
                break;
            case "Left":
                transform.position = new Vector3(-speed, 0, 0) + transform.position;
                break;
        }
        List<Collider2D> temp = new List<Collider2D>();
        int t = hitbox.OverlapCollider(new ContactFilter2D(), temp);
        foreach(Collider2D coll in temp)
        {
            if(coll.GetComponent<Interactable>() != null)
            {
                coll.GetComponent<Interactable>().OnBow();
                Destroy(gameObject);
                break;
            }
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}
