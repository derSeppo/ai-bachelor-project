using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : Node
{
    #region Public Members/Properties
    [Range(0, 3)]
    public int roadRotation;
    public int StartingPosition { get => startingPosition; }
    #endregion

    #region Private Members
    private Dictionary<int, float> startingPositions;
    private int startingPosition;
    #endregion

    //Start coroutine, because it needs access to the instance of BuildController
    IEnumerator Start()
    {
        setupStartingPositions();
        startingPosition = 0;

        yield return new WaitForSeconds(0.1f);
        spawnRoad(BuildController.Instance.StartRoad);
        while (roadRotation > 0)
        {
            Rotate();
            roadRotation--;
        }
    }

    public void moveStart(int pos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, startingPositions[pos]);
    }

    //Links every possible starting position to a number from 0 to 10
    private void setupStartingPositions()
    {
        startingPositions = new Dictionary<int, float>();
        for(int i = 0; i <= 10; i++)
        {
            startingPositions.Add(i, 20 - i * 4);
        }
    }
}
