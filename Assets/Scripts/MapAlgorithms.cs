using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAlgorithms
{
    public HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int WalkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        Vector2Int currentPosition = startPosition;
        path.Add(currentPosition);
        for (int i = 0; i < WalkLength; i++)
        {
            Vector2Int direction = RandomDirection();
            currentPosition += direction;
            path.Add(currentPosition);
        }
        return path;
    }

    public List<Vector2Int> directions = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    public Vector2Int RandomDirection()
    {
        return directions[MyRandom.random(0, directions.Count)];
    }
    public void setSeed(int seed)
    {
        MyRandom.seed(seed);
    }
}



