using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
using System.Linq;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase floorTile;
    [SerializeField]
    private TileBase wallTop;
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]
    private EnemySpawn enemySpawn;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile, true);
        
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile, bool doSpawn = false)
    {
        foreach ( var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
            if (doSpawn)
            {
               if( Random.Range(1, 100) < 5)
                {
                    var spawnPosition = tilemap.WorldToCell((Vector3Int)position);
                    enemySpawn.Spawn(spawnPosition);
                }
                

            }
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    public void Clear()
    {
        floorTilemap.ClearAllTiles();// clear the floor tiles
        wallTilemap.ClearAllTiles(); // clear the wall tiles

        var enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies) //while 
        {
            Destroy(enemy); // Destroys the prefab
        }
    }
}
