using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour
{
    public Tilemap map;
    public Tilemap obstacle;

    public void ClearMap(bool isObstacle)
    {
        if (isObstacle == false)
        {
            map.ClearAllTiles();
        }
        else
        {
            obstacle.ClearAllTiles();
        }
    }

    public void SetTileTo(int x, int y, TileBase tiletype, bool isObstacle)
    {
        if (isObstacle == false)
        {
            map.SetTile(map.WorldToCell(new Vector3(x, y, 0)), tiletype);
        }
        else
        {
            obstacle.SetTile(obstacle.WorldToCell(new Vector3(x, y, 0)), tiletype);
        }
    }

    public void DeleteTile(int x, int y, bool isObstacle)
    {
        if (isObstacle == false)
        {
            map.SetTile(map.WorldToCell(new Vector3(x, y, 0)), null);
        }
        else
        {
            obstacle.SetTile(obstacle.WorldToCell(new Vector3(x, y, 0)), null);
        }

    }
}
