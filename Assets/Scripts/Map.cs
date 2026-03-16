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

            var hitTilemap = hit.GetComponent<Tilemap>();
            // NOTE: return hit collider's tilemap

            if (hitTilemap != null)
            {
                Vector3Int hitCell = hitTilemap.WorldToCell(mouseWorldPos);
                // NOTE: from hit tilemap's persp, take mouse world pos and convert to cell pos
                var hitTile = tilemap.GetTile(hitCell);

                Debug.Log("Clicked tile: " + hitTile);
            }
        }

        //throw new NotImplementedException();
    }
}
