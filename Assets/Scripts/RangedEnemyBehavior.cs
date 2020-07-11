using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehavior : Interactable
{
    public float speed;
    public PlayerBehavior player;
    int shootTimer;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0)
        {
            speed = .01f;
        }
        shootTimer = 500;
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
            if(xMotion * xMotion > yMotion * yMotion)
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
            else
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
            shootTimer = 300;
        }
    }

    public override void OnHit()
    {
        Destroy(gameObject);
    }

    public override void OnSoftRepair() { }

    public override void OnHardRepair() { }

    public override void OnBomb()
    {
        Destroy(gameObject);
    }

    public override void OnBow()
    {
        Destroy(gameObject);
    }
}
