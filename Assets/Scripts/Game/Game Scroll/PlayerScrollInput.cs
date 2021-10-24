using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerScrollInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _container;
    [SerializeField] private WinCheckerScroll _winCheckerScroll;

    [SerializeField] private float _duration = 0.3f;
    
    private float[] _positions;
    private float _distance;
    
    private float _startPosition;
    private float _endPosition;
    
    public static UnityAction Swiped;
    
    private void Start()
    {
        _positions = new float[_container.childCount];
        _distance = 1f / (_positions.Length - 1f);

        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _distance * i;
        }

        GetScrollRectToMiddle();
    }
    
    private void GetScrollRectToMiddle()
    {
        _scrollRect.horizontalNormalizedPosition = _positions[2];
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
            SwipePanelLeft();
        }
        else
        {
            SwipePanelRight();
        }
    }

    private void SwipePanelLeft()
    {
        var endValue = _positions[3];
        
        Swiped?.Invoke();
        
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(OnSwipedLeft);
    }
    
    private void SwipePanelRight()
    {
        var endValue = _positions[1];
        
        Swiped?.Invoke();
        
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(OnSwipedRight);
    }

    private void OnSwipedLeft()
    {
        var firstChild = _container.GetChild(0);
        
        firstChild.transform.SetAsLastSibling();
        
        GetScrollRectToMiddle();
        _winCheckerScroll.CheckAllPartsMatch(2);
    }

    private void OnSwipedRight()
    {
        var lastChildIndex = _container.childCount - 1;
        var lastChild = _container.GetChild(lastChildIndex);
        
        lastChild.transform.SetAsFirstSibling();

        GetScrollRectToMiddle();
        _winCheckerScroll.CheckAllPartsMatch(2);
    }
}
