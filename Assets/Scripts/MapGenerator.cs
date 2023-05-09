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
    private bool startRandomlyEachIteration = true;


    protected override void RunGeneration()
    {
        HashSet<Vector2Int> floorPositions = RandomWalk();
        tilemapPainter.ClearTilemap();
        tilemapPainter.PaintFloorTilemap(floorPositions);
    }


    private HashSet<Vector2Int> RandomWalk()
    {
        Vector2Int currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i=0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = MapAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration)
            {
                currentPosition = path.ElementAt(UnityEngine.Random.Range(0, path.Count));
            }
        }
        return floorPositions;
    }
}
