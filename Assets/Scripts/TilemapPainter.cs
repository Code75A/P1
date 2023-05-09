using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tile floorTile;

    public void PaintFloorTilemap(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTilemap(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTilemap(IEnumerable<Vector2Int> positions, Tilemap tilemap, Tile tile)
    {
        foreach(var position in positions)
        {
            paintSingleTile(position, tilemap, tile);
        }
    }

    private void paintSingleTile(Vector2Int position, Tilemap tilemap, Tile tile)
    {
        tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tile);
    }

    public void ClearTilemap()
    {
        floorTilemap.ClearAllTiles();
    }
}
