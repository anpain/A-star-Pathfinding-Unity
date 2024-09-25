using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Movement : MonoBehaviour
{
    public GameObject target;
    public List<GameObject> allTargets = new List<GameObject>();

    void Update()
    {
        if (GetComponent<PawnManager>().isSelected)
        {
            if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
            {
                AddPoint();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Moving(); 
            }
        }

        if (allTargets.Count > 0)
        {
            if (Vector3.Distance(transform.position, allTargets[0].transform.position) <= 1f)
            {
                Destroy(allTargets[0]);
                allTargets.RemoveAt(0);
            }
        }

        if (this.GetComponent<AIDestinationSetter>().target == null && allTargets.Count > 0)
        {
            this.GetComponent<AIDestinationSetter>().target = allTargets[0].transform;
        }
    }

    public void Moving()
    {
        if (allTargets.Count > 0)
        {
            for (int i = 0; i < allTargets.Count; i++)
            {
                Destroy(allTargets[i]);
            }
        }
        allTargets.Clear();
        AddPoint();
    }

    public void AddPoint()
    {
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1);
        GameObject targetPawns;
        targetPawns = Instantiate(target, gameObject.transform.parent);
        targetPawns.transform.position = mousePos;
        allTargets.Add(targetPawns);
    }
}
