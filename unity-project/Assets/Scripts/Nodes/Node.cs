using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    #region Public Properties
    public Road Road { get => road; }
    public int X { get => (int)transform.position.x; }
    public int Z { get => (int)transform.position.z; }
    #endregion

    #region Protected Members
    protected int rotation = 0;
    protected Road road = null;
    protected GameObject track;
    #endregion

    #region Protected Methods
    //Rotates the node counterclockwise
    protected void Rotate()
    {
        transform.Rotate(0, -90, 0);
        BitArray OldRoadType = new BitArray(road.MovementOptions);
        for (int i = 0; i < 4; i++)
        {
            road.MovementOptions[(i + 1) % 4] = OldRoadType[i];
        }
        rotation = (rotation + 1) % 4;
    }

    //Sets a new road on this node
    protected void spawnRoad(Road rd)
    {
        road = new Road(rd);
        track = (GameObject)Instantiate(rd.Prefab, transform.position + rd.Offset, transform.rotation);
        track.transform.SetParent(gameObject.transform);
    }
    #endregion
}
