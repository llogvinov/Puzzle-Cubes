using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;

    [SerializeField] private ItemPart _itemPart;
    
    private int _halfNumberOfItems;
    private float _spriteWidth;
    
    private enum SwipeDirection
    {
        Left, Right
    }

    private SwipeDirection _swipeDirection;
    
    public static UnityAction PartsMatched;

    private void Start()
    {
        _halfNumberOfItems = (_headParts.childCount - 1) / 2;
        _spriteWidth = _itemPart.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.H))
                OnContainerSwiped(_headParts, SwipeDirection.Left);
            else if (Input.GetKey(KeyCode.B))
                OnContainerSwiped(_bodyParts, SwipeDirection.Left);
            else if (Input.GetKey(KeyCode.L))
                OnContainerSwiped(_legsParts, SwipeDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.H))
                OnContainerSwiped(_headParts, SwipeDirection.Right);
            else if (Input.GetKey(KeyCode.B))
                OnContainerSwiped(_bodyParts, SwipeDirection.Right);
            else if (Input.GetKey(KeyCode.L))
                OnContainerSwiped(_legsParts, SwipeDirection.Right);
        }
    }
    
    private void OnContainerSwiped(Transform container, SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case SwipeDirection.Left:
                OnSwipeLeft(container);
                break;
            case SwipeDirection.Right:
                OnSwipeRight(container);
                break;
        }
    }

    private void OnSwipeLeft(Transform container)
    {
        Debug.Log(container.name + " left");

        float currentPositionX = container.GetChild(0).position.x;
        float newPositionX = container.GetChild(container.childCount - 1).position.x;

        container.GetChild(0).position = new Vector3(newPositionX, 0f);

        for (int i = 1; i < container.childCount; i++)
        {
            newPositionX = currentPositionX;
            currentPositionX = container.GetChild(i).position.x;
            container.GetChild(i).position = new Vector3(newPositionX, 0f);
        }
    }

    private void OnSwipeRight(Transform container)
    {
        Debug.Log(container.name + " right");
        
        float currentPositionX = container.GetChild(container.childCount - 1).position.x;
        float newPositionX = container.GetChild(0).position.x;

        container.GetChild(container.childCount - 1).position = new Vector3(newPositionX, 0f);

        for (int i = container.childCount - 1 - 1; i >= 0; i--)
        {
            newPositionX = currentPositionX;
            currentPositionX = container.GetChild(i).position.x;
            container.GetChild(i).position = new Vector3(newPositionX, 0f);
        }
    }
    
    private void CheckAllPartsMatch(int halfNumberOfItems)
    {
        int headItemID = _headParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;

        if (headItemID == bodyItemID && headItemID == legsItemID)
        {
            PartsMatched?.Invoke();
        }
    }
}
