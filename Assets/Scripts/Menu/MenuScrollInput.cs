using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MenuScrollInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _container;
    
    [SerializeField] private float _duration = 0.5f;
    
    private float[] _positions;
    private float _distance;

    private float _startPosition;
    private float _endPosition;
    
    private void Start()
    {
        _positions = new float[_container.childCount];
        _distance = 1f / (_positions.Length - 1f);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }
        
        _scrollRect.horizontalNormalizedPosition = _positions[GameDataManager.GetSelectedCategoryID()];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _scrollRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endPosition = _scrollRect.horizontalNormalizedPosition;

        if (_endPosition > _startPosition)
        {
            SwipePanelLeft(_endPosition);
        }
        else
        {
            SwipePanelRight(_endPosition);
        }
    }

    private void SwipePanelLeft(float endPosition)
    {
        var endValue = FindClosestPositionRight(endPosition);
        
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration);
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

    private void SwipePanelRight(float endPosition)
    {
        var endValue = FindClosestPositionLeft(endPosition);

        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration);
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
    
}
