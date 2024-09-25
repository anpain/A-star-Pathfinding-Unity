using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Queue : MonoBehaviour
{
    private GameObject target;
    private GameObject[] AllPawnsGO;

    private List<Vector3Int> queueToBuild = new List<Vector3Int>();
    private List<GameObject> queueToMake = new List<GameObject>();
    private List<Vector3> queueToMoving = new List<Vector3>();


    private void Start()
    {
        AllPawnsGO = (GameObject.FindGameObjectsWithTag("Player"));
    }

    public void AddToQueueToBuild(Vector3Int posToBuild)
    {
        queueToBuild.Add(posToBuild);
        queueToMoving.Add(posToBuild);
    }
}
