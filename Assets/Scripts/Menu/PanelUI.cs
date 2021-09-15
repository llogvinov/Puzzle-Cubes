using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [SerializeField] protected GameObject _background;
    [SerializeField] protected GameObject _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] protected Button[] _buttonsToHide;

    [SerializeField] private int _panelID;

    public int PanelID => _panelID;

    private const float LeanTime = 0.3f;
    
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
        _background.SetActive(true);
        _panelUI.SetActive(true);

        var background = _background.GetComponent<CanvasGroup>();
        background.alpha = 0;
        background.LeanAlpha(1, LeanTime);

        var panel = _panelUI.GetComponent<Transform>();
        panel.localPosition = new Vector2(0, Screen.height);
        panel.LeanMoveLocalY(0, LeanTime).setEaseOutExpo();
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
        ShowButtons();
    }

    protected void HidePanel()
    {
        var background = _background.GetComponent<CanvasGroup>();
        background.LeanAlpha(1, LeanTime).setOnComplete(() => _background.SetActive(false));

        var panel = _panelUI.GetComponent<Transform>();
        panel.LeanMoveLocalY(Screen.height, LeanTime)
            .setEaseInExpo()
            .setOnComplete(() => _panelUI.SetActive(false));
    }

    private void ShowButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
}
