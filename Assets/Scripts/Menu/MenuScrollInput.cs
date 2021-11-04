using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MenuScrollInput : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _container;
    [SerializeField] private float _duration;

    private Transform[] _allButtons;

    private float _startPosition;
    private float _endPosition;
    
    private void Start()
    {
        AdjustButtons();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _scrollRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endPosition = _scrollRect.horizontalNormalizedPosition;

        if (_endPosition > _startPosition - MenuScrollUI.Distance / 3.5f 
            && _endPosition < _startPosition + MenuScrollUI.Distance / 3.5f)
            ResetPanel();
        else if (_endPosition > _startPosition)
            SwipePanelLeft(_endPosition);
        else
            SwipePanelRight(_endPosition);
    }

    private void ResetPanel()
    {
        _scrollRect.DOHorizontalNormalizedPos(MenuScrollUI.MiddlePosition, _duration / 2f);
    }
    
    public void SwipePanelLeft(float endPosition)
    {
        float endValue = FindClosestPositionRight(endPosition);
        int difference = Mathf.RoundToInt(Mathf.Abs(endValue - MenuScrollUI.MiddlePosition) / MenuScrollUI.Distance);
        
        MenuSoundsManager.Instance.PlaySwipeSound();
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(() => OnSwipedLeft(difference));
    }

    public void SwipePanelRight(float endPosition)
    {
        float endValue = FindClosestPositionLeft(endPosition);
        int difference = Mathf.RoundToInt(Mathf.Abs(endValue - MenuScrollUI.MiddlePosition) / MenuScrollUI.Distance);

        MenuSoundsManager.Instance.PlaySwipeSound();
        _scrollRect
            .DOHorizontalNormalizedPos(endValue, _duration)
            .OnComplete(() => OnSwipedRight(difference));
    }
    
    private float FindClosestPositionRight(float endPosition)
    {
        float result = 1f;

        foreach (var position in MenuScrollUI.Positions)
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

        for (int i = 0; i < MenuScrollUI.Positions.Length; i++)
        {
            index = i;

            if (MenuScrollUI.Positions[i] > endPosition)
                break;
        }

        return index == 0 ? MenuScrollUI.Positions[index] : MenuScrollUI.Positions[index - 1];
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
        _scrollRect.horizontalNormalizedPosition = MenuScrollUI.MiddlePosition;
    }

    private void ShakeButtons()
    {
        /*
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(_container.DOLocalMoveX(_container.transform.position .x + 2f, _duration / 4f))
            .Append(_container.DOLocalMoveX(_container.transform.position .x - 4f, _duration / 4f))
            .Append(_container.DOLocalMoveX(_container.transform.position .x + 2f, _duration / 4f));
        */
        
        foreach (var button in _allButtons)
        {
            // button.DOShakeScale(_duration * 2f, 0.03f);
        }
    }
}
