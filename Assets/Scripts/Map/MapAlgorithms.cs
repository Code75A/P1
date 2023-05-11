using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength, int minRoadWidth)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        Vector2Int currentPosition = startPosition;
        path.Add(currentPosition);
        for (int i = 0; i < walkLength; i++)
        {
            Vector2Int direction = MyDirection2D.RandomDirection();
            currentPosition += direction;
            path.Add(currentPosition);
            for (int j = -minRoadWidth / 2; j<= (minRoadWidth + 1) / 2; j++)
            {
                for (int k = -minRoadWidth / 2; k <= (minRoadWidth + 1) / 2; k++)
                {
                    path.Add(currentPosition + new Vector2Int(j, k));
                }
            }
        }
        return path;
    }

    
    public static void setSeed(int seed)
    {
        MyRandom.seed(seed);
    }
    public static List<BoundsInt> splitRoom(BoundsInt roomToSplit, int minWidth, int minHeight)
    {
        List<BoundsInt> roomBounds = new List<BoundsInt>();
        Queue<BoundsInt> queue = new Queue<BoundsInt>();
        queue.Enqueue(roomToSplit);
        while (queue.Count > 0)
        {
            var room = queue.Dequeue();
            if (room.size.x > minWidth && room.size.y > minHeight)
            {
                if (Random.Range(0, 1) < 0.5f)
                {
                    if (room.size.y > minHeight * 2)
                    {
                        splitByY(room, minHeight, queue);
                    }
                    else if (room.size.x > minWidth * 2)
                    {
                        splitByX(room, minWidth, queue);
                    }
                    else roomBounds.Add(room);
                }
            }
        }
        return roomBounds;
    }

    private static void splitByX(BoundsInt room, int minWidth, Queue<BoundsInt> queue)
    {
        if (room.size.x > minWidth)
        {
            int splitX = MyRandom.random(minWidth, room.size.x - minWidth);
            queue.Enqueue(new BoundsInt(room.position, new Vector3Int(splitX, room.size.y, 0)));
            queue.Enqueue(new BoundsInt(new Vector3Int(room.position.x + splitX, room.position.y, 0), new Vector3Int(room.size.x - splitX, room.size.y, 0)));
        }
    }
    private static void splitByY(BoundsInt room, int minHeight, Queue<BoundsInt> queue)
    {
        if (room.size.y > minHeight)
        {
            int splitY = MyRandom.random(minHeight, room.size.y - minHeight);
            queue.Enqueue(new BoundsInt(room.position, new Vector3Int(room.size.x, splitY, 0)));
            queue.Enqueue(new BoundsInt(new Vector3Int(room.position.x, room.position.y + splitY, 0), new Vector3Int(room.size.x, room.size.y - splitY, 0)));
        }
    }
}
public static class MyDirection2D
{
    public static List<Vector2Int> directions = new List<Vector2Int>()

    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    public static Vector2Int RandomDirection()
    {
        return directions[MyRandom.random(0, directions.Count)];
    }
}


