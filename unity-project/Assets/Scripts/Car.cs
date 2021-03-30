using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region Singleton

    private static Car instance = null;

    public static Car Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    #region Public Members
    public StartNode startNode;
    public EndNode EndNode;
    public GameObject carPrefab;
    #endregion

    #region Public Properties
    public Node CurrNode { get => currNode; }
    #endregion

    #region Private Members
    private Node currNode;

    private int carRotation;
    private GameObject carModel;

    private Dictionary<int, Vector3> movementDirection;
    #endregion

    void Start()
    {
        instance = this;

        currNode = startNode;

        movementDirection = new Dictionary<int, Vector3>();
        movementDirection.Add(0, Vector3.back);
        movementDirection.Add(1, Vector3.right);
        movementDirection.Add(2, Vector3.forward);
        movementDirection.Add(3, Vector3.left);
        
    }

    #region Public Methods
    //Reset the cars position and model
    public void ResetCar()
    {
        carRotation = 1;
        Destroy(carModel);
        carModel = Instantiate(carPrefab, startNode.transform.position + new Vector3(0, 1.1f, 0), Quaternion.Euler(0, carRotation * -90, 0));
        currNode = startNode;
    }

    //Move the car to the next node
    public void MoveCar(int direction) {
        RaycastHit hit;
        if (Physics.Raycast(currNode.transform.position, movementDirection[direction], out hit, 4))
        {
            Node nextNode = hit.collider.gameObject.GetComponent<Node>();

            if (currNode.Road.MovementOptions[direction])
                if (nextNode.Road.MovementOptions[(direction + 2) % 4])
                {
                    carModel.transform.SetPositionAndRotation(nextNode.transform.position + new Vector3(0, 1.1f, 0), Quaternion.Euler(0, direction * -90, 0));
                    currNode = nextNode;
                }
        }
    }

    //Get all possible movements from the current node
    //0 Unten; 1 Rechts; 2 Oben; 3 Links
    public BitArray getMovementOptions()
    {
        BitArray aiMovementOptions = new BitArray(4);
        Node[] neighbors = new Node[4];
        neighbors[0] = LookForNeighbor(Vector3.back);
        neighbors[1] = LookForNeighbor(Vector3.right);
        neighbors[2] = LookForNeighbor(Vector3.forward);
        neighbors[3] = LookForNeighbor(Vector3.left);
        for (int i = 0; i < 4; i++)
        {
            if ((neighbors[i] != null) && (neighbors[i].Road != null))
            {
                if ((currNode.Road.MovementOptions[i]) && (neighbors[i].Road.MovementOptions[(i + 2) % 4]))
                {
                    aiMovementOptions[i] = true;
                }
            }
        }
        return aiMovementOptions;
    }
    #endregion

    #region Private Methods
    //Get the neighbor of the current node in a specific direction
    private Node LookForNeighbor(Vector3 RaycastDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(currNode.transform.position, RaycastDirection, out hit, 4))
        {
            Node nextNode = hit.collider.gameObject.GetComponent<Node>();
            return nextNode;
        }
        return null;
    }
    #endregion
}
