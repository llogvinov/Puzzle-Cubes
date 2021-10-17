using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Win : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;

    private bool _isPointerDown;
    
    public static UnityAction PanelSwiped;
    
    private void OnEnable()
    {
        PanelSwiped += OnPanelSwiped;
    }

    private void OnDisable()
    {
        PanelSwiped -= OnPanelSwiped;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isPointerDown = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isPointerDown)
            return;
        
        _isPointerDown = false;
        SwipePanel();
    }

    private void OnPanelSwiped()
    {
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void SwipePanel()
    {
        _scrollRect
            .DOHorizontalNormalizedPos(1f, 0.5f)
            .OnComplete(() => PanelSwiped?.Invoke());
    }
}