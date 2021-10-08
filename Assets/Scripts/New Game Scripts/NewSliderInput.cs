using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewSliderInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Transform _container;    
    
    private float _startValue;
    private float[] _positions;
    private float _distance;

    private int _middleChildIndex = 2;

    public static UnityAction Swiped;

    private void Start()
    {
        CountPositions();
        
        _scrollbar.value = 0.5f;
        _startValue = _scrollbar.value;
    }

    private void CountPositions()
    {
        _positions = new float[_container.childCount];
        _distance = 1f / (_positions.Length - 1);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");

        _startValue = _scrollbar.value;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (_scrollbar.value > _startValue)
            OnSwipeLeft();
        else
            OnSwipeRight();
    }
    
    private void OnSwipeLeft()
    {
        var firstChild = _container.GetChild(0);

        _scrollbar.value =
            Mathf.Lerp(_scrollbar.value, .25f, 0.1f);
        
        firstChild.transform.SetAsLastSibling();
        _scrollbar.value = _positions[_middleChildIndex];
                
        Swiped?.Invoke();
    }

    private void OnSwipeRight()
    {
        var lastChildIndex = _container.childCount - 1;
        var lastChild = _container.GetChild(lastChildIndex);

        _scrollbar.value = 
            Mathf.Lerp(_scrollbar.value, .75f, 0.1f);
        
        lastChild.transform.SetAsFirstSibling();
        _scrollbar.value = _positions[_middleChildIndex];
        
        Swiped?.Invoke();
    }

}
