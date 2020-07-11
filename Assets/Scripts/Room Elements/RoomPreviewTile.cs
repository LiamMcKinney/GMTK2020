using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPreviewTile : MonoBehaviour
{
    public RoomEndButton room;
    bool inUse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehavior player = collision.GetComponent<PlayerBehavior>();

        if(player != null)
        {
            player.LookAtRoom(room);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehavior player = collision.GetComponent<PlayerBehavior>();

        if (player != null)
        {
            player.StopLookingAtRoom();
        }
    }
}
