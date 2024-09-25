using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public MapRenderer mapRenderer;
    public Caves caves;
    private StartGame startGame;
    public TileBase stone, preStone, grass, dirt, sand, water;
    public int mapSize;
    public int seed;
    public float stoneHeight = 0.8f, preStoneHeight, grassHeight = 0.4f, dirtHeight = 0.3f, sandHeight = 0.2f, waterHeight = 0.15f;
    public NoiseSettings mapSettings;
    public float[,] noiseMap;

    private void Start()
    {
        startGame = GameObject.Find("StartGameEvent").GetComponent<StartGame>();
        PrepareMap();
    }

    public void PrepareMap()
    {
        noiseMap = new float[mapSize, mapSize];
        if (startGame != null)
        {
            ModeSelection();
            seed = startGame.seed;
        }
        else
        {
            seed = Random.Range(0, 100000);

            stoneHeight = 0.61f;
            preStoneHeight = 0.58f;
            grassHeight = 0.27f;
            dirtHeight = 11f;
            sandHeight = 0.25f;
            waterHeight = 0;
            mapSettings.octaves = 2;
            mapSettings.startFrequency = 0.01f;
            mapSettings.persistance = 0.3f;
        }
        Debug.Log("Generating map");
        mapRenderer.ClearMap(false);
        mapRenderer.ClearMap(true);


        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                var noise = NoiseHelper.SumNoise(x + seed, y + seed, mapSettings);
                noiseMap[x, y] = noise;
                if (noise > stoneHeight)
                {
                    mapRenderer.SetTileTo(x, y, stone, true);
                }
                else if (noise > preStoneHeight)
                {
                    mapRenderer.SetTileTo(x, y, preStone, false);
                }
                else if (noise > grassHeight)
                {
                    mapRenderer.SetTileTo(x, y, grass, false);
                }
                else if (noise > dirtHeight)
                {
                    mapRenderer.SetTileTo(x, y, dirt, false);
                }
                else if (noise > sandHeight)
                {
                    mapRenderer.SetTileTo(x, y, sand, false);
                }
                else if (noise > waterHeight)
                {
                    mapRenderer.SetTileTo(x, y, water, true);
                }
            }
        }
        caves.GenerateCaves();
        StartCoroutine (Waiter());
    }

    public void ModeSelection()
    {
        if (startGame.mode == 1) //Пустошь
        {
            stoneHeight = 0.6f;
            preStoneHeight = 0.5f;
            grassHeight = 11f;
            dirtHeight = 0.12f;
            sandHeight = 0.001f;
            waterHeight = 0;
            mapSettings.octaves = 2;
            mapSettings.startFrequency = 0.01f;
            mapSettings.persistance = 0.2f;
        }
        else if (startGame.mode == 2) //Острова
        {
            stoneHeight = 0.68f;
            preStoneHeight = 0.66f;
            grassHeight = 0.51f;
            dirtHeight = 11f;
            sandHeight = 0.495f;
            waterHeight = 0;
            mapSettings.octaves = 2;
            mapSettings.startFrequency = 0.01f;
            mapSettings.persistance = 0.4f;
        }
        else if (startGame.mode == 3) //Обычная  
        {
            stoneHeight = 0.61f;
            preStoneHeight = 0.58f;
            grassHeight = 0.27f;
            dirtHeight = 11f;
            sandHeight = 0.25f;
            waterHeight = 0;
            mapSettings.octaves = 2;
            mapSettings.startFrequency = 0.01f;
            mapSettings.persistance = 0.3f;

        }
        else if (startGame.mode == 4)//Немного гористая
        {
            stoneHeight = 0.53f;
            preStoneHeight = 0.5f;
            grassHeight = 0.37f;
            dirtHeight = 0.3f;
            sandHeight = 0.27f;
            waterHeight = 0;
            mapSettings.octaves = 2;
            mapSettings.startFrequency = 0.008f;
            mapSettings.persistance = 0.5f;
        }
        else
        { //Горы
            stoneHeight = 0.48f;
            preStoneHeight = 0.46f;
            grassHeight = 0.29f;
            dirtHeight = 11;
            sandHeight = 0.27f;
            waterHeight = 0;
            mapSettings.octaves = 6;
            mapSettings.startFrequency = 0.01f;
            mapSettings.persistance = 0.3f;
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.1f);
        AstarPath.active.Scan();
    }
}
