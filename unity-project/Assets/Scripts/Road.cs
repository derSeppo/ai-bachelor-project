using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadType {reward, penalty, neutral}

public class Road
{
    #region Public Properties
    public Vector3 Offset { get => offset; }
    public RoadType RoadType { get => roadType; }
    public GameObject Prefab { get => prefab; }
    public BitArray MovementOptions { get => movementOptions; set => movementOptions = value; }
    #endregion

    #region Private Members
    private RoadType roadType;

    private BitArray movementOptions = new BitArray(4);

    private GameObject prefab;
    private Vector3 offset;
    #endregion

    public Road(GameObject prefab, Vector3 offset) {
        this.prefab = prefab;
        this.offset = offset;

        //Sets the variables of the instance based on the name of the model
        switch (prefab.name)
        {
            case "road":
                movementOptions[0] = true;
                movementOptions[2] = true;
                roadType = RoadType.neutral;
                break;
            case "roadCrossing":
                movementOptions[0] = true;
                movementOptions[2] = true;
                roadType = RoadType.penalty;
                break;
            case "curve":
                movementOptions[2] = true;
                movementOptions[3] = true;
                roadType = RoadType.neutral;
                break;
            case "tIntersection":
                movementOptions[0] = true;
                movementOptions[2] = true;
                movementOptions[3] = true;
                roadType = RoadType.neutral;
                break;
            case "intersection":
                movementOptions[0] = true;
                movementOptions[1] = true;
                movementOptions[2] = true;
                movementOptions[3] = true;
                roadType = RoadType.neutral;
                break;
            case "roadEnd":
                movementOptions[0] = true;
                roadType = RoadType.neutral;
                break;
        }
    }

    //Copy Constructor
    public Road(Road rd)
    {
        prefab = rd.Prefab;
        offset = rd.Offset;
        movementOptions = new BitArray(rd.MovementOptions);
        roadType = rd.RoadType;
    }
}
