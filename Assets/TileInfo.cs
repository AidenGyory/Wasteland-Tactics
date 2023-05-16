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
    public Vector2 tileCoords; 

    public TileType tileType;
    public TileState tileState; 


    [Button(ButtonSizes.Small)]
    public void ToggleTile()
    {

        tileType += 1;

        if((int)tileType > Enum.GetValues(typeof(TileType)).Length)
        {
            tileType = TileType.random;
        }

        _renderer.materials[0] = _materials[(int)tileType];
        _renderer.materials[1] = _materials[(int)tileType];

    }

}
