using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyBehavior : Enemy
{
    public float speed;
    //public PlayerBehavior player;
    int attackCounter;
    Rigidbody2D rb;
    public int health;
    Animator animator;

    public int invincibilityFrames;
    int iFramesLeft = 0;

    public float knockbackSpeed;
    public float deceleration;
    public Vector2 knockbackMomentum;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(speed == 0)
        {
            speed = 2f;
        }
        if(health == 0)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        iFramesLeft--;

        if(player == null)
        {
            return;
        }
        if(knockbackMomentum != Vector2.zero)
        {
            rb.velocity = knockbackMomentum;

            knockbackMomentum = Vector2.MoveTowards(knockbackMomentum, Vector2.zero, deceleration);
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
        TryAttack();
    }

    public override void OnHit()
    {
        if (iFramesLeft <= 0)
        {
            iFramesLeft = invincibilityFrames;
            health -= 2;
            knockbackMomentum = (transform.position - player.transform.position).normalized * knockbackSpeed;
            CheckHealth();
        }
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
        int t = GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), temp);
        return temp;
    }

    void TryAttack() {
        foreach(Collider2D coll in Collisions())
        {
            if (coll.GetComponent<PlayerBehavior>() != null)
            {
                animator.SetTrigger("Attack");
                coll.GetComponent<PlayerBehavior>().OnHit();
                attackCounter = 200;
            }
        }
    }

    void CheckHealth()
    {
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}
