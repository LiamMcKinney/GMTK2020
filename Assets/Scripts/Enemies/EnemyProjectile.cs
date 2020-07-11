using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Collider2D hitbox;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction;
        List<Collider2D> temp = new List<Collider2D>();
        int t = hitbox.OverlapCollider(new ContactFilter2D(), temp);
        foreach (Collider2D coll in temp)
        {
            if (coll.GetComponent<PlayerBehavior>() != null)
            {
                coll.GetComponent<PlayerBehavior>().OnHit();
                Destroy(gameObject);
                break;
            }
            if (coll.GetComponent<Interactable>() != null)
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
