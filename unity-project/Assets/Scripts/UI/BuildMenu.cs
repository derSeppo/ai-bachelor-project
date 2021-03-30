using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{

    #region Menu Selection Methods

    public void SelectStraight()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.Straight;
    }

    public void SelectCurve()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.Curve;
    }

    public void SelectTIntersection()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.TIntersection;
    }

    public void SelectIntersection()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.Intersection;
    }
    public void SelectRoadEnd()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.RoadEnd;
    }

    public void SelectCrossing()
    {
        BuildController.Instance.TrackToBuild = BuildController.Instance.Crossing;
    }

    public void SelectDeleteTrack()
    {
        BuildController.Instance.TrackToBuild = null;
    }

    #endregion
}
