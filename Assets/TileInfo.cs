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

    [SerializeField] Material[] _materials;
    [Space]
    [SerializeField] Renderer _renderer;
    [Space]
    public TileMapData mapData; 

    public Vector2 tileCoords; 

    public TileType tileType;
    public TileState tileState;

    public void SetTileMapData()
    {
        mapData.tileCoords = tileCoords;
        mapData.tileType = tileType;
        mapData.tileState = tileState;
    }

    [Button(ButtonSizes.Small)]
    public void ToggleTile()
    {

        tileType += 1;

        if((int)tileType >= Enum.GetValues(typeof(TileType)).Length)
        {
            tileType = TileType.random;
        }
        RefreshTileInfo();
        
    }

    [Button(ButtonSizes.Small)]
    public void RefreshTileInfo()
    {
        foreach (Material _mat in _renderer.materials)
        {
            _mat.CopyPropertiesFromMaterial(_materials[(int)tileType]);
        }
        SetTileMapData();
    }

    

}
