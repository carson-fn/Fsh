using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    private Tilemap tilemap;
    private LayerMask tilemapLayer;

    // -------------------------------------------------

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapLayer = 1 << tilemap.gameObject.layer;
    }

    void Update()
    {
        handleInput();
    }

    // -------------------------------------------------

    private void handleInput()
    {
        // mouse primary click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryToSelectBiome();
        }
    }

    private void TryToSelectBiome()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // NOTE: from main camera's persp, get mouse pos and convert to world coords

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, tilemapLayer);
        // NOTE: return smallest z collider that overlaps with pos on layer

        if (hit != null)
        {
            Debug.Log("Clicked collider: " + hit.name);

            // If you want to get the tilemap and tile:
            var tilemap = hit.GetComponent<UnityEngine.Tilemaps.Tilemap>();
            if (tilemap != null)
            {
                Vector3Int cell = tilemap.WorldToCell(mouseWorldPos);
                var tile = tilemap.GetTile(cell);

                Debug.Log("Clicked tile: " + tile);
            }
        }

        //throw new NotImplementedException();
    }
}
