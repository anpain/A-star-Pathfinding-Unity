using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Caves : MonoBehaviour
{
    public float cavesHeightMax = 0.6f;
    public float cavesHeightMin = 0.5f;

    public TileBase stone;
    public TileBase preStone;

    public int seed;

    public int mapSize;
    public float[,] noiseMap;

    public NoiseSettings mapSettings;
    public MapRenderer mapRenderer;
    public MapGenerator mapGenerator;

    public void GenerateCaves()
    {
        Debug.Log("Generating caves");

        seed = Random.Range(0, 10000);
        mapSize = mapGenerator.mapSize;
        noiseMap = new float[mapSize, mapSize];

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                mapSettings.startFrequency = 0.005f;
                mapSettings.octaves = 1;

                var noise = NoiseHelper.SumNoise(x + seed, y + seed, mapSettings);
                noiseMap[x, y] = noise;
                if (noise > cavesHeightMin && noise < cavesHeightMax && mapRenderer.obstacle.GetTile(new Vector3Int(x, y, 0)) == stone)
                {
                    mapRenderer.SetTileTo(x, y, preStone, false);
                    mapRenderer.DeleteTile(x, y, true);
                }
            }
        }
    }
}
