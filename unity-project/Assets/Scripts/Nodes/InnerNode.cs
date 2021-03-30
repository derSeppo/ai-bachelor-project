using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerNode : Node
{
    #region Public Members

    public Color hoverColor;

    #endregion

    #region Private Members
    private GameObject preview;

    private Renderer rend;
    private Color startColor;

    #endregion

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    #region Mouse Event Methods
    //Sets the preview of the selected road and changes the color of the node
    void OnMouseEnter()
    {
        if (Controller.Instance.State == GameState.building)
        {
            rend.material.color = hoverColor;
            Road trackToBuild = BuildController.Instance.TrackToBuild;
            if (trackToBuild != null)
                if (track == null)
                    preview = (GameObject)Instantiate(trackToBuild.Prefab, transform.position + trackToBuild.Offset, transform.rotation);
        }
    }

    //Resets the preview and the color of the node
    void OnMouseExit()
    {
        rend.material.color = startColor;
        if (preview != null)
            Destroy(this.preview);
    }

    private void OnMouseDown()
    {
        if (Controller.Instance.State == GameState.building)
        {
            Road trackToBuild = BuildController.Instance.TrackToBuild;
            if (trackToBuild != null)
            {
                if (track != null)
                {
                    //Rotates the road on the current node
                    Rotate();

                }
                else
                {
                    //Sets the road on the current node
                    if (preview != null)
                        Destroy(this.preview);
                    spawnRoad(trackToBuild);
                }
            }
            else
            {
                if (track != null)
                {
                    //Destroys the road on the current node
                    transform.Rotate(0, rotation * (90), 0);
                    rotation = 0;
                    road = null;
                    Destroy(this.track);
                }
            }
        }
    }
    #endregion
}
