using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class DungeonGeneration
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) // collection to store unique values, remove duplicates as there is no way for me to process the same field twice
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousposition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCadinalDirection();
            path.Add(newPosition);
            previousposition = newPosition;


        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corriderLength)// Select a single direction and walk in that direction, take the last postion of the path to get the next start position. Using list this will keeep the order of the last positiuon visited
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCadinalDirection();
        var currentPositon = startPosition;
        corridor.Add(currentPositon);
        for (int i = 0; i < corriderLength; i++)
        {
            currentPositon += direction;
            corridor.Add(currentPositon);

        }
        return corridor;

    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)// represents a bounding box I can determine where the rooms will generate
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)//check the size of the rooms
            {
                if (Random.value < 0.5f) // only prefer one of the splits
                {
                    if (room.size.y >= minHeight * 2)// if we cannot split it vertically we will split it horizontally
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)// if we cannot split it horizontally we will split it vertically
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }

                }
                else
                {
                    if (room.size.y >= minHeight * 2)// if we cannot split it vertically we will split it horizontally
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)// if we cannot split it horizontally we will split it vertically
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }

                }
            }
        }
      return roomsList;
    }

private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
    var xSplit = Random.Range(1, room.size.x);
    BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
    BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
      new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
    roomsQueue.Enqueue(room1);
    roomsQueue.Enqueue(room2);
    }

private static void SplitHorizontally( int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
    var ySplit = Random.Range(1, room.size.x); 
    BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
    BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
        new Vector3Int(room.size.x , room.size.y - ySplit, room.size.z));
    roomsQueue.Enqueue(room1);
    roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // up direction
        new Vector2Int(1,0), //right direction
        new Vector2Int (0,-1), //down direction
        new Vector2Int(-1,0),//Left direction
           
    };

    public static Vector2Int GetRandomCadinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];// get us a random direction
    }
}

