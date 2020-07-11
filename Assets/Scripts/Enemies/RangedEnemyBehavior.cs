using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : Interactable
{
    public float speed;
    public PlayerBehavior player;
    int shootTimer;
    public GameObject projectile;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
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
        transform.position = new Vector3(xMotion, yMotion, 0) + oldPos;
        shootTimer--;
        if (shootTimer < 1)
        {
            EnemyProjectile shot = Instantiate(projectile).GetComponent<EnemyProjectile>();
            shot.Move(transform.position - new Vector3(xMotion*20, yMotion*20, 0));
            float xDist = transform.position.x - target.x;
            float yDist = transform.position.y - target.y;
            float distRatio = yDist / xDist;
            if(distRatio >= 1 || distRatio <= -1)
            {
                if (yMotion > 0)
                {
                    shot.direction = "Down";
                }
                else
                {
                    shot.direction = "Up";
                }
            }
            else
            {
                if (xMotion > 0)
                {
                    shot.direction = "Left";
                }
                else
                {
                    shot.direction = "Right";
                }
            }
            shootTimer = 300;
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
