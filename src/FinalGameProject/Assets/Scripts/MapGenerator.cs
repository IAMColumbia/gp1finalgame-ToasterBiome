using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap wallsMap;

    public TileBase generation_tile;
    public TileBase generation_floor;
    public TileBase wall;
    public TileBase bedrock;

    public int birth = 4;
    public int death = 3;
    public int simSteps = 3;

    public TileBase[] ores;
    public float oreChance;
    public float startingDensity = 0.45f;

    public GameObject spiderSpawner;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateMap()
    {

        TileBase[] allTiles = wallsMap.GetTilesBlock(wallsMap.cellBounds);



        int height = wallsMap.cellBounds.yMax - wallsMap.cellBounds.yMin;
        int width = wallsMap.cellBounds.xMax - wallsMap.cellBounds.xMin;

        TileBase[,] allTiles2D = new TileBase[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allTiles2D[x, y] = allTiles[y * width + x];
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allTiles2D[x, y] == generation_tile)
                {
                    if (Random.value < startingDensity)
                    {
                        allTiles2D[x, y] = generation_floor;
                    }

                }
            }
        }

        TileBase[,] newTiles = allTiles2D;
        for(int i = 0; i < simSteps; i++)
        {
            newTiles = SimulationStep(newTiles);
        }

        //place spider dens

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (newTiles[x, y] == generation_floor)
                {
                    //can place one
                    if (Random.value < 0.01f)
                    {
                        Instantiate(spiderSpawner, new Vector2(x - Mathf.Floor(width/2) - 0.5f, y - Mathf.Floor(height / 4) - 1.5f), Quaternion.identity);
                    }
                }
            }
        }

        //turn generation tiles into real tiles... pain..... only pain...

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(newTiles[x,y] == generation_floor)
                {
                    newTiles[x, y] = null;
                }

                if (newTiles[x, y] == generation_tile)
                {
                    newTiles[x, y] = wall;
                }

            }
        }

        //generate ores

        newTiles = GenerateOres(newTiles);

        

        //reconstruct it into a 1d array for stupid unity tilemap
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allTiles[y * width + x] = newTiles[x, y];
            }
        }

        //update it
        wallsMap.SetTilesBlock(wallsMap.cellBounds, allTiles);
    }

    TileBase[,] SimulationStep(TileBase[,] tiles)
    {
        TileBase[,] newTiles2D = new TileBase[tiles.GetLength(0), tiles.GetLength(1)];

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                //check if it's not null
                if (tiles[x, y] != null)
                {
                    if (tiles[x, y] == generation_tile || tiles[x, y] == generation_floor)
                    {
                        //please attempt to get neighbors
                        int aliveNeighbors = 0;

                        //war is hell
                        if (tiles[x - 1, y] != null && tiles[x - 1, y] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x + 1, y] != null && tiles[x + 1, y] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x - 1, y - 1] != null && tiles[x - 1, y - 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x, y - 1] != null && tiles[x, y - 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x + 1, y - 1] != null && tiles[x + 1, y - 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x - 1, y + 1] != null && tiles[x - 1, y + 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x, y + 1] != null && tiles[x, y + 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }
                        if (tiles[x + 1, y + 1] != null && tiles[x + 1, y + 1] != generation_floor)
                        {
                            aliveNeighbors++;
                        }

                        //now we got all the neighbors, time to apply rules..
                        if (tiles[x, y] == generation_tile)
                        {
                            if (aliveNeighbors < death)
                            {
                                newTiles2D[x, y] = generation_floor;
                            }
                            else
                            {
                                newTiles2D[x, y] = generation_tile;
                            }
                        }
                        else
                        {
                            if (aliveNeighbors > birth)
                            {
                                newTiles2D[x, y] = generation_tile;
                            }
                            else
                            {
                                newTiles2D[x, y] = generation_floor;
                            }
                        }
                    } else
                    {
                        newTiles2D[x, y] = tiles[x, y];
                    }
                }
            }

        }

        return newTiles2D;
    }

    TileBase[,] GenerateOres(TileBase[,] tiles)
    {
        TileBase[,] newTiles2D = new TileBase[tiles.GetLength(0), tiles.GetLength(1)];

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] == wall)
                {
                    //genereate an ore
                    if (Random.value < oreChance)
                    {
                        //just gets a random ore, all likely chance..
                        newTiles2D[x, y] = ores[Random.Range(0, ores.Length)];
                    } else
                    {
                        newTiles2D[x, y] = wall;
                    }
                }
                else
                {
                    newTiles2D[x, y] = tiles[x, y];
                }
            }
        }

        return newTiles2D;
    }
}
