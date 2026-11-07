using System;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 15;
    [SerializeField] private float cellSize = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();    
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogWarning("Tile prefab is not assigned!", this);
            return;
        }
        for (int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(
                    (x + 0.5f) * cellSize,
                    0.05f,
                    (z + 0.5f) * cellSize);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
        Debug.Log($"Grid created: size {width}x{height}, {width * height} tiles.", this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
