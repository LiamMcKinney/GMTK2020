using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
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

    public Collider2D HitBox()
    {
        return gameObject.GetComponent<Collider2D>();
    }
}
