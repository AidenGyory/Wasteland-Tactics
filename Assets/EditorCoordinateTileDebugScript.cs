using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorCoordinateTileDebugScript : MonoBehaviour
{

    [SerializeField] TMP_Text coordsDisplay; 

    // Start is called before the first frame update
    public void DisplayCoords(int x, int y)
    {
        coordsDisplay.text = "" + x + "," + y;
    }

    // Update is called once per frame
    void Start()
    {
        coordsDisplay.transform.parent.gameObject.SetActive(false);
    }
}
