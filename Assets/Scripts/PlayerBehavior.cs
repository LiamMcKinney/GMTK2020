using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    float movementUp = 0;
    float movementDown = 0;
    float movementRight = 0;
    float movementLeft = 0;
    Vector2 playerFacing;
    // Start is called before the first frame update
    void Start()
    {
        playerFacing = new Vector2(0, 0);
        if (speed == 0f)
        {
            speed = 0.015f;
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
        playerFacing = new Vector2(movementRight - movementLeft, movementUp - movementDown);
    }
}
