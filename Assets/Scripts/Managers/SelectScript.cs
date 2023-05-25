using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class SelectScript : MonoBehaviour
{
    public enum SelectState
    {
        Unselected,
        Highlighted,
        Selected,
    }

    [SerializeField] private bool canOutline;
    
    [Header("Object State Info")]
    public SelectState currentSelectState;

    [SerializeField] private UnityEvent triggerWhenObjectIsSelected; 
    [SerializeField] private UnityEvent triggerWhenObjectIsUnselected;
    [SerializeField] private UnityEvent triggerWhenObjectIsHighlighted;
    [SerializeField] private UnityEvent triggerWhenObjectIsNoLongerHighlighted;
    
    private GameObject _outline;


    private void Start()
    {
        if (!canOutline) {return;}

        transform.GetComponentInChildren<OutlineCreatorScript>().enabled = true;
    }

    public void HighlightObject()
    {
        // guard for if state is anything other than unselected  
        if(currentSelectState != SelectState.Unselected) { return; }

        // Set state to highlighted 
        currentSelectState = SelectState.Highlighted;
        triggerWhenObjectIsHighlighted.Invoke();

        _outline.SetActive(true); 
    }

    public void UnhighlightObject()
    {
        // Guard for if object is selected 
        if (currentSelectState == SelectState.Selected) {return; }
        // if not selected then set state to unselected 
        currentSelectState = SelectState.Unselected;

        // Unhighlight Object
        if(triggerWhenObjectIsNoLongerHighlighted != null)
        {
            triggerWhenObjectIsNoLongerHighlighted.Invoke();

        }
        _outline.SetActive(false);
    }

    public void SelectObject()
    {
        // Guard for if object is selected 
        if (currentSelectState == SelectState.Selected) { return; }

        // if not selected then set state to unselected 
        currentSelectState = SelectState.Selected;

        //Invoke "SelectObj()" Event in Editor
        triggerWhenObjectIsSelected.Invoke();
        
    }

    public void UnselectObject()
    {
        // Guard for if object is selected 
        if (currentSelectState != SelectState.Selected) { return; }

        // if not selected then set state to unselected 
        currentSelectState = SelectState.Unselected;

        // Unselect Object
        triggerWhenObjectIsUnselected.Invoke();
        
        _outline.SetActive(false);
    }

    public void SetOutline(GameObject outlineModel)
    {
        _outline = outlineModel; 
    }
}
