using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPreviewTile : MonoBehaviour
{
    public RoomEndButton room;
    bool inUse;
    PlayerBehavior player;
    CameraManager camera;
    Vector3 camLocation;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        camLocation = (room.bottomLeftCamLocation + room.topRightCamLocation) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            camLocation += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * speed;
            camLocation.x = Mathf.Clamp(camLocation.x, room.bottomLeftCamLocation.x, room.topRightCamLocation.x);
            camLocation.y = Mathf.Clamp(camLocation.y, room.bottomLeftCamLocation.y, room.topRightCamLocation.y);

            camera.targetPosition = camLocation;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                inUse = false;
                player.isUsingPreview = false;
                player.StopLookingAtRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
        
        if(player != null)
        {
            player.transform.position = transform.position;
            inUse = true;
            player.LookAtRoom(room);
            player.isUsingPreview = true;
            this.player = player;
            camera = player.cam;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
        if (player != null)
        {
            inUse = false;
            player.StopLookingAtRoom();
        }
    }
}
