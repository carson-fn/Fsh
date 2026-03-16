using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    private TileColliderMapper mapper;

    private Tilemap tilemap;
    private LayerMask tilemapLayer;

    // -------------------------------------------------

    private void Awake()
    {
        mapper = GetComponent<TileColliderMapper>();

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
            TrySelectBiome();
        }
    }

    private void TrySelectBiome()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // NOTE: from main camera's persp, get mouse pos and convert to world coords

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, tilemapLayer);
        // NOTE: return smallest z collider that overlaps with pos on layer

        if (hit != null)
        {
            Debug.Log("Clicked collider: " + hit.name);

            if (mapper.TryGetTileFromCollider(hit, out TileBase tile, out Vector3Int cell))
            {
                Debug.Log($"Clicked tile {tile.name} at {cell}");

                if (tile is BiomeTile biome)
                {
                    Debug.Log("Biome: " + biome.biomeName);
                }

            }
            else
            {
                Debug.Log("Clicked collider but tile does not exist: " + hit.name);
            }


            //var hitTilemap = hit.GetComponent<Tilemap>();
            //// NOTE: return hit collider's tilemap

            //if (hitTilemap != null)
            //{
            //    Vector3Int hitCell = hitTilemap.WorldToCell(mouseWorldPos);
            //    // NOTE: from hit tilemap's persp, take mouse world pos and convert to cell pos
            //    var hitTile = tilemap.GetTile(hitCell);

            //    Debug.Log("Clicked tile: " + hitTile);
            //}
        }

        //throw new NotImplementedException();
    }
}
