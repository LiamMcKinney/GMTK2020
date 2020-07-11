using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : Enemy
{
    public float speed;
    //public PlayerBehavior player;
    Collider2D attackBox;
    int attackCounter;
    public GameObject attackHitbox;
    Rigidbody2D rb;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(speed == 0)
        {
            speed = 2f;
        }
        attackCounter = 500;
        attackBox = Instantiate(attackHitbox).GetComponent<Collider2D>();
        if(health == 0)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
        Vector3 target = player.transform.position;
        Vector3 oldPos = transform.position;
        float xMotion = 0;
        float yMotion = 0;
        if (target.x > oldPos.x)
        {
            xMotion = speed;
        }
        else if (target.x < oldPos.x)
        {
            xMotion = -speed;
        }
        if (target.y > oldPos.y)
        {
            yMotion = speed;
        }
        else if (target.y < oldPos.y)
        {
            yMotion = -speed;
        }
        rb.velocity = new Vector2(xMotion, yMotion);
        attackBox.GetComponent<EnemyAttackHitbox>().Move(transform.position);
        attackCounter--;
        if (attackCounter < 1)
        {
            TryAttack();
        }
    }

    public override void OnHit()
    {
        health -= 2;
        CheckHealth();
    }

    public override void OnSoftRepair() { }

    public override void OnHardRepair() { }

    public override void OnBomb()
    {
        health -= 5;
        CheckHealth();
    }

    public override void OnBow()
    {
        health--;
        CheckHealth();
    }
    
    public List<Collider2D> Collisions()
    {
        List<Collider2D> temp = new List<Collider2D>();
        int t = attackBox.OverlapCollider(new ContactFilter2D(), temp);
        return temp;
    }

    void TryAttack() {
        foreach(Collider2D coll in Collisions())
        {
            if (coll.GetComponent<PlayerBehavior>() != null)
            {
                coll.GetComponent<PlayerBehavior>().OnHit();
                attackCounter = 200;
            }
        }
    }

    void CheckHealth()
    {
        if(health < 1)
        {
            Destroy(attackBox.gameObject);
            Destroy(gameObject);
        }
    }
}
