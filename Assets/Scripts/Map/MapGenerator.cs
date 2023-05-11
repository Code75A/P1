using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class MapGenerator: MonoBehaviour
{
    [SerializeField]
    private int maxRoomNumber = 5;
    [SerializeField]
    private int corridorGenerateIteration = 10, corridorGenerateMinLength = 10, corridorGenerateMaxLength = 40;
    [SerializeField]
    private bool makeCorridorFirst = false;
    [SerializeField]
    private int roomMinWidth = 20, roomMinHeight = 20, DungeonWidth = 100, DungeonHeight = 100;
    [SerializeField]
    private bool changePositionBeforeIteration = true;
    [SerializeField]
    private int roomGenerateIteration = 10,roomGenerateLength = 10;
    [SerializeField]
    private int seed = 114514;
    [SerializeField]
    protected TilemapPainter tilemapPainter;
    [SerializeField]
    protected Vector2Int startPosition = new Vector2Int(0, 0);
    [SerializeField]
    protected int offset = 1;//房间之间的间隔
    [SerializeField]
    private int minRoadWidth = 3;

    public void RunGeneration()
    {
        tilemapPainter.ClearTilemap();

        MapAlgorithms.setSeed(seed);

        HashSet<Vector2Int> corridor = new();
        HashSet<Vector2Int> floorPositions = new();
        
        if(makeCorridorFirst)
            makeByCorridor(corridor, floorPositions);
        else
            makeByRoom(corridor, floorPositions);

        tilemapPainter.PaintFloorTilemap(floorPositions);

        HashSet<Vector2Int> wallPositions = GenerateWalls(floorPositions);
        tilemapPainter.PaintWallTilemap(wallPositions);
    }

    private void makeByRoom(HashSet<Vector2Int> corridor, HashSet<Vector2Int> floorPositions)
    {
        var rooms = MapAlgorithms.splitRoom(new BoundsInt(new Vector3Int(0, 0, 0),
            new Vector3Int(DungeonWidth, DungeonHeight, 0)), roomMinWidth, roomMinHeight);
        foreach(var room in rooms)
        {
            HashSet<Vector2Int> roomFloors = RoomGenerator.GenerateRoom(roomGenerateIteration, 
                roomGenerateLength, minRoadWidth, new Vector2Int(room.min.x+room.size.x/2, room.min.y+room.size.y/2),
                changePositionBeforeIteration);
            foreach(var floor in roomFloors)
            {
                if(floor.x >= offset + room.min.x && offset + floor.x < room.max.x 
                    && floor.y >= offset + room.min.y && offset + floor.y < room.max.y)
                {
                    floorPositions.Add(floor);
                }
            }
        }
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach(var room in rooms)
        {
            roomCenters.Add(new Vector2Int(room.min.x + room.size.x / 2, room.min.y + room.size.y / 2));
        }
        corridor = ConnectRooms(roomCenters);
        floorPositions.UnionWith(corridor);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[MyRandom.random(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);
        while(roomCenters.Count > 0)
        {
            Vector2Int nextRoomCenter = findClosetRoomCenter(roomCenters, currentRoomCenter);
            var newCorridor = makeCorridorBetween(currentRoomCenter, nextRoomCenter);
            corridor.UnionWith(newCorridor);
            currentRoomCenter = nextRoomCenter;
            roomCenters.Remove(currentRoomCenter);
        }
        return corridor;
    }

    private HashSet<Vector2Int> makeCorridorBetween(Vector2Int currentRoomCenter, Vector2Int nextRoomCenter)
    {
        HashSet<Vector2Int> newCorridor = new();
        Vector2Int currentPosition = currentRoomCenter;
        if (MyRandom.random(0, 1) < 0.5f) {
            while (currentPosition.x < nextRoomCenter.x)
            {
                newCorridor.Add(currentPosition);
                currentPosition.x++;
            }
            while (currentPosition.x > nextRoomCenter.x)
            {
                newCorridor.Add(currentPosition);
                currentPosition.x--;
            }
            while (currentPosition.y < nextRoomCenter.y)
            {
                newCorridor.Add(currentPosition);
                currentPosition.y++;
            }
            while (currentPosition.y > nextRoomCenter.y)
            {
                newCorridor.Add(currentPosition);
                currentPosition.y--;
            }
        }
        else
        {
            while (currentPosition.y < nextRoomCenter.y)
            {
                newCorridor.Add(currentPosition);
                currentPosition.y++;
            }
            while (currentPosition.y > nextRoomCenter.y)
            {
                newCorridor.Add(currentPosition);
                currentPosition.y--;
            }
            while (currentPosition.x < nextRoomCenter.x)
            {
                newCorridor.Add(currentPosition);
                currentPosition.x++;
            }
            while (currentPosition.x > nextRoomCenter.x)
            {
                newCorridor.Add(currentPosition);
                currentPosition.x--;
            }
        }
        return newCorridor;
    }

    private Vector2Int findClosetRoomCenter(List<Vector2Int> roomCenters, Vector2Int currentRoomCenter)
    {
        var chooseRoomCenter = roomCenters[0];
        var minDistance = (chooseRoomCenter - currentRoomCenter).magnitude;
        for(var i = 1; i < roomCenters.Count; i++)
        {
            var distance = (roomCenters[i] - currentRoomCenter).magnitude;
            if(distance < minDistance)
            {
                minDistance = distance;
                chooseRoomCenter = roomCenters[i];
            }
        }
        return chooseRoomCenter;
    }

    private void makeByCorridor(HashSet<Vector2Int> corridor, HashSet<Vector2Int> floorPositions)
    {
        var roomPositions = CorridorGenerator.GenerateCorridor(corridorGenerateIteration,
            corridorGenerateMinLength, corridorGenerateMaxLength, out corridor, startPosition);
        List<BoundsInt> previousBounds = new();
        foreach (var room in roomPositions)
        {
            if (previousBounds.Count >= maxRoomNumber)
            {
                break;
            }
            var newRoom = RoomGenerator.GenerateRoom(roomGenerateIteration, roomGenerateLength,
                minRoadWidth, room, changePositionBeforeIteration);
            Vector2Int minPosition = new Vector2Int(int.MaxValue, int.MaxValue);
            Vector2Int maxPosition = new Vector2Int(int.MinValue, int.MinValue);
            foreach (var position in newRoom)
            {
                minPosition.x = Math.Min(minPosition.x, position.x);
                minPosition.y = Math.Min(minPosition.y, position.y);
                maxPosition.x = Math.Max(maxPosition.x, position.x);
                maxPosition.y = Math.Max(maxPosition.y, position.y);
            }
            bool flag = true;//用于判断是否有重叠
            foreach (var bound in previousBounds)
                if (bound.x <= maxPosition.x + offset && bound.y <= maxPosition.y + offset)
                    if (bound.x + bound.size.x + offset >= minPosition.x && 
                        bound.y + bound.size.y + offset >= minPosition.y)
                    {
                        flag = false;
                        break;
                    }
            if (flag)
            {
                previousBounds.Add(new BoundsInt((Vector3Int)minPosition, (Vector3Int)(maxPosition - minPosition)));
                floorPositions.UnionWith(newRoom);
            }
        }
        floorPositions.UnionWith(corridor);
    }
    
    private HashSet<Vector2Int> GenerateWalls(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in MyDirection2D.directions)
            {
                if (!floorPositions.Contains(position + direction))
                {
                    wallPositions.Add(position + direction);
                }
            }
        }
        return wallPositions;
    }
}
