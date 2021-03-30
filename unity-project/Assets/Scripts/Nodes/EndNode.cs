using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : Node
{
    #region Public Members
    [Range(0, 3)]
    public int roadRotation;
    public Color hoverColor;
    #endregion

    #region Private Members
    private Renderer rend;
    private Color startColor;
    private Vector3 startMousePosition;
    #endregion

    //Start coroutine, because it needs access to the instance of BuildController
    IEnumerator Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        yield return new WaitForSeconds(0.1f);
        spawnRoad(BuildController.Instance.FinishRoad);
        while (roadRotation > 0)
        {
            Rotate();
            roadRotation--;
        }
    }

    #region Mouse Event Methods
    //Changes the color of the node
    void OnMouseEnter()
    {
        if (Controller.Instance.State == GameState.building)
        {
            rend.material.color = hoverColor;

        }
    }

    //Resets the color of the node
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    //Drags the Finish inside the end zone as soon as the mouse was moved far enough
    private void OnMouseDrag()
    {
        if (Controller.Instance.State == GameState.building)
        {
            float MousePositionChange = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.y)).z - startMousePosition.z;
            if (MousePositionChange >= 4 && transform.position.z + 4 <= 20)
            {
                transform.Translate(new Vector3(0, 0, 4), Space.World);
                startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.y));
            }
            else if (MousePositionChange <= -4 && transform.position.z - 4 >= -20)
            {
                transform.Translate(new Vector3(0, 0, -4), Space.World);
                startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.y));
            }
        }
    }

    private void OnMouseDown()
    {
        startMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.y));
    }
    #endregion
}
