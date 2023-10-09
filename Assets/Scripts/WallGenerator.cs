using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class WallGenerator 
{
  public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer )
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction; // checking for positon
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition); // find all the positions near the floor so we can place a wall there                   
            }
        }
        return wallPositions;
    }
}
