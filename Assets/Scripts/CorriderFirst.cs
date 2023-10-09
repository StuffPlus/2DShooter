using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorriderFirst : SimpleRandomWalk // inherit variables from the other script
{
    [SerializeField]
    private int corriderLength = 14, corriderCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;
    // dont need the random walk parameters as i can use it from the simple random walk script
    
    protected override void RunProceduralGeneration()
    {
        CorriderFirstgeneration();
    }

    private void CorriderFirstgeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorriders(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositons = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositons); // create a room at a dead end even going above the room percent

        floorPositions.UnionWith(roomPositons);//Create the corridors and the rooms

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) // creating rooms at dead ends
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position)==false) // checks for dead ends
            {
                var room = RunRandomwalk(randomWalkParameters, position);//Generates room
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent); //calculate the amount of room to select

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); // new id for each potential position for our rooms. ToList to turn the variable into a list. randomised

        foreach (var roomPosition in roomsToCreate)// look through each position 
        {
            var roomFloor = RunRandomwalk(randomWalkParameters, roomPosition); // generate rooms at the positions 
            roomPositions.UnionWith(roomFloor);
            
        }
        return roomPositions; //return as hashset
    }

    private void CreateCorriders(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPositon = startPosition;
        potentialRoomPositions.Add(currentPositon);
        for (int i = 0; i < corriderCount; i++)
        {
            var corrider = DungeonGeneration.RandomWalkCorridor(currentPositon, corriderLength);
            currentPositon = corrider[corrider.Count - 1];
            potentialRoomPositions.Add(currentPositon);
            floorPositions.UnionWith(corrider);

        }
            
    }
}
