using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class HexTileGeneration : MonoBehaviour
{
    [SerializeField] GameObject hexagonPrefab;

    [Header("Map Settings")]
    [SerializeField] Vector2 mapSize;
    [Space]
    [Header("OFFSETS FOR TILE PLACEMENT")]
    [SerializeField] float _tileXOffset; 
    [SerializeField] float _tileZOffset;

    [Button(ButtonSizes.Small)]
    private void CreateMap()
    {
        ClearMap();
        CreateHexTileMap();
    }

    [Button(ButtonSizes.Small)]
    private void ClearMap()
    {
        List<GameObject> tileList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            tileList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject tile in tileList)
        {
            DestroyImmediate(tile);
        }
        

    }


    public void CreateHexTileMap()
    {
        //for each number on the X Axis (width) 
        for (int x = 0; x <= mapSize.x; x++)
        {
            //for each number on the Z Axis (height) 
            for (int z = 0; z <= mapSize.y; z++)
            {
                GameObject _tile = Instantiate(hexagonPrefab);

                TileInfo _info = _tile.GetComponent<TileInfo>();
                EditorCoordinateTileDebugScript _debug = _tile.GetComponent<EditorCoordinateTileDebugScript>();

                _info.tileCoords.x = x;
                _info.tileCoords.y = z;

                _debug.DisplayCoords(x,z); 

                if (z % 2 == 0)
                {
                    //If even number then offset in a line
                    _tile.transform.position = new Vector3(transform.position.x + x * _tileXOffset, transform.position.y, transform.position.z + z * _tileZOffset);
                }
                else
                {
                    // if odd number then offset half the width to slot in the middle
                    _tile.transform.position = new Vector3(transform.position.x + x * _tileXOffset + _tileXOffset / 2, transform.position.y, transform.position.z + z * _tileZOffset);
                }
                
                SetTileParent(_tile, x, z);

            }
        }
    }

    // Set the tile parent to make clean 
    void SetTileParent(GameObject _tile, int x, int z)
    {
        _tile.transform.parent = transform;
        _tile.name = "HexTile: " + x.ToString() + ", " + z.ToString();

    }
}
