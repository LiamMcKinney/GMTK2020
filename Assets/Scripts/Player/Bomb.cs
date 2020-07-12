using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int detonationTimer;
    public BoxCollider2D blastZone;
    public Vector2 blastSize;
    public Sprite explosionSprite;
    // Start is called before the first frame update
    void Start()
    {
        detonationTimer = 400;
    }

    // Update is called once per frame
    void Update()
    {
        detonationTimer--;
        if (detonationTimer == 1)
        {
            GetComponent<SpriteRenderer>().sprite = explosionSprite;
            blastZone.isTrigger = true;
            blastZone.size = blastSize;
        }else if(detonationTimer <= 0) { 
            List<Collider2D> temp = new List<Collider2D>();
            int t = blastZone.OverlapCollider(new ContactFilter2D(), temp);
            foreach(Collider2D coll in temp)
            {
                if(coll.GetComponent<Interactable>() != null)
                {
                    coll.GetComponent<Interactable>().OnBomb();
                }else if(coll.GetComponent<PlayerBehavior>() != null)
                {
                    coll.GetComponent<PlayerBehavior>().BlowUp(transform.position);
                }

            }
            Destroy(gameObject);
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}
