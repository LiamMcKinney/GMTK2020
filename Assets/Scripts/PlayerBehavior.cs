﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public AttackHitbox attackBox;
    public float attackBoxOffsetMultiplier;
    public float speed;
    float movementUp = 0;
    float movementDown = 0;
    float movementRight = 0;
    float movementLeft = 0;
    Vector2 playerFacing;
    int attackCounter;
    int bowCounter;
    int attackCooldown;
    int bombTimer;
    int bombCooldown;
    public GameObject projectile;
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        playerFacing = new Vector2(0, 0);
        if (speed == 0f)
        {
            speed = 0.015f;
        }
        if(attackBoxOffsetMultiplier == 0f)
        {
            attackBoxOffsetMultiplier = 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movementUp = 0;
        movementDown = 0;
        movementRight = 0;
        movementLeft = 0;
        if (GameInputManager.GetKey("Up"))
        {
            movementUp = speed;
        }
        if (GameInputManager.GetKey("Down"))
        {
            movementDown = speed;
        }
        if (GameInputManager.GetKey("Left"))
        {
            movementLeft = speed;
        }
        if (GameInputManager.GetKey("Right"))
        {
            movementRight = speed;
        }
        transform.position = new Vector3(movementRight - movementLeft, movementUp - movementDown, 0) + transform.position;
        if (movementRight - movementLeft != 0 || movementUp - movementDown != 0)
        {
            playerFacing = new Vector2(movementRight - movementLeft, movementUp - movementDown);
        }
        attackBox.Move(new Vector3(playerFacing.x*attackBoxOffsetMultiplier, playerFacing.y*attackBoxOffsetMultiplier, 0) + transform.position);
        if (GameInputManager.GetKeyDown("Attack"))
        {
            if (attackCooldown < 1)
            {
                attackCounter = 5;
                attackCooldown = 5;
            }
        }
        attackCooldown--;
        if (attackCounter > 0)
        {
            List<Collider2D> targets = attackBox.Collisions();
            foreach (Collider2D coll in targets)
            {
                if (coll.GetComponent<Interactable>() != null)
                {
                    coll.GetComponent<Interactable>().OnHit();
                }
            }
        }
        attackCounter--;
        if (GameInputManager.GetKeyDown("Bow"))
        {
            if (bowCounter < 1)
            {
                Projectile shot = Instantiate(projectile).GetComponent<Projectile>();
                if (playerFacing.y > 0)
                {
                    shot.direction = "Up";
                }
                else if (playerFacing.x > 0)
                {
                    shot.direction = "Right";
                }
                else if (playerFacing.y < 0)
                {
                    shot.direction = "Down";
                }
                else if (playerFacing.x < 0)
                {
                    shot.direction = "Left";
                }
                bowCounter = 60;
            }
        }
        bowCounter--;
        if (GameInputManager.GetKeyDown("Bomb"))
        {
            if (bombCooldown < 1)
            {
                Instantiate(bomb);
            }
        }
        bombCooldown--;
        if (GameInputManager.GetKey("SoftRepairTool"))
        {
            List<Collider2D> targets = attackBox.Collisions();
            foreach (Collider2D coll in targets)
            {
                if (coll.GetComponent<Interactable>() != null)
                {
                    coll.GetComponent<Interactable>().OnSoftRepair();
                }
            }

        }
        if (GameInputManager.GetKey("HardRepairTool"))
        {
            List<Collider2D> targets = attackBox.Collisions();
            foreach (Collider2D coll in targets)
            {
                if (coll.GetComponent<Interactable>() != null)
                {
                    coll.GetComponent<Interactable>().OnHardRepair();
                }
            }
        }
    }
}
