using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class TileInfo : MonoBehaviour
{
    public enum TileType
    {
        random, //gray 
        spawnTile, //Green
        empty, //white
        resource, //blue
        power, //yellow
        hazard, //red
        reward, //purple
        mountain, //Orange
    }

    public enum TileState
    {
        AlreadyFlipped,
        Flippable,
        NotFlippable,
    }
    public Vector2 tileCoords;
    [Space]
    public TileType tileType;
    public TileState tileState;
    [Space]
    
    [Header("Tile Data for Map Generation")]
    public TileMapData mapData;
    [SerializeField] GameObject[] _tileModels;

    private TileType _previousTileType; 
    private TileState _previousTileState;

    public void SetTileMapData()
    {
        mapData.tileCoords = tileCoords;
        mapData.tileType = tileType;
        mapData.tileState = tileState;
    }

    private void OnValidate()
    {
        if (tileType != _previousTileType || tileState != _previousTileState)
        {
            _previousTileType = tileType;
            _previousTileState = tileState;
            RefreshTileInfo();
        }
    }

    public void RefreshTileInfo()
    {
        for (int i = 0; i < _tileModels.Length; i++)
        {
            if(i != (int)tileType)
            {
                _tileModels[i].SetActive(false);
            }
            else
            {
                _tileModels[i].SetActive(true);
            }
        }

        if(tileState == TileState.AlreadyFlipped)
        {
            transform.eulerAngles = new Vector3(90,0,90);
        }
        else
        {
            transform.eulerAngles = new Vector3(-90, 0, 90);
        }

        SetTileMapData();
    }

    

}
