using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapPainter tilemapPainter = null;
    [SerializeField]
    protected Vector2Int startPosition = new Vector2Int(0, 0);

    public void GenerateMap()
    {
        tilemapPainter.ClearTilemap();
        RunGeneration();
    }

    protected abstract void RunGeneration();
}
