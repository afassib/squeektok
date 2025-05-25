using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEventHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public UnityEvent onSelected; // Assignable event in Inspector
    public GameObject selectionPrefab;

    public void Awake()
    {
        if(selectionPrefab != null)
        {
            selectionPrefab.SetActive(false);
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        if(selectionPrefab!=null)
            selectionPrefab.SetActive(true);
        Debug.Log("Button Selected: " + gameObject.name);
        onSelected?.Invoke(); // Calls all assigned methods
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (selectionPrefab != null) 
            selectionPrefab.SetActive(false);
    }
}