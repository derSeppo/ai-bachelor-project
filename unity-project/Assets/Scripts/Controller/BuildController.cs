using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    #region Singleton

    private static BuildController instance = null;

    public static BuildController Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    #region Public Members
    public GameObject prefabRoad;
    public GameObject prefabCurve;
    public GameObject prefabTIntersection;
    public GameObject prefabIntersection;
    public GameObject prefabRoadEnd;
    public GameObject prefabCrossing;

    public Vector3 OffsetRoad;
    public Vector3 OffsetCurve;
    public Vector3 OffsetTIntersection;
    public Vector3 OffsetIntersection;
    public Vector3 OffsetRoadEnd;
    public Vector3 OffsetCrossing;
    #endregion

    #region Public Properties
    public Road Straight { get => straight; }
    public Road Curve { get => curve; }
    public Road TIntersection { get => tIntersection; }
    public Road Intersection { get => intersection; }
    public Road RoadEnd { get => roadEnd; }
    public Road Crossing { get => crossing; }
    public Road StartRoad { get => start; }
    public Road FinishRoad { get => finish; }

    public Road TrackToBuild { get => trackToBuild; set => trackToBuild = value; }

    #endregion

    #region Private Members

    private Road straight;
    private Road curve;
    private Road tIntersection;
    private Road intersection;
    private Road roadEnd;
    private Road crossing;
    private Road start;
    private Road finish;

    private Road trackToBuild;

    #endregion

    //Sets up all possible roads
    void Start()
    {
        instance = this;

        straight = new Road(prefabRoad, OffsetRoad);
        curve = new Road(prefabCurve, OffsetCurve);
        tIntersection = new Road(prefabTIntersection, OffsetTIntersection);
        intersection = new Road(prefabIntersection, OffsetIntersection);
        roadEnd = new Road(prefabRoadEnd, OffsetRoadEnd);
        crossing = new Road(prefabCrossing, OffsetCrossing);
        start = new Road(prefabRoadEnd, OffsetRoadEnd);
        finish = new Road(prefabRoadEnd, OffsetRoadEnd);
    }

}
