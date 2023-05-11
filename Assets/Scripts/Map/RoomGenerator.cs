using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RoomGenerator
{
    public static HashSet<Vector2Int> GenerateRoom(int iteration, int walkLength,int minRoadWidth, Vector2Int startPosition, bool changePositionBeforeIteration)
    {
        Vector2Int currentPosition = startPosition;
        HashSet<Vector2Int> returnedPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iteration; i++)
        {
            HashSet<Vector2Int> path = MapAlgorithms.SimpleRandomWalk(currentPosition, walkLength, minRoadWidth);
            returnedPositions.UnionWith(path);
            if (changePositionBeforeIteration)
            {
                currentPosition = returnedPositions.ElementAt(MyRandom.random(0, returnedPositions.Count));
            }
        }
        return returnedPositions;
    }
}
