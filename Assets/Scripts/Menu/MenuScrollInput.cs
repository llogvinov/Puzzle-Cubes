using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class MenuScrollInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _container;
    
    [SerializeField] private float _duration = 0.5f;

    private Transform[] _allButtons;
    
    private float[] _positions;
    private float _distance;

    private float _startPosition;
    private float _endPosition;
    
    private void Start()
    {
        AdjustButtons();
        AdjustPositions();

        GetScrollRectToMiddle();
    }

    private void AdjustButtons()
    {
        _allButtons = new Transform[_container.childCount];

        for (int i = 0; i < _allButtons.Length; i++)
        {
            _allButtons[i] = _container.GetChild(i);
        }
    }

    private void AdjustPositions()
    {
        _positions = new float[_container.childCount];
        _distance = 1f / (_positions.Length - 1f);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _scrollRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endPosition = _scrollRect.horizontalNormalizedPosition;

        if (_endPosition > _startPosition)
            SwipePanelLeft(_endPosition);
        else
            SwipePanelRight(_endPosition);
    }

    private void SwipePanelLeft(float endPosition)
    {
        var endValue = FindClosestPositionRight(endPosition);
        var difference = Mathf.RoundToInt(Mathf.Abs(endValue - _positions[3]) / _distance);
        
        MenuSoundsManager.Instance.PlaySwipeSound();
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(() => OnSwipedLeft(difference));
    }

    private void SwipePanelRight(float endPosition)
    {
        var endValue = FindClosestPositionLeft(endPosition);
        var difference = Mathf.RoundToInt(Mathf.Abs(endValue - _positions[3]) / _distance);

        MenuSoundsManager.Instance.PlaySwipeSound();
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(() => OnSwipedRight(difference));
    }
    
    private float FindClosestPositionRight(float endPosition)
    {
        float result = 1f;

        foreach (var position in _positions)
        {
            if (position < endPosition) 
                continue;
            
            result = position;
            break;
        }

        return result;
    }

    private float FindClosestPositionLeft(float endPosition)
    {
        int index = 0;

        for (int i = 0; i < _positions.Length; i++)
        {
            index = i;

            if (_positions[i] > endPosition)
                break;
        }

        return index == 0 ? _positions[index] : _positions[index - 1];
    }
    
    private void OnSwipedLeft(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            var firstChild = _container.GetChild(0);
        
            firstChild.transform.SetAsLastSibling();
        
            GetScrollRectToMiddle();
            ShakeButtons();
        }
    }

    private void OnSwipedRight(int difference)
    {
        for (int i = 0; i < difference; i++)
        {
            var lastChildIndex = _container.childCount - 1;
            var lastChild = _container.GetChild(lastChildIndex);
        
            lastChild.transform.SetAsFirstSibling();

            GetScrollRectToMiddle();
            ShakeButtons();
        }
    }

    private void GetScrollRectToMiddle()
    {
        _scrollRect.horizontalNormalizedPosition = _positions[3];
    }

    private void ShakeButtons()
    {
        foreach (var button in _allButtons)
        {
            button.DOShakeScale(_duration * 2f, 0.03f);
        }
    }
}
