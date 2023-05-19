using UnityEngine;
using UnityEngine.Events;


public class SelectScript : MonoBehaviour
{
    public enum SelectState
    {
        Unselected,
        Highlighted,
        Selected,
    }

    [Header("Object State Info")]
    public SelectState currentSelectState;

    [SerializeField] UnityEvent TriggerWhenObjectIsSelected; 
    [SerializeField] UnityEvent TriggerWhenObjectIsUnselected;
    [SerializeField] UnityEvent TriggerWhenObjectIsHighlighted;
    [SerializeField] UnityEvent TriggerWhenObjectIsUnhighlighted;

    public void HighlightObject()
    {
        // guard for if state is anything other than unselected  
        if(currentSelectState != SelectState.Unselected) { return; }

        // Set state to highlighted 
        currentSelectState = SelectState.Highlighted;
        TriggerWhenObjectIsHighlighted.Invoke();
    }

    public void UnhighlightObject()
    {
        // Guard for if object is selected 
        if (currentSelectState == SelectState.Selected) {return; }
        // if not selected then set state to unselected 
        currentSelectState = SelectState.Unselected;

        // Unhighlight Object
        if(TriggerWhenObjectIsUnhighlighted != null)
        {
            TriggerWhenObjectIsUnhighlighted.Invoke();

        }
    }

    public void SelectObject()
    {
        // Guard for if object is selected 
        if (currentSelectState == SelectState.Selected) { return; }

        // if not selected then set state to unselected 
        currentSelectState = SelectState.Selected;

        //Invoke "SelectObj()" Event in Editor
        TriggerWhenObjectIsSelected.Invoke();
        
    }

    public void UnselectObject()
    {
        // Guard for if object is selected 
        if (currentSelectState != SelectState.Selected) { return; }

        // if not selected then set state to unselected 
        currentSelectState = SelectState.Unselected;

        // Unselect Object
        TriggerWhenObjectIsUnselected.Invoke();
    }
}
