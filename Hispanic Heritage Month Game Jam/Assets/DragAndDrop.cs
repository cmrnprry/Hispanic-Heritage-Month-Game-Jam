using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private GameObject dragItem;
    public bool isDropZone = false;

    //Drags the clicked item
    public void DragItem()
    {
        dragItem.transform.position = Input.mousePosition;
    }

    public void DropItem()
    {
        if (isDropZone)
        {
            switch (dragItem.tag)
            {
                case "Masa":
                    break;
                case "Filling":
                    break;
                case "Husk":
                    break;
                default:
                    Debug.LogError("Uhhhhh");
                    break;
            }
        }
    }

    //Sets the game Object to be dragged
    public void SetDragItem(GameObject item)
    {
        dragItem = item;
    }

    public void EnterDropZone()
    {
        isDropZone = true;
    }

    public void ExitDropZone()
    {
        isDropZone = false;
    }
}
