using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomController : MonoBehaviour
{

    public PersistentDataManager persistentData;
    public float roomSpaceX = 15;
    public float roomSpaceY = 10;

    //public Transform[] doors;
    public Transform doorUp;
    public Transform doorDown;
    public Transform doorLeft;
    public Transform doorRight;

    public Transform activeRoom;
    public Transform mainCamera;
    public Transform player;

    public List<Transform> roomTilesets;
    private Queue<Transform> roomTilesQueue;


    private Vector2Int currentRoom = Vector2Int.zero;

    // Start is called before the first frame update
    void Start()
    {
        roomTilesQueue = new Queue<Transform>(roomTilesets);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void MoveRoom(string door)
    {
        Vector2 moveto = Vector2.zero;
        Vector2 plmove = Vector2.zero;
        Vector2Int nextRoom = currentRoom;
        DoorTrigger.Direction entrance = DoorTrigger.Direction.UP;
        switch (door)
        {
            case "UP": moveto = Vector2.up * roomSpaceY; plmove = Vector2.up * 1.5f; nextRoom += Vector2Int.up; entrance = DoorTrigger.Direction.DOWN; break;
            case "DOWN": moveto = Vector2.down * roomSpaceY; plmove = Vector2.down * 1.5f; nextRoom += Vector2Int.down; entrance = DoorTrigger.Direction.UP; break;
            case "LEFT": moveto = Vector2.left * roomSpaceX; plmove = Vector2.left * 1.5f; nextRoom += Vector2Int.left; entrance = DoorTrigger.Direction.RIGHT; break;
            case "RIGHT": moveto = Vector2.right * roomSpaceX; plmove = Vector2.right * 1.5f; nextRoom += Vector2Int.right; entrance = DoorTrigger.Direction.LEFT; break;
        }
        GenerateRoom(nextRoom);
        currentRoom = nextRoom;
        //Move collision tiles
        activeRoom.localPosition = activeRoom.localPosition + (Vector3)moveto;

        UnblockExits();
        BlockExit(entrance);
        //Move the camera and Player into the new room
        mainCamera.DOMove(activeRoom.localPosition, 0.3f);
        player.DOMove(player.position + (Vector3)plmove, 0.25f);
    }

    public void GenerateRoom(Vector2Int roomPos)
    {
        //Obtain a room blueprint
        RoomGenerator roomGen = new RoomGenerator(persistentData.currentData);
        RoomGenerator.Blueprint blueprint = roomGen.GenerateRoom();

        //Build the room to the blueprint:

        //TODO: Currently just swapping tilesets, do actual room generation.
        //      (Make seperate methods for any special cases.)
        Transform tileset = roomTilesQueue.Dequeue();
        roomTilesQueue.Enqueue(tileset);
        tileset.localPosition = (Vector3)Vector2.Scale(roomPos, new Vector2(roomSpaceX, roomSpaceY)) + Vector3.back;
    }

    public void BlockExit(DoorTrigger.Direction door)
    {
        switch (door)
        {
            case DoorTrigger.Direction.UP: doorUp.gameObject.SetActive(true); break;
            case DoorTrigger.Direction.DOWN: doorDown.gameObject.SetActive(true); break;
            case DoorTrigger.Direction.LEFT: doorLeft.gameObject.SetActive(true); break;
            case DoorTrigger.Direction.RIGHT: doorRight.gameObject.SetActive(true); break;
        }
    }
    public void UnblockExits()
    {
        doorUp.gameObject.SetActive(false);
        doorDown.gameObject.SetActive(false);
        doorLeft.gameObject.SetActive(false);
        doorRight.gameObject.SetActive(false);
    }
}
