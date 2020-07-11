using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : MonoBehaviour
{
    public RoomEndButton centralComputer;

    //if disabled, the room will not trigger when the player enters. This is for completed rooms.
    bool disabled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("something entered");
        if(!disabled && collision.GetComponent<PlayerBehavior>() != null)
        {
            centralComputer.StartRoom();
            collision.GetComponent<PlayerBehavior>().hasHealed = false;
        }
    }

    public void Disable()
    {
        disabled = true;
    }
}
