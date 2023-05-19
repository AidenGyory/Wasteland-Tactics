using UnityEditor;
using UnityEngine;

public class SelectObjectScript : MonoBehaviour
{
    public static SelectObjectScript Instance;

    public bool canSelect;

    public SelectScript highlightedObject;
    public SelectScript selectedObject;

    //Raycast for Object Selection
    private Ray _ray;
    private RaycastHit _hit;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (canSelect)
        {
            //Raycast down to Selectable objects in scene 
            RayCastToObjects();

            if(Input.GetMouseButtonDown(0)) 
            {
                AttemptToSelectObject(); 
            }
        }

    }
    void RayCastToObjects()
    {
        // Ray equals the screen to point value of the screen to the mouse pointer 
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // CAST a ray out till it hits an object collider 
        if (Physics.Raycast(_ray, out _hit))
        {
            //Guard for invalid raycast 
            if (_hit.transform.GetComponent<SelectScript>() == null && highlightedObject != null)
            {
                if (highlightedObject != null)
                {
                    highlightedObject.UnhighlightObject();
                    highlightedObject = null;
                }
                return;
            }
            else // _hit.transform.GetComponent<SelectScript>() == "something" 
            {
                //Guard for if raycast hits already highlighted object
                if (highlightedObject == _hit.transform.GetComponent<SelectScript>()) { return; }

                //Unhlight the current highlighted object
                if (highlightedObject != null)
                {
                    highlightedObject.UnhighlightObject();
                }

                // Highlighted object now equals the new raycast target 
                highlightedObject = _hit.transform.GetComponent<SelectScript>();
                highlightedObject.HighlightObject();
            }
        }

    }

    void AttemptToSelectObject()
    {
        if(selectedObject != null)
        {
            selectedObject.GetComponent<SelectScript>().UnselectObject();
            selectedObject = null;
        }

        if (highlightedObject != null)
        {
            selectedObject = highlightedObject;
            selectedObject.GetComponent<SelectScript>().SelectObject();
        }

        

    }
}
