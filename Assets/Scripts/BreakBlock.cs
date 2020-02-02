using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
 
public class BreakBlock : MonoBehaviour
{
    public Tile wallTile;
    public Tilemap wallMap;
    public Tilemap breakMap;
 
    private Vector3Int previous;
 
    // do late so that the player has a chance to move in update if necessary
    private void LateUpdate()
    {
        // get current grid location
        Vector3Int currentCell = breakMap.WorldToCell(transform.position);
        // add one in a direction (you'll have to change this to match your directional control)
        // if the position has changed
        if(currentCell != previous)
        {
            if (breakMap.GetTile(previous) != null) 
            {
                // set the new tile
                wallMap.SetTile(previous, wallTile);
                
                // erase previous
                breakMap.SetTile(previous, null);
            }

            // save the new position for next frame
            previous = currentCell;
        }
    }
}