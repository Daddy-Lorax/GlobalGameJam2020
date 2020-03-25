using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class which contains logic for generating the room layout and contents.
/// </summary>
public class RoomGenerator
{
    //Room Generator Attributes
    PersistentData persistentData;
    Context context;

    public bool verboseLog = true;//Whether to print all function calls to the debug logger.

    #region Data Structs
    /// <summary>
    /// Data container for generated rooms which contains info about how to instanciate them.
    /// </summary>
    public struct Blueprint
    {
        //Room Layout
        int numExits;//calculate this in future
        bool hasExitTop, hasExitBottom, hasExitLeft, hasExitRight;
        int tilesetId;//which tile layout to use for this room
        //...TO BE COMPLETED

        //Room Contents
        int numRobots;
        List<Vector2> robotPositions;
        int numScraps;
        List<Vector2> scrapPositions;
        //...TO BE COMPLETED

        //SpecialFlags
        List<string> specialFlags;

        public void AddFlag(string flag)
        {
            if (specialFlags == null) { specialFlags = new List<string>(); }
            specialFlags.Add(flag);
        }

        public void RemoveFlag(string flag)
        {
            if (specialFlags == null) { Debug.LogWarning("Room Blueprint:  Attempted to remove flag from empty list."); }
            else
            {
                if (specialFlags.Contains(flag))
                { specialFlags.Remove(flag); }
                else { Debug.LogWarning("Room Blueprint:  Attempted to remove non-existant flag."); }
            }
        }
    }

    /// <summary>
    /// Data container for the information that is used to generate rooms.
    /// NOTE: This may be redundant with the persistentt data manager, but will be lef in in case it becomes useful.
    /// </summary>
    public struct Context
    {
        //Spatial Data
        DoorTrigger.Direction enterDirection;

        //Progression Data
        int numRoomsBeenToThisFloor;
        //...TO BE COMPLETED
    }
    #endregion

    public RoomGenerator(PersistentData persistentData)
    {
        this.persistentData = persistentData;
    }

    public Blueprint GenerateRoom()
    {
        Blueprint room = new Blueprint();
        LogV("Starting Room Gen.");
        room = GenerateLayout(room);
        room = GenerateContents(room);
        room = CheckOverrides(room);
        LogV("Finished Room Gen");
        return room;
    }


    #region Room Layout

    private Blueprint GenerateLayout(Blueprint room)
    {
        DecideExits(room);
        return room;
    }

    private Blueprint DecideExits(Blueprint room)
    {
        //TODO: Modify room for exits

        return room;
    }

    private Blueprint DecideTileset(Blueprint room)
    {

        //TODO

        return room;
    }

    /// <summary>
    /// 
    /// </summary>
    private Blueprint VerifyLayout(Blueprint room)
    {

        //TODO

        return room;
    }

    #endregion

    #region Room Content

    /// <summary>
    /// Parent function to decide the contents of a room.
    /// </summary>
    private Blueprint GenerateContents(Blueprint room)
    {
        room = DecideNPCRobots(room);
        room = DecideScraps(room);
        room = VerifyContent(room);
        return room;
    }

    /// <summary>
    /// Decides initial npc number and placement
    /// </summary>
    private Blueprint DecideNPCRobots(Blueprint room)
    {

        //TODO

        return room;
    }

    /// <summary>
    /// Decides scraps number and placement.
    /// </summary>
    private Blueprint DecideScraps(Blueprint room)
    {

        //TODO

        return room;
    }

    /// <summary>
    /// Validates the basic room content and applies any content level overrides.
    /// </summary>
    private Blueprint VerifyContent(Blueprint room)
    {

        //TODO

        return room;
    }

    #endregion

    #region Special Overrides
    //////////////////////////////////////////////////////////////
    // NOTE:                                                    //
    //   This section is only for unique room situations        //
    //   which must have a specific layout. For general         //
    //   room generation, use the functions above instead.      //
    //////////////////////////////////////////////////////////////

    /// <summary>
    /// Parent function which checks overrides.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    private Blueprint CheckOverrides(Blueprint room)
    {

        //TODO

        return room;
    }

    /// <summary>
    /// Creates the hallway that leads to the ending.
    /// </summary>
    private Blueprint Override_EndingHall(Blueprint room)
    {
        
        return room;
    }

    /// <summary>
    /// Template for override rules, use the naming convention Override_[NAME].
    /// </summary>
    private Blueprint Override_Example(Blueprint room)
    {
        /*
        if(context.etc)
        {
            //Apply override

        }
        */
        return room;
    }
    #endregion


    private void LogV(string text) { if (verboseLog) { Debug.Log(text); } }
}
