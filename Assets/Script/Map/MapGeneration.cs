using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileBase> GrassTiles;
    public List<TileBase> FlowerTiles;
    public List<TileBase> PavementTiles;
    public List<GameObject> PropPrefabs;
    public List<GameObject> PlantPrefabs;
    public Transform PlantRoot;
    public Transform PropRoot;

    public int MapSize;

    public float flowerWeight;

    public int PavementsCount;
    public float PavementGrowthChance;
    private List<Vector2Int> Directions = new List<Vector2Int>();

    public int minPlantCount, maxPlantCount;
    public int minPropsCount, maxPropsCount;

    void Start()
    {
        Directions.Add(new Vector2Int(0, 1));
        Directions.Add(new Vector2Int(0, -1));
        Directions.Add(new Vector2Int(1, 0));
        Directions.Add(new Vector2Int(-1, 0));
        GenerateGrassGround();
        GeneratePavements();
        GeneratePlantAndProps();
    }


    void Update()
    {
        
    }

    void GenerateGrassGround()
    {
        for(int i = -MapSize; i <= MapSize; i++)
            for(int j = -MapSize; j <= MapSize; j++)
            {
                if(checkRandom(flowerWeight)) tilemap.SetTile(new Vector3Int(i, j, 0), FlowerTiles[Random.Range(0, FlowerTiles.Count)]);
                else tilemap.SetTile(new Vector3Int(i, j, 0), GrassTiles[Random.Range(0, GrassTiles.Count)]);
            }
    }
    void GeneratePavements()
    {
        for(int Pav = 0; Pav <= PavementsCount; Pav++)
        {
            int x = Random.Range(-MapSize + 1, MapSize - 1);
            int y = Random.Range(-MapSize + 1, MapSize - 1);
            tilemap.SetTile(new Vector3Int(x, y, 0), PavementTiles[Random.Range(0, PavementTiles.Count)]);
            for(int i = 0; i < 4; i++)
            {
                if (checkRandom(PavementGrowthChance)) PavementGrow(x + Directions[i].x, y + Directions[i].y, i);
            }
        }
    }
    void PavementGrow(int x,int y,int dir)
    {
        if (x <= -MapSize || x >= MapSize || y <= -MapSize || y >= MapSize) return;
        tilemap.SetTile(new Vector3Int(x, y, 0), PavementTiles[Random.Range(0, PavementTiles.Count)]);
        if (!checkRandom(PavementGrowthChance)) return;
        if (checkRandom(0.5f))
            PavementGrow(x + Directions[dir].x, y + Directions[dir].y, dir);
        else if (checkRandom(0.5f))
            PavementGrow(x + Directions[dir ^ 2].x, y + Directions[dir ^ 2].y, dir ^ 2);
        else PavementGrow(x + Directions[dir ^ 3].x, y + Directions[dir ^ 3].y, dir ^ 3);
    }

    void GeneratePlantAndProps()
    {
        int plantCnt = Random.Range(minPlantCount, maxPlantCount + 1);
        for(int i = 0; i < plantCnt; i++)
        {
            int idx = Random.Range(0, PlantPrefabs.Count);
            float x = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            float y = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            while (Mathf.Abs(x) <= 5f) x = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            while (Mathf.Abs(y) <= 5f) y = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            Vector3 pos = new Vector3(x, y, 0);
            GameObject obj = GameObject.Instantiate(PlantPrefabs[idx], pos, Quaternion.identity);
            obj.transform.SetParent(PlantRoot);
        }

        int propsCnt = Random.Range(minPropsCount, maxPropsCount + 1);
        for (int i = 0; i < propsCnt; i++)
        {
            int idx = Random.Range(0, PropPrefabs.Count);
            float x = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            float y = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            while (Mathf.Abs(x) <= 5f) x = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            while (Mathf.Abs(y) <= 5f) y = Random.Range(-(MapSize - 1) * 1f, (MapSize - 1) * 1f);
            Vector3 pos = new Vector3(x, y, 0);
            GameObject obj = GameObject.Instantiate(PropPrefabs[idx], pos, Quaternion.identity);
            obj.transform.SetParent(PropRoot);
        }
    }

    bool checkRandom(float val) { return Random.value <= val; }
}
