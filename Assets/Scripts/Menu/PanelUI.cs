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
    
    private void Start()
    {
        AddButtonsEvents();
    }
    
    private void AddButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }

    private void ClosePanelUI()
    {
        _background.SetActive(false);
        _panelUI.SetActive(false);
        
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
    
}
