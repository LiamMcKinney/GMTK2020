using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FloorGenerator : MonoBehaviour
{
    public float dungeonSize;
    public float sizeIncreasePerFloor;
    //public Vector2 roomSize;//dimensions of a room
    Dictionary<Vector2, Room> layout;

    public Tilemap floorGrid;
    public Tilemap roomGrid;
    public Tile wallTile;
    public Tile groundTile;

    public List<GameObject> roomPrefabs;

    public GameObject horizHallway;
    public GameObject verticHallway;

    public GameObject previewTilePrefab;
    public GameObject doorPrefab;
    public GameObject entrancePrefab;

    public Tile floorTile;

    //values to control how many enemies spawn in each room.
    public float initialDifficulty;
    public float difficultyScaling;
    public float difficultyIncreasePerFloor;

    Vector2 lastRoom;//coordinates of the last room generated

    public CameraManager cam;

    public PlayerBehavior player;

    public Canvas ui;
    public GameObject gameOverPanel;
    public Slider hpBar;
    public Text floorText;
    public Text goldText;
    public Text totalGoldText;
    int floorNumber = 0;

    // Use this for initialization
    void Start()
    {

        GenerateMap();
    }

    void GenerateMap()
    {
        floorNumber++;
        floorText.text = "Floor " + floorNumber;

        dungeonSize += sizeIncreasePerFloor;

        initialDifficulty += difficultyIncreasePerFloor;

        layout = new Dictionary<Vector2, Room>();

        GenerateDungeonPlan();

        GenerateDungeon();
    }

    void GenerateDungeonPlan()
    {
        //generate first room
        GameObject firstRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
        RoomEndButton server = firstRoom.GetComponentInChildren<RoomEndButton>();
        layout.Add(Vector2.zero, new Room(new Bounds(Vector2.zero, server.roomBounds.size)));
        //layout.Add(Vector2.zero, new Room(Vector2.zero));

        //generate and initialize player in the middle of the room
        //player = Instantiate(playerPrefab, new Vector2((int)(roomSize.x / 2) + .5f, (int)(roomSize.y / 2) + .5f), Quaternion.identity).GetComponent<PlayerBehavior>();
        //player.transform.position = new Vector2((int)(roomSize.x / 2) + .5f, (int)(roomSize.y / 2) + .5f);


        while (layout.Count < dungeonSize)
        {
            //select a random room to add an exit to.
            Room[] rooms = new Room[layout.Count];
            layout.Values.CopyTo(rooms, 0);
            Room nextRoom = rooms[Random.Range(0, rooms.Length)];

            Vector3 center = nextRoom.boundingRect.center;

            Vector3 extents = nextRoom.boundingRect.extents;

            //if there are no open exits in that room, reset the loop
            if (nextRoom.openExits.Count == 0)
            {
                continue;
            }

            //choose a random available exit direction, get/make the room there, and remove that exit as an option.
            Vector3 exitDir = nextRoom.openExits[Random.Range(0, nextRoom.openExits.Count)];

            GameObject createdRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count - 1)];
            RoomEndButton newServer = createdRoom.GetComponentInChildren<RoomEndButton>();

            Bounds newBounds = new Bounds(center + (Vector3.Dot(extents, exitDir) + Vector3.Dot(newServer.roomBounds.extents, exitDir) + 6) * exitDir, newServer.roomBounds.size);

            bool conflictingLocation = false;
            foreach(Room room in rooms)
            {
                if (newBounds.Intersects(room.boundingRect)){
                    conflictingLocation = true;
                }
            }

            if (conflictingLocation)
            {
                continue;
            }

            layout.Add(newBounds.center, new Room(newBounds));

            layout[newBounds.center].prefab = createdRoom;

            nextRoom.openExits.Remove(exitDir);
            layout[newBounds.center].openExits.Remove(-exitDir);
        }
    }

    void GenerateDungeon()
    {
        foreach (Vector2 key in layout.Keys)
        {
            CreateRoom(key, layout[key]);
        }
    }

    void CreateRoom(Vector2 position, Room room)
    {
        RoomEndButton server = Instantiate(room.prefab, room.boundingRect.center, Quaternion.identity).GetComponentInChildren<RoomEndButton>();

        server.bottomLeftCamLocation = room.boundingRect.min + new Vector3(0, 0, -10);
        server.topRightCamLocation = room.boundingRect.max + new Vector3(0, 0, -10);

        floorGrid.SetTilesBlock(GetBoundsInt(room.boundingRect), roomGrid.GetTilesBlock(GetBoundsInt(server.roomBounds)));

        List<Vector2> exits = new List<Vector2>(new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right });

        foreach(Vector2 exit in room.openExits)
        {
            exits.Remove(exit);
        }

        foreach(Vector3 exit in exits)
        {
            CreateHallway(room.boundingRect.center + (Vector3.Dot(room.boundingRect.extents, exit) + 3) * exit, server, -exit * 2);
        }
    }

    void CreateHallway(Vector3 center, RoomEndButton server, Vector3 doorOffset)
    {
        //create floor tiles for the hallway
        for(int i = -3; i < 3; i++)
        {
            floorGrid.SetTile(new Vector3Int((int)center.x + i, (int)center.y - 1, (int) center.z), floorTile);
            floorGrid.SetTile(new Vector3Int((int)center.x + i, (int)center.y, (int)center.z), floorTile);

            floorGrid.SetTile(new Vector3Int((int)center.x - 1, (int)center.y + i, (int)center.z), floorTile);
            floorGrid.SetTile(new Vector3Int((int)center.x, (int)center.y + i, (int)center.z), floorTile);
        }

        RoomPreviewTile previewTile = Instantiate(previewTilePrefab, center + (doorOffset / 4) + (3f/4f)*new Vector3(doorOffset.y, doorOffset.x), Quaternion.identity).GetComponent<RoomPreviewTile>();

        previewTile.room = server;
        previewTile.speed = .02f;

        Door door = Instantiate(doorPrefab, center + doorOffset, Quaternion.identity).GetComponent<Door>();

        door.room = server;
        server.doors.Add(door);

        RoomEntrance entrance = Instantiate(entrancePrefab, center + doorOffset * 2, Quaternion.identity).GetComponent<RoomEntrance>();
        entrance.centralComputer = server;
        server.entrances.Add(entrance);
    }

    BoundsInt GetBoundsInt(Bounds bounds)
    {
        return new BoundsInt((int)bounds.min.x, (int)bounds.min.y, (int)bounds.min.z, (int)bounds.size.x, (int)bounds.size.y, (int)bounds.size.z);
    }

    class Room
    {
        public List<Vector2> openExits = new List<Vector2>(new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right });
        public Vector2 position;
        public Bounds boundingRect;
        public GameObject prefab;

        public Room(Bounds bounds)
        {
            boundingRect = bounds;
        }
    }

}
