using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEndButton : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<Interactable> roomElements = new List<Interactable>();

    public List<RoomEntrance> entrances;
    public List<Door> doors;

    public InputUIManager controlManager;

    public Bounds roomBounds;

    Animator animator;

    public float percentageComplete;
    //the ideal camera location/size to overview the entire room.
    public Vector3 bottomLeftCamLocation;
    public Vector3 topRightCamLocation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in enemies)
        {
            if(enemy == null)
            {
                enemies.Remove(enemy);
                break;
            }
        }
    }

    public void AttemptFinishRoom()
    {
        print("attempting close");
        if(enemies.Count == 0)
        {
            CompleteRoom();
        }
        else
        {
            
        }
    }

    void CompleteRoom()
    {
        print("closing");
        int completionCounter = 0;
        int totalItems = roomElements.Count;
        foreach(Interactable i in roomElements)
        {
            if (i.isRepaired)
            {
                completionCounter++;
            }
            Destroy(i);
        }
        if (totalItems != 0)
        {
            percentageComplete = completionCounter / totalItems;
        }
        else
        {
            percentageComplete = 1;
        }

        foreach(Door door in doors)
        {
            door.Open();
        }

        animator.SetBool("Fixed", true);

        controlManager.UnlockControls();
    }

    public void StartRoom(PlayerBehavior player)
    {
        controlManager.LockControls();
        foreach(Enemy enemy in enemies)
        {
            enemy.player = player;
        }
        foreach(RoomEntrance entrance in entrances)
        {
            entrance.Disable();
        }

        foreach(Door door in doors)
        {
            door.Close();
        }
    }
}
