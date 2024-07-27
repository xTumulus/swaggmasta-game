using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;     // needed to make Tiles & Tilemaps work

public class TilemapManager : MonoBehaviour
{
    // static public Tile[] DELVER_TILES;
    // static public Dictionary<char, Tile> COLL_TILE_DICT;
    //
    // [Header ("Inscribed")]
    // public Tilemap visualMap;
    // public Tilemap collisionMap;
    //
    // private TileBase[] visualTileBaseArray;
    // private TileBase[] collTileBaseArray;
    //
    // void Awake()
    // {
    //     LoadTiles();
    // }
    //
    // void Start()
    // {
    //     ShowTiles();
    // }
    //
    // /// <summary>
    // /// Load all the Tiles from the Resources/Tiles_Visual folder into an array
    // /// </summary>
    // void LoadTiles()
    // {
    //     int num;
    //
    //     // Load all the Sprites from DelverTiles
    //     Tile[] tempTiles = Resources.LoadAll<Tile>("Tiles_Visual");
    //
    //     // The order of the Tiles is not guaranteed, so arrange them by number
    //     DELVER_TILES = new Tile[tempTiles.Length];
    //     for (int i = 0; i < tempTiles.Length; i++)
    //     {
    //         string[] bits = tempTiles[i].name.Split ('_');
    //         if (int.TryParse (bits[1], out num))
    //             DELVER_TILES[num] = tempTiles[i];
    //         else
    //             Debug.LogError ("Failed to parse num of: " + tempTiles[i].name);
    //     }
    //     Debug.Log ("Parsed " + DELVER_TILES.Length + " tiles into TILES_VISUAL");
    //
    //     // Load all of the Sprites from CollisionTiles
    //     tempTiles = Resources.LoadAll<Tile>("Tiles_Collision");
    //     // Collision tiles are stored in a Dictionary for easier access
    //     COLL_TILE_DICT = new Dictionary<char, Tile>();
    //     for (int i = 0; i < tempTiles.Length; i++)
    //     {
    //         char c = tempTiles[i].name[0];
    //         COLL_TILE_DICT.Add (c, tempTiles[i] );
    //     }
    //     Debug.Log ("COLL_TILE_DICT contains " + COLL_TILE_DICT.Count + " tiles");
    // }
    //
    // /// <summary>
    // /// Uses GetMapTiles() to generate an array of TileBases with the right tile
    // ///  in each position on the map.  Then set them as a block on visualMap.
    // /// </summary>
    // void ShowTiles()
    // {
    //     visualTileBaseArray = GetMapTiles();
    //     visualMap.SetTilesBlock (MapInfo.GET_MAP_BOUNDS(), visualTileBaseArray);
    //
    //     collTileBaseArray = GetCollisionTiles();
    //     collisionMap.SetTilesBlock (MapInfo.GET_MAP_BOUNDS(), collTileBaseArray);
    // }
    //
    // /// <summary>
    // /// Use MapInfo.MAP to create a TileBase[] array holding the tiles to file
    // ///  the visualMap Tilemap.
    // /// </summary>
    // TileBase[] GetMapTiles()
    // {
    //     int tileNum;
    //     Tile tile;
    //     TileBase[] mapTiles = new TileBase[MapInfo.W * MapInfo.H];
    //     for (int y = 0; y < MapInfo.H; y++)
    //     {
    //         for (int x = 0; x < MapInfo.W; x++)
    //         {
    //             tileNum = MapInfo.MAP[x,y];
    //             tile = DELVER_TILES[tileNum];
    //             mapTiles[y * MapInfo.W + x] = tile;
    //         }
    //     }
    //     return mapTiles;
    // }
    //
    // /// <summary>
    // /// Use MapInfo.MAP and MapInfo.COLLISIONS to create a TileBase[] array
    // ///  holding the tiles to fill the collisionMap TileMap
    // /// </summary>
    // /// <returns>The TileBases for collisionMap</returns>
    // TileBase[] GetCollisionTiles()
    // {
    //     int tileNum;
    //     Tile tile;
    //     char tileChar;
    //     TileBase[] mapTiles = new TileBase[MapInfo.W * MapInfo.H];
    //     for (int y = 0; y < MapInfo.H; y++)
    //     {
    //         for (int x = 0; x < MapInfo.W; x++)
    //         {
    //             tileNum = MapInfo.MAP[x,y];
    //             tileChar = MapInfo.COLLISIONS[tileNum];
    //             tile = COLL_TILE_DICT[tileChar];
    //             mapTiles[y * MapInfo.W + x] = tile;
    //         }
    //     }
    //     return mapTiles;
    // }
}
