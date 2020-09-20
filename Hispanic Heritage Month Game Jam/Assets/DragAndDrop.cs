using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private GameObject dragItem;

    //Sets the game Object to be dragged
    public void SetDragItem(GameObject item)
    {
        dragItem = item;
    }

    //Drags the clicked item
    public void DragItem()
    {
        dragItem.transform.position = Input.mousePosition;
    }
}
