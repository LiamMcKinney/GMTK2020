using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : Enemy
{
    public float speed;
    //public PlayerBehavior player;
    int shootTimer;
    public GameObject projectile;
    public int health;
    Rigidbody2D rb;

    public float knockbackSpeed;
    public float deceleration;
    public Vector2 knockbackMomentum;

    public Animator animator;

    public int invincibilityFrames;
    int iFramesLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (speed == 0)
        {
            speed = .01f;
        }
        shootTimer = 500;
        if(health == 0)
        {
            health = 3;
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
        if (knockbackMomentum != Vector2.zero)
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
            xMotion = -speed;
        }
        else if (target.x < oldPos.x)
        {
            xMotion = speed;
        }
        if (target.y > oldPos.y)
        {
            yMotion = -speed;
        }
        else if (target.y < oldPos.y)
        {
            yMotion = speed;
        }
        rb.velocity = new Vector3(xMotion, yMotion, 0);
        shootTimer--;
        if (shootTimer < 1)
        {
            animator.SetTrigger("Shoot");
            EnemyProjectile shot = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
            shot.creator = this;
            //shot.Move(transform.position - new Vector3(xMotion*20, yMotion*20, 0));
            shot.direction = (target - transform.position).normalized * .03f;
            shootTimer = 300;
        }
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
        health -= 3;
        CheckHealth();
    }

    public override void OnBow()
    {
        health--;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}
