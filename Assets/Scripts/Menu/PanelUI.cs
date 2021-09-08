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

    [SerializeField] protected MenuSoundsManager _soundsManager;

    public int PanelID => _panelID;

    private void OnEnable() => CheckAgeUI.AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => CheckAgeUI.AgeConfirmed -= OnAgeConfirmed;
    
    protected void AddPanelButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    private void OnAgeConfirmed()
    {
        if (_panelID != CheckAgeUI.PanelID)
            return;
        
        ShowPanel();
        // HideButtons();
    }

    private void ShowPanel()
    {
        _background.SetActive(true);
        _panelUI.SetActive(true);
    }

    private void HideButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void ClosePanelUI()
    {
        _soundsManager.PlayClickedSound();
        
        HidePanel();
        ShowButtons();
    }

    private void HidePanel()
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
