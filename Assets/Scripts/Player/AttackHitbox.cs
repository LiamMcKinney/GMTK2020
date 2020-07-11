using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }

    public List<Collider2D> Collisions()
    {
        List<Collider2D> temp = new List<Collider2D>();
        int t = gameObject.GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), temp);
        return temp;
    }
}
