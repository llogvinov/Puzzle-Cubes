using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SliderInpt : MonoBehaviour
{
    [SerializeField] private TouchSlider _slider;
    
    [Space] 
    [SerializeField] private Transform _container;
    
    private bool _isPointerDown;
    private float _startPos;
    private float _endPos;
    
    public static UnityAction<Transform> SwipedLeft;
    public static UnityAction<Transform> SwipedRight;
    public static UnityAction Swiped;

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

    private void OnPointerDown(float xStart)
    {
        _isPointerDown = true;

        _startPos = xStart;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (_isPointerDown)
        {
            _container.position = new Vector3(xMovement * 5f, 0f, 0f);
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
        if (_endPos > _startPos)
        {
            Swiped?.Invoke();
            SwipedRight?.Invoke(_container);
        }
        else
        {
            Swiped?.Invoke();
            SwipedLeft?.Invoke(_container);
        }
    }
}