using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class WinScroll : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;

    [SerializeField] private float _duration = 0.3f;

    public static UnityAction PanelSwiped;
    
    private void OnEnable()
    {
        PanelSwiped += OnPanelSwiped;
    }

    private void OnDisable()
    {
        PanelSwiped -= OnPanelSwiped;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SwipePanel();
    }

    private void OnPanelSwiped()
    {
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void SwipePanel()
    {
        _scrollRect
            .DOHorizontalNormalizedPos(1f, _duration)
            .OnComplete(() => PanelSwiped?.Invoke());
    }
}