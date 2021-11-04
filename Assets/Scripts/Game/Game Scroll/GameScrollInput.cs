using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class GameScrollInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _container;
    [SerializeField] private WinCheckerScroll _winCheckerScroll;

    [SerializeField] private float _duration = 0.3f;
    
    private float _startPosition;
    private float _endPosition;
    
    public static UnityAction Swiped;
    
    private void Start()
    {
        GetScrollRectToMiddle();
    }
    
    private void GetScrollRectToMiddle()
    {
        _scrollRect.horizontalNormalizedPosition = GameScrollUI.Positions[GameScrollUI.MiddlePositionIndex];
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
        float endValue = GameScrollUI.Positions[GameScrollUI.MiddlePositionIndex + 1];
        
        Swiped?.Invoke();
        
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(OnSwipedLeft);
    }
    
    private void SwipePanelRight()
    {
        float endValue = GameScrollUI.Positions[GameScrollUI.MiddlePositionIndex - 1];
        
        Swiped?.Invoke();
        
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(OnSwipedRight);
    }

    private void OnSwipedLeft()
    {
        Transform firstChild = _container.GetChild(0);
        
        firstChild.transform.SetAsLastSibling();
        
        OnSwipeEnded();
    }

    private void OnSwipedRight()
    {
        int lastChildIndex = _container.childCount - 1;
        Transform lastChild = _container.GetChild(lastChildIndex);
        
        lastChild.transform.SetAsFirstSibling();

        OnSwipeEnded();
    }

    private void OnSwipeEnded()
    {
        GetScrollRectToMiddle();
        _winCheckerScroll.CheckAllPartsMatch(GameScrollUI.MiddlePositionIndex);
    }
}
