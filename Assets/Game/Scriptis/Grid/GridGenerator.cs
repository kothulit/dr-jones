using System;
using System.Xml.Serialization;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private enum CellType
    {
        None,
        Wall,
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;
    [Header("Settings")]
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 15;
    [SerializeField] private float cellSize = 1f;
    private CellType[,] map;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateMapData();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogWarning("Tile prefab is not assigned!", this);
            return;
        }
        if (wallPrefab == null)
        {
            Debug.LogWarning("Wall prefab is not assigned!", this);
            return;
        }
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(
                    (x + 0.5f) * cellSize,
                    -0.05f,
                    (z + 0.5f) * cellSize);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);

                if (map[x, z] == CellType.Wall)
                {
                    position.y = 0.5f;
                    Instantiate(wallPrefab, position, Quaternion.identity, transform);
                }
            }
        }
        Debug.Log($"Grid created: size {width}x{height}, {width * height} tiles.", this);
    }

    private void CreateMapData()
    {
        map = new CellType[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                map[x, z] = CellType.None;
            }
        }

        for (int x = 0; x <width; x++)
        {
            SetWall(x, 0);
            SetWall(x, height - 1);
        }
        for (int z = 1;  z < height-1; z++)
        {
            SetWall(0, z);
            SetWall(width-1, z);
        }
    }

    private void SetWall(int x, int z)
    {
        map[x, z] = CellType.Wall;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

