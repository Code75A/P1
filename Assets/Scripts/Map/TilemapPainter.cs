using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private Tile floorTile, wallTile;

    public void PaintFloorTilemap(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTilemap(floorPositions, floorTilemap, floorTile);
    }
    public void PaintWallTilemap(IEnumerable<Vector2Int> wallPositions)
    {
        PaintTilemap(wallPositions, wallTilemap, wallTile);
    }

    private void PaintTilemap(IEnumerable<Vector2Int> positions, Tilemap tilemap, Tile tile)
    {
        foreach(var position in positions)
        {
            PaintSingleTile(position, tilemap, tile);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, Tile tile)
    {
        tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tile);
    }

    public void ClearTilemap()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
