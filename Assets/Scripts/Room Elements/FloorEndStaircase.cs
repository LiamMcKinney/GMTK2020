using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorEndStaircase : MonoBehaviour
{
    public string nextFloor;

    public List<RoomEndButton> rooms;
    public float floorCompletion;
    public WinScreen winScreen;
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
        if(collision.GetComponent<PlayerBehavior>() != null)
        {
            float totalRoomCompletion = 0;
            foreach(RoomEndButton room in rooms)
            {
                totalRoomCompletion += room.percentageComplete;
            }
            floorCompletion = totalRoomCompletion / rooms.Count;
            winScreen.InputScore(floorCompletion);
            SceneManager.LoadScene(nextFloor);
        }
    }
}
