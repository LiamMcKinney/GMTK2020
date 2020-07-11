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

    //the ideal camera location/size to overview the entire room.
    public Vector3 bottomLeftCamLocation;
    public Vector3 topRightCamLocation;

    // Start is called before the first frame update
    void Start()
    {
        
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
        foreach(Interactable i in roomElements)
        {
            Destroy(i);
        }

        foreach(Door door in doors)
        {
            door.Open();
        }

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
