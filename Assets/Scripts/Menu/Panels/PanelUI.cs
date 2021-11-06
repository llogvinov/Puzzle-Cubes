using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _background;
    [SerializeField] protected Transform _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] protected Button[] _buttonsToHide;

    [SerializeField] private int _panelID;
    
    public int PanelID => _panelID;

    private const float Duration = 0.3f;

    protected void AddPanelButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    protected void OnAgeConfirmed()
    {
        if (_panelID != CheckAgeUI.CurrentPanelID)
            return;

        ShowPanel();
    }

    protected void ShowPanel()
    {
        _panelUI.gameObject.SetActive(true);
        _panelUI.localPosition = new Vector2(0, Screen.height);
        
        _panelUI
            .DOLocalMoveY(0f, Duration)
            .SetEase(Ease.OutExpo);
    }

    protected void ShowBackground()
    {
        _background.gameObject.SetActive(true);
        _background.alpha = 0;
        
        _background.DOFade(1f, Duration);
    }

    protected void HideButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void ClosePanelUI()
    {
        MenuSoundsManager.Instance.PlayClickedSound();

        HidePanel();
        HideBackground();
        ShowButtons();
    }

    protected void HidePanel()
    {
        _panelUI
            .DOLocalMoveY(-Screen.height, Duration)
            .SetEase(Ease.InExpo)
            .OnComplete(() => _panelUI.gameObject.SetActive(false));
    }

    protected void HideBackground()
    {
        _background
            .DOFade(1f, Duration)
            .OnComplete(() => _background.gameObject.SetActive(false));
    }

    protected void ShowButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
}
