using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEndButton : MonoBehaviour
{
    public List<Interactable> enemies = new List<Interactable>();
    public List<Interactable> roomElements = new List<Interactable>();

    public List<RoomEntrance> entrances;

    public InputUIManager controlManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptRoomClosure()
    {
        print("attempting close");
        if(enemies.Count == 0)
        {
            CloseRoom();
        }
        else
        {
            
        }
    }

    void CloseRoom()
    {
        print("closing");
        foreach(Interactable i in roomElements)
        {
            Destroy(i);
        }

        controlManager.UnlockControls();
    }

    public void StartRoom()
    {
        controlManager.LockControls();
        foreach(RoomEntrance entrance in entrances)
        {
            entrance.Disable();
        }
    }
}
