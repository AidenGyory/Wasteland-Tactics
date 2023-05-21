using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapData
{
    
    //Tile Map generation data
    public Vector2 tileCoords;
    public TileInfo.TileType tileType;
    public TileInfo.TileState tileState;
    public TileInfo.tileOwners AssignedTileOwnerOnStart;

    public TileMapData(Vector2 tileCoords, TileInfo.TileType tileType, TileInfo.TileState tileState, TileInfo.tileOwners tileOwner)
    {
        this.tileCoords = tileCoords;
        this.tileType = tileType;
        this.tileState = tileState;
        this.AssignedTileOwnerOnStart = tileOwner;
    }
}

[CreateAssetMenu(fileName = "New Map Profile", menuName = "Wasteland Tactics/Map/Create New Map Profile")]
public class MapProfileSO : ScriptableObject
{
    public GameObject ThemedHexagonPrefab;
    public List<TileMapData> mapData;
    
}
