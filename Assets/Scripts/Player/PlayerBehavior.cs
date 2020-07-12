using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public CameraManager cam;
    public bool isUsingPreview;
    public Vector3 camOffset;

    public AttackHitbox attackBox;
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
    Rigidbody2D rb;
    public float bowKnockback;
    public int invincibilityFrames;
    int iFramesLeft = 0;

    public float initialBombSpeed;
    public float deceleration;
    public Vector2 explosionMomentum;

    public Animator animator;

    public bool hasHealed;

    public int health;

    public List<AudioClip> hurtNoises;
    public List<AudioClip> hitNoises;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFacing = new Vector2(0, 0);
        if (speed == 0f)
        {
            speed = 0.015f;
        }
        hasHealed = false;
    }

    // Update is called once per frame
    void Update()
    {
        iFramesLeft--;
        if(explosionMomentum != Vector2.zero)
        {
            rb.velocity = explosionMomentum;
            cam.targetPosition = transform.position + camOffset;

            explosionMomentum = Vector2.MoveTowards(explosionMomentum, Vector2.zero, deceleration);
            return;
        }
        if (GameInputManager.isConfiguringControls || isUsingPreview)
        {
            return;
        }
        cam.targetPosition = transform.position + camOffset;
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
        rb.velocity = new Vector3(movementRight - movementLeft, movementUp - movementDown, 0);
        if (movementRight - movementLeft != 0 || movementUp - movementDown != 0)
        {
            playerFacing = new Vector2(movementRight - movementLeft, movementUp - movementDown);
            animator.SetBool("Moving", true);
            animator.SetInteger("Direction", GetFacingDirection());
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        switch (GetFacingDirection())
        {
            case 0:
                attackBox.Move(new Vector3(0, 0.8f, 0) + transform.position + new Vector3(0, -0.5f, 0));
                attackBox.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case 1:
                attackBox.Move(new Vector3(0.8f, 0, 0) + transform.position + new Vector3(0, -0.5f, 0));
                attackBox.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case 2:
                attackBox.Move(new Vector3(0, -0.8f, 0) + transform.position + new Vector3(0, -0.5f, 0));
                attackBox.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case 3:
                attackBox.Move(new Vector3(-0.8f, 0, 0) + transform.position + new Vector3(0, -0.5f, 0));
                attackBox.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
        }
        CheckAttack();
        CheckBow();
        CheckBomb();
        CheckTools();
        bombCooldown--;
        bowCounter--;
        attackCooldown--;
        attackCounter--;
        if (GameInputManager.GetKey("Block") && !hasHealed)
        {
            health++;
            hasHealed = true;
        }
    }

    void CheckAttack()
    {
        if (GameInputManager.GetKeyDown("Attack"))
        {
            if (attackCooldown < 1)
            {
                attackCounter = 5;
                attackCooldown = 5;
            }
        }

        if (attackCounter > 0)
        {
            List<Collider2D> targets = attackBox.Collisions();
            foreach (Collider2D coll in targets)
            {
                if (coll.GetComponent<Interactable>() != null)
                {
                    coll.GetComponent<Interactable>().OnHit();
                    cam.PlayClip(hitNoises[0]);
                }
            }
        }
    }

    void CheckBow()
    {
        if (GameInputManager.GetKeyDown("Bow"))
        {
            if (bowCounter < 1)
            {
                Projectile shot = Instantiate(projectile).GetComponent<Projectile>();
                shot.Move(transform.position);
                if (playerFacing.y > 0)
                {
                    shot.direction = "Up";
                    transform.position = transform.position + new Vector3(0, -bowKnockback, 0);
                }
                else if (playerFacing.x > 0)
                {
                    shot.direction = "Right";
                    transform.position = transform.position + new Vector3(-bowKnockback, 0, 0);
                }
                else if (playerFacing.y < 0)
                {
                    shot.direction = "Down";
                    transform.position = transform.position + new Vector3(0, bowKnockback, 0);
                }
                else if (playerFacing.x < 0)
                {
                    shot.direction = "Left";
                    transform.position = transform.position + new Vector3(bowKnockback, 0, 0);
                }
                bowCounter = 60;
            }
        }
    }

    void CheckBomb()
    {
        if (GameInputManager.GetKeyDown("Bomb"))
        {
            if (bombCooldown < 1)
            {
                Bomb boom = Instantiate(bomb).GetComponent<Bomb>();
                Vector3 offset = Vector3.zero;
                switch (GetFacingDirection())
                {
                    case 0:
                        offset = Vector3.up;
                        break;
                    case 1:
                        offset = Vector3.right;
                        break;
                    case 2:
                        offset = Vector3.down;
                        break;
                    case 3:
                        offset = Vector3.left;
                        break;
                }
                boom.Move(offset + transform.position + new Vector3(0, -0.5f, 0));
                bombCooldown = 100;
            }
        }
    }
    void CheckTools()
    {
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

    public void OnHit()
    {
        if (iFramesLeft <= 0)
        {
            iFramesLeft = invincibilityFrames;
            health--;
            cam.Shake();
            cam.PlayClip(hurtNoises[0]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision entered");
        if(collision.collider.GetComponent<RoomEndButton>() != null)
        {
            print("computer found");
            collision.collider.GetComponent<RoomEndButton>().AttemptFinishRoom();
        }
    }

    public void LookAtRoom(RoomEndButton room)
    {
        isUsingPreview = true;
        rb.velocity = Vector2.zero;
        cam.ZoomToTarget(room.bottomLeftCamLocation);
    }

    public void StopLookingAtRoom()
    {
        isUsingPreview = false;
        cam.ZoomToTarget(transform.position);
    }

    int GetFacingDirection()
    {
        if(playerFacing.y > Mathf.Abs(playerFacing.x))
        {
            return 0;
        }else if(playerFacing.y < -Mathf.Abs(playerFacing.x))
        {
            return 2;
        }else if(playerFacing.x >= Mathf.Abs(playerFacing.y)){
            return 1;
        }
        else
        {
            return 3;
        }
    }

    public void BlowUp(Vector3 source)
    {
        print("boom");
        explosionMomentum = (transform.position - source).normalized * initialBombSpeed;
    }
}
