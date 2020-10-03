using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum GameState
{
    Fold, Filling, Masa
}

public class DragAndDrop : MonoBehaviour
{
    private GameObject dragItem = null;

    public Sprite[] images;
    public Image husk;
    public bool isDropZone = false;
    public Texture2D open, closed;
    public Texture2D[] hands;
    public GameState state = GameState.Masa;

    //Images to remove masa/filling spoon
    public Image masa, masaSpoon;
    public Image filling, fillingSpoon;

    public Animator tray;
    public Animator tamale;


    private void Start()
    {
        tray.GetComponent<Animator>();
        tamale.GetComponent<Animator>();
        //  StartCoroutine(NextTamale());

    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateHand(closed);
        }
        else
        {
            UpdateHand(open);
        }

        if (Input.GetMouseButtonUp(0))
        {
            closed = hands[0];

            if (dragItem != null)
                DropItem();

            AddSpoon();
        }
    }

    void Masa()
    {
        if (isDropZone)
        {
            if (state != GameState.Masa)
            {
                Debug.Log("wrong :(");
            }
            else
            {
                state = GameState.Filling;
                husk.sprite = images[1];
                Debug.Log("add it");
            }
        }


        AddSpoon();
    }

    void Filling()
    {

        if (isDropZone)
        {
            if (state != GameState.Filling)
            {
                Debug.Log("wrong :(");
            }
            else
            {
                // state = GameState.Fold;
                husk.sprite = images[2];
                Debug.Log("add it");

                state = GameState.Fold;
                husk.sprite = images[3];
                Debug.Log("add it");
                StartCoroutine(NextTamale());
            }
        }


        AddSpoon();
    }

    IEnumerator NextTamale()
    {
        tray.SetTrigger("Show");

        yield return new WaitForSecondsRealtime(1.5f);

        tamale.SetTrigger("Slide");
        yield return new WaitForSecondsRealtime(1.5f);

        tray.SetTrigger("Hide");
        tamale.SetTrigger("Hide");

        yield return new WaitForSecondsRealtime(1f);
        husk.sprite = images[0];
        tamale.SetTrigger("New");

        yield return new WaitForSecondsRealtime(1.5f);
        AddSpoon();
        state = GameState.Masa;

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
        Debug.Log("here");
        switch (dragItem.tag)
        {
            case "Masa":
                Masa();
                break;
            case "Filling":
                Filling();
                break;
            default:
                Debug.LogError("Uhhhhh");
                break;
        }

        dragItem = null;
        AddSpoon();
    }

    public void RemoveDragItem()
    {
        if (!Input.GetMouseButton(0)){
            dragItem = null;
            //Resetting closed hand sprite to be empty when mouse exits the bowl area
            closed = hands[0];
        }
    }

    //Sets the game Object to be dragged
    public void SetDragItem(GameObject item)
    {
        if (dragItem == null)
        {
            dragItem = item;

            switch (dragItem.tag)
            {
                case "Masa":
                    closed = hands[1];
                    break;
                case "Filling":
                    closed = hands[2];
                    break;
                default:
                    closed = hands[0];
                    break;
            }
        }

    }

    public void RemoveSpoon(int type)
    {
        //Disabling both prevents issues with hovering over while already holding a spoon
        masa.gameObject.SetActive(false);
        filling.gameObject.SetActive(false);

        if (type == 1)
        {
            masaSpoon.gameObject.SetActive(false);
        }
        else if (type == 2)
        {
            fillingSpoon.gameObject.SetActive(false);
        }
    }

    void AddSpoon()
    {
        masa.gameObject.SetActive(true);
        masaSpoon.gameObject.SetActive(true);
        filling.gameObject.SetActive(true);
        fillingSpoon.gameObject.SetActive(true);

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
