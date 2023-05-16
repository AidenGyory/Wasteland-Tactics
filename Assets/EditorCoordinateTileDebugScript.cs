using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorCoordinateTileDebugScript : MonoBehaviour
{

    [SerializeField] TMP_Text coordsTopDisplay; 
    [SerializeField] TMP_Text coordsBottomDisplay;

    // Start is called before the first frame update
    public void DisplayCoords(int x, int y)
    {
        coordsTopDisplay.text = "" + x + "," + y;
        coordsBottomDisplay.text = "" + x + "," + y;
    }

    void Start()
    {
        coordsTopDisplay.transform.parent.gameObject.SetActive(false);
    }
}
