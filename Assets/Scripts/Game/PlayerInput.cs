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
    
    private enum SwipeDirection
    {
        Left, Right
    }
    
    public static UnityAction PartsMatched;

    private void Start()
    {
        _halfNumberOfItems = (_headParts.childCount - 1) / 2;
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
        
        CheckAllPartsMatch(_halfNumberOfItems);
    }

    private void OnSwipeLeft(Transform container)
    {
        GameSoundsManager.Instance.PlaySwipeSound();
        
        var firstChild = container.GetChild(0);
        
        float currentPositionX = firstChild.position.x;
        float newPositionX = container.GetChild(container.childCount - 1).position.x;

        firstChild.position = new Vector3(newPositionX, 0f);

        for (int i = 1; i < container.childCount; i++)
        {
            ChangeItemPosition(container, i, ref newPositionX, ref  currentPositionX);
        }
        
        firstChild.transform.SetAsLastSibling();
    }

    private void OnSwipeRight(Transform container)
    {
        GameSoundsManager.Instance.PlaySwipeSound();
        
        var lastChildIndex = container.childCount - 1;
        var lastChild = container.GetChild(lastChildIndex);
        
        float currentPositionX = lastChild.position.x;
        float newPositionX = container.GetChild(0).position.x;

        lastChild.position = new Vector3(newPositionX, 0f);

        for (int i = lastChildIndex - 1; i >= 0; i--)
        {
            ChangeItemPosition(container, i, ref newPositionX, ref  currentPositionX);
        }
        
        lastChild.transform.SetAsFirstSibling();
    }

    private void ChangeItemPosition(Transform container, int index, ref float newPositionX, ref float currentPositionX)
    {
        newPositionX = currentPositionX;
        currentPositionX = container.GetChild(index).position.x;
        container.GetChild(index).position = new Vector3(newPositionX, 0f);
    }
    
    private void CheckAllPartsMatch(int halfNumberOfItems)
    {
        int headItemID = _headParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;

        if (headItemID == bodyItemID && headItemID == legsItemID)
        {
            PartsMatched?.Invoke();
            Debug.Log("parts matched");
        }
    }
}
