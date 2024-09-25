using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class BuildingSystem : MonoBehaviour
{
    public bool building;
    public bool destruction;
    public bool previeLine;

    [Space(5)]
    public Queue queue;

    [SerializeField] private Block block;

    [SerializeField] private TileBase highlightTile;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private Tilemap selectionTilemap;

    private Vector3Int startPos;
    private Vector3Int endPoS;

    private BoundsInt bound;
    private Bounds obstacle;

    private List<Vector3Int> constructionQueue = new List<Vector3Int>();

    private void Update()
    {
        if (building)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = GetMouseOnGridPos();
            }

            if (Input.GetMouseButton(0) && previeLine)
            {
                endPoS = GetMouseOnGridPos();
                PreviewLine();
            }
            else if (Input.GetMouseButton(0) && !previeLine)
            {
                endPoS = GetMouseOnGridPos();
                PreviewBox();
            }

            if (Input.GetMouseButtonUp(0))
            {
                AddToQueue();
            }
        }

        if (destruction)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = GetMouseOnGridPos();
            }

            if (Input.GetMouseButton(0) && previeLine)
            {
                endPoS = GetMouseOnGridPos();
                PreviewLine();
            }
            else if (Input.GetMouseButton(0) && !previeLine)
            {
                endPoS = GetMouseOnGridPos();
                PreviewBox();
            }

            if (Input.GetMouseButtonUp(0))
            {
                Delete();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            building = false;
            destruction = false;
        }   
    }

    private void PreviewBox()
    {
        selectionTilemap.ClearAllTiles();

        bound.xMin = endPoS.x < startPos.x ? endPoS.x : startPos.x;
        bound.xMax = endPoS.x > startPos.x ? endPoS.x : startPos.x;
        bound.yMin = endPoS.y < startPos.y ? endPoS.y : startPos.y;
        bound.yMax = endPoS.y > startPos.y ? endPoS.y : startPos.y;

        for (int x = bound.xMin; x <= bound.xMax; x++)
        {
            for (int y = bound.yMin; y <= bound.yMax; y++)
            {
                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) == null && building)
                    selectionTilemap.SetTile(new Vector3Int(x, y, 0), highlightTile);

                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) != null && destruction)
                    selectionTilemap.SetTile(new Vector3Int(x, y, 0), highlightTile);
            }
        }
    }

    private void PreviewLine()
    {
        selectionTilemap.ClearAllTiles();

        float diffX = Mathf.Abs(endPoS.x - startPos.x);
        float diffY = Mathf.Abs(endPoS.y - startPos.y);

        bool isHorizontal = diffX >= diffY;

        if (isHorizontal)
        {
            bound.xMin = endPoS.x < startPos.x ? endPoS.x : startPos.x;
            bound.xMax = endPoS.x > startPos.x ? endPoS.x : startPos.x;
            bound.yMin = startPos.y;
            bound.yMax = startPos.y;
        }
        else
        {
            bound.yMin = endPoS.y < startPos.y ? endPoS.y : startPos.y;
            bound.yMax = endPoS.y > startPos.y ? endPoS.y : startPos.y;
            bound.xMin = startPos.x;
            bound.xMax = startPos.x;
        }

        for (int x = bound.xMin; x <= bound.xMax; x++)
        {
            for (int y = bound.yMin; y <= bound.yMax; y++)
            {
                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) == null && building)
                    selectionTilemap.SetTile(new Vector3Int(x, y, 0), highlightTile);

                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) != null && destruction)
                    selectionTilemap.SetTile(new Vector3Int(x, y, 0), highlightTile);
            }
        }
    }

    private void AddToQueue()
    {
        for (int x = bound.xMin; x <= bound.xMax; x++)
        {
            for (int y = bound.yMin; y <= bound.yMax; y++)
            {
                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) == null)
                {
                    queue.AddToQueueToBuild(new Vector3Int(x, y, 0));
                }
            }
        }
        selectionTilemap.ClearAllTiles();
    }

    private void  Build(Vector3Int position)
    {
        obstacle = new Bounds(new Vector3(position.x, position.y, 0), new Vector3(1, 1, 0));
        var guo = new GraphUpdateObject(obstacle);
        AstarData.active.UpdateGraphs(guo);

        mainTilemap.SetTile(position, block.tile);
    }

    private void Delete()
    {
        for (int x = bound.xMin; x <= bound.xMax; x++)
        {
            for (int y = bound.yMin; y <= bound.yMax; y++)
            {
                if (mainTilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                    mainTilemap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
        selectionTilemap.ClearAllTiles();
    }


    private Vector3Int GetMouseOnGridPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPos = mainTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;

        return mouseCellPos;
    }
}
