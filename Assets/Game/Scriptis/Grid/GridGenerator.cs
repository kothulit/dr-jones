using System;
using System.Globalization;
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
    [Header("Map")]
    [SerializeField] private TextAsset levelMap;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 15;
    [SerializeField] private float cellSize = 1f;

    private CellType[,] map;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!TryLoadMap()) return;
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

    private void SetWall(int x, int z)
    {
        map[x, z] = CellType.Wall;
    }

    private void SetNone(int x, int z)
    {
        map[x, z] = CellType.None;
    }

    private bool TryLoadMap()
    {
        if (levelMap == null)
        {
            Debug.LogWarning("Level map is not asigned!", this);
            return false;
        }

        string mapData = levelMap.text;
        mapData = mapData.Replace("\r", "");

        string[] rows = mapData.Split('\n');

        int rowCount = rows.Length;

        while (rowCount > 0 && string.IsNullOrWhiteSpace(rows[rowCount - 1]))
        {
            rowCount--;
        }

        if (rowCount != height)
        {
            Debug.LogWarning($"Invalid map height! Expected {height}, got {rowCount}", this);
            return false;
        }

        map = new CellType[width, height];

        for (int row = 0; row < height; row++)
        {
            if (rows[row].Length != width)
            {
                Debug.LogWarning($"Invalid row {row + 1}, expected {width} characters!", this);
                return false;
            }

            int z = height -1 - row;

            for (int x = 0; x < width; x++)
            {
                char symbol = rows[row][x];
                switch (symbol)
                {
                    case '.':
                            SetNone(x, z);
                            break;

                    case '#':
                            SetWall(x, z);
                            break;

                    default:
                        Debug.LogWarning($"Unknown symbol {symbol} at row {row + 1}!", this);
                        return false;
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            if (map[x,0] != CellType.Wall || map[x,height-1] != CellType.Wall)
            {
                Debug.LogWarning("Map border has non wall cell!", this); return false;
            }
        }

        for (int z =0 ; z < height; z++)
        {
            if (map[0, z] != CellType.Wall || map[width -1, z] != CellType.Wall)
            {
                Debug.LogWarning("Map border has non wall cell!", this); return false;
            }
        }

        Debug.Log("Map loaded sucsesful.");
        return true;
    }
}

