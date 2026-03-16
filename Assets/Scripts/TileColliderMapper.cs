using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileColliderMapper : MonoBehaviour
{
    private Tilemap tilemap;
    private Dictionary<Collider2D, Vector3Int> colliderToCell;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        colliderToCell = new Dictionary<Collider2D, Vector3Int>();

        // Force collider generation
        tilemap.RefreshAllTiles();

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        // NOTE: each tile collider is a child, get colliders from children

        foreach (var col in colliders)
        {
            //Vector3Int cell = tilemap.WorldToCell(col.bounds.center);
            // NOTE: above doesn't work because center of collider is not always center of tile, use transform instead
            Vector3Int cell = tilemap.WorldToCell(col.transform.position);

            colliderToCell[col] = cell;
        }
    }

    public bool TryGetTileFromCollider(Collider2D col, out TileBase tile, out Vector3Int cell)
    {
        // NOTE: out declares var as an output, will fill out and return outs if successful

        if (colliderToCell.TryGetValue(col, out cell))
        {
            tile = tilemap.GetTile(cell);
            return tile != null;
        }
        // NOTE: if cannot get value

        tile = null;
        cell = default;
        return false;
    }
}
