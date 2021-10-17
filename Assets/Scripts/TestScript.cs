using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Transform _currentContainer;
    
    [SerializeField] private Transform _headsContainer;
    [SerializeField] private Transform _bodiesContainer;
    [SerializeField] private Transform _legsContainer;

    [Space]
    [SerializeField] private float _moveTime = 0.3f;
    
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;

    private int _middleChildIndex;

    public static UnityAction Swiped;
    public static UnityAction PlayerWon;
    public static UnityAction<int> ItemCollected;

    private void Start()
    {
        _middleChildIndex = _headsContainer.childCount / 2;
        
        _leftPosition = _headsContainer.GetChild(0).position;
        _rightPosition = _headsContainer.GetChild(_headsContainer.childCount - 1).position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Swiped?.Invoke();
            MoveContainerLeft(_currentContainer);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Swiped?.Invoke();
            MoveContainerRight(_currentContainer);
        }
    }

    private void MoveContainerLeft(Transform container)
    {
        container
            .DOMoveX(-GameUIGenerator.SpriteWidth, _moveTime)
            .OnComplete(() => OnCompleteLeft(container));
    }

    private void MoveContainerRight(Transform container)
    {
        container
            .DOMoveX(GameUIGenerator.SpriteWidth, _moveTime)
            .OnComplete(() => OnCompleteRight(container));
    }
    
    private void OnCompleteLeft(Transform container)
    {
        PlaceContainerBack(container);
        AdjustContainerPartsLeft(container);
        CheckAllPartsMatch(_middleChildIndex);
    }
    
    private void OnCompleteRight(Transform container)
    {
        PlaceContainerBack(container);
        AdjustContainerPartsRight(container);
        CheckAllPartsMatch(_middleChildIndex);
    }

    private void PlaceContainerBack(Transform container)
    {
        container.position = Vector3.zero;
    }

    private void AdjustContainerPartsLeft(Transform container)
    {
        for (int i = 1; i < container.childCount; i++)
        {
            container.GetChild(i).position -= new Vector3(GameUIGenerator.SpriteWidth, 0f, 0f);
        }
        
        var firstChild = container.GetChild(0);
        firstChild.position = _rightPosition;

        firstChild.SetAsLastSibling();
    }
    
    private void AdjustContainerPartsRight(Transform container)
    {
        for (int i = 0; i < container.childCount - 1; i++)
        {
            container.GetChild(i).position += new Vector3(GameUIGenerator.SpriteWidth, 0f, 0f);
        }
        
        var lastChild = container.GetChild(container.childCount - 1);
        lastChild.position = _leftPosition;
        
        lastChild.SetAsFirstSibling();
    }
    
    public void CheckAllPartsMatch(int index)
    {
        int headItemID = _headsContainer.GetChild(index).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodiesContainer.GetChild(index).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsContainer.GetChild(index).GetComponent<ItemPart>().ItemID;

        if (headItemID != bodyItemID || headItemID != legsItemID) 
            return;
        
        PlayerWon?.Invoke();
        ItemCollected?.Invoke(headItemID);
        Debug.Log("parts matched");
    }
}
