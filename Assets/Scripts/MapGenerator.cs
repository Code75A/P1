using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : AbstractMapGenerator
{
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    private int walkLength = 10;
    [SerializeField]
    private int seed = 114514;
    [SerializeField]
    private bool changePositionBeforeIteration = true;


    protected override void RunGeneration()
    {
        HashSet<Vector2Int> floorPositions = RandomWalk();
        tilemapPainter.ClearTilemap();
        tilemapPainter.PaintFloorTilemap(floorPositions);
    }


    private HashSet<Vector2Int> RandomWalk()
    {
        MapAlgorithms mapAlgorithms = new MapAlgorithms();
        mapAlgorithms.setSeed(seed);
        Vector2Int currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i=0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = mapAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if (changePositionBeforeIteration)
            {
                currentPosition = floorPositions.ElementAt(MyRandom.random(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
