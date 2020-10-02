﻿using System.Collections;
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
    private GameObject dragItem;

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

        if(Input.GetMouseButtonUp(0) && isDropZone)
        {
            DropItem();
        }
    }

    void Masa()
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

        AddSpoon(1);
    }

    void Filling()
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

            state = GameState.Masa;
            //husk = images[3];
            Debug.Log("add it");
            StartCoroutine(NextTamale());
        }

        AddSpoon(2);
    }

    void Fold()
    {
        if (state != GameState.Fold)
        {
            Debug.Log("wrong :(");
        }
        else
        {
            state = GameState.Masa;
            //husk = images[3];
            Debug.Log("add it");
            StartCoroutine(NextTamale());
        }
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
        if (isDropZone)
        {
            switch (dragItem.tag)
            {
                case "Masa":
                    Masa();
                    break;
                case "Filling":
                    Filling();
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

    public void RemoveSpoon(int type)
    {
        if (type == 1)
        {
            masa.gameObject.SetActive(false);
            masaSpoon.gameObject.SetActive(false);
        }
        else if (type == 2)
        {
            filling.gameObject.SetActive(false);
            fillingSpoon.gameObject.SetActive(false);
        }
    }

    void AddSpoon(int type)
    {
        if (type == 1)
        {
            masa.gameObject.SetActive(true);
            masaSpoon.gameObject.SetActive(true);
        }
        else if (type == 2)
        {
            filling.gameObject.SetActive(true);
            fillingSpoon.gameObject.SetActive(true);
        }
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
