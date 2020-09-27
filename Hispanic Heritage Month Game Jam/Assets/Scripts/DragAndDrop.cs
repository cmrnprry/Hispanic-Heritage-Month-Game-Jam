using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum GameState
{
    Fold, Filling, Masa
}

public class DragAndDrop : MonoBehaviour
{
    private GameObject dragItem;
    public bool isDropZone = false;
    public Texture2D open, closed;
    public GameState state = GameState.Masa;

    private void Update()
    {
        switch (state)
        {
            case GameState.Masa:
                break;
            case GameState.Filling:
                break;
            case GameState.Fold:
                break;
            default:
                Debug.LogError("State is wrong");
                break;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("closed");
            UpdateHand(closed);
        }
        else
        {
            Debug.Log("open");

            UpdateHand(open);
        }
    }

    private void UpdateHand(Texture2D hand)
    {
        CursorMode mode = CursorMode.ForceSoftware;
        float xspot = hand.width / 4;
        float yspot = hand.height / 4;
        Vector2 hotSpot = new Vector2(xspot, yspot);
        Cursor.SetCursor(hand, hotSpot, mode);
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

    //Drags the clicked item
    public void DragItem()
    {
        dragItem.transform.position = Input.mousePosition;
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
