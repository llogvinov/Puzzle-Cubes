using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInpt : MonoBehaviour
{
    private enum SwipeDirection
    {
        Left, Right
    }

    [SerializeField] private PlayerInput _playerInput;
    
    [SerializeField] private TouchSlider _slider;
    
    [Space] 
    [SerializeField] private Transform _container;
    
    private bool _isPointerDown;

    private float _startPos;
    private float _endPos;
    
    private int _halfNumberOfItems;

    private void OnEnable()
    {
        _slider.OnPointerDownEvent += OnPointerDown;
        _slider.OnPointerDragEvent += OnPointerDrag;
        _slider.OnPointerUpEvent += OnPointerUp;
    }

    private void OnDisable()
    {
        _slider.OnPointerDownEvent -= OnPointerDown;
        _slider.OnPointerDragEvent -= OnPointerDrag;
        _slider.OnPointerUpEvent -= OnPointerUp;
    }

    private void Start()
    {
        _halfNumberOfItems = (_container.childCount - 1) / 2;
    }
    
    private void OnPointerDown(float xStart)
    {
        _isPointerDown = true;

        _startPos = xStart;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (_isPointerDown)
        {
            _container.position = new Vector3(xMovement, 0f, 0f);
        }
    }

    private void OnPointerUp(float xEnd)
    {
        if (!_isPointerDown) 
            return;
        
        _isPointerDown = false;
        _endPos = xEnd;
        CheckDirection();
    }

    private void CheckDirection()
    {
        if (Mathf.Abs(_endPos - _startPos) < 0.2f)
            return;
        
        if (_endPos > _startPos)
        {
            Debug.Log("Right");
            OnContainerSwiped(_container, SwipeDirection.Right);
        }
        else
        {
            Debug.Log("Left");
            OnContainerSwiped(_container, SwipeDirection.Left);
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

        _container.position = Vector3.zero;

        // check parts matched
        _playerInput.CheckAllPartsMatch(_halfNumberOfItems);
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
}


