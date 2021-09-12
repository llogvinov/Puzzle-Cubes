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
        _background.SetActive(false);
        _panelUI.SetActive(false);
    }

    private void ShowButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
}
