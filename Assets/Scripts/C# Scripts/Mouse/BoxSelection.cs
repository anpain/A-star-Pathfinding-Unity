using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector2 initialPos;
    private Vector2 currPos;
    private BoxCollider2D boxCollider;

    public BuildingSystem BuildingSystem;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (!BuildingSystem.building && !BuildingSystem.destruction)
        {
            if (Input.GetMouseButtonDown(0) && !PawnManager.mouseOverPawn)
            {
                lineRenderer.positionCount = 4;
                initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPosition(0, new Vector2(initialPos.x, initialPos.y));
                lineRenderer.SetPosition(1, new Vector2(initialPos.x, initialPos.y));
                lineRenderer.SetPosition(2, new Vector2(initialPos.x, initialPos.y));
                lineRenderer.SetPosition(3, new Vector2(initialPos.x, initialPos.y));

                boxCollider = gameObject.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
                boxCollider.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            if (Input.GetMouseButton(0) && !PawnManager.mouseOverPawn)
            {
                currPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (initialPos != currPos)
                {
                    lineRenderer.SetPosition(0, new Vector2(initialPos.x, initialPos.y));
                    lineRenderer.SetPosition(1, new Vector2(initialPos.x, currPos.y));
                    lineRenderer.SetPosition(2, new Vector2(currPos.x, currPos.y));
                    lineRenderer.SetPosition(3, new Vector2(currPos.x, initialPos.y));

                    transform.position = (currPos + initialPos) / 2;

                    boxCollider.size = new Vector2(Mathf.Abs(initialPos.x - currPos.x), Mathf.Abs(initialPos.y - currPos.y));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.positionCount = 0;
                Destroy(boxCollider);
                transform.position = Vector3.zero;
            }
        }
    }
}
