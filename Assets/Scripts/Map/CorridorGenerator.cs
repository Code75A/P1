using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorridorGenerator
{
    public static List<Vector2Int> GenerateCorridor(int iterations, 
        int walkMinLength, int walkMaxLength,out HashSet<Vector2Int> corridor, Vector2Int startPosition)
    {
        List<Vector2Int> roomsPosition = new();
        Vector2Int currentPosition = startPosition;
        corridor = new HashSet<Vector2Int>();
        corridor.Add(currentPosition);
        for (int i = 0; i < iterations; i++)
        {
            Vector2Int direction = MyDirection2D.RandomDirection();
            for (int j = 0; j < MyRandom.random(walkMinLength, walkMaxLength); j++)
            {
                currentPosition = currentPosition + direction;
                corridor.Add(currentPosition);
            }
            roomsPosition.Add(currentPosition);
        }
        return roomsPosition;
    }
}
