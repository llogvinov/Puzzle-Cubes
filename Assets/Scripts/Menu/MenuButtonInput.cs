using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuButtonInput : MonoBehaviour
{
    [SerializeField] private MenuScrollInput _menuScrollInput;
    
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [Space] 
    [SerializeField] private float _animationDuration;
    [SerializeField] private int _loopsNumber;
    [SerializeField] private float _finalScale;
    
    private float _t;
    private float _tMax;
     
    private void Start()
    {
        _tMax = _animationDuration * _loopsNumber + 6;
        _t = _tMax / 2f;
    }
    
    private void Update()
    {
        _t += Time.deltaTime;
        
        if (_t >= _tMax)
        {
            ShakeArrows();
            _t = 0f;
        }
    }
    
    private void ShakeArrows()
    {
        foreach (var arrow in new [] {_leftArrow, _rightArrow })
        {
            arrow.transform
                .DOScale(_finalScale, _animationDuration)
                .SetLoops(_loopsNumber * 2, LoopType.Yoyo);
        }
    }

    public void OnLeftArrowClicked()
    {
        _t = 0f;
        _menuScrollInput.SwipePanelRight(MenuScrollUI.LeftPosition);
    }

    public void OnRightArrowClicked()
    {
        _t = 0f;
        _menuScrollInput.SwipePanelLeft(MenuScrollUI.RightPosition);
    }
}
