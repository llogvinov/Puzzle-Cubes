using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] private Button[] _buttonsToHide;
    
    private void OnEnable() => CheckAgeUI.AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => CheckAgeUI.AgeConfirmed -= OnAgeConfirmed;
    
    private void Start()
    {
        AddButtonsEvents();
    }
    
    private void AddButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    private void OnAgeConfirmed()
    {
        ShowPanel();
        HideButtons();
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
