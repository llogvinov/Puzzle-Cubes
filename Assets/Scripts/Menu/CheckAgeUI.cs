using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckAgeUI : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] private Button[] _buttonsToHide;

    private string _age;
    private const int MinNecessaryAge = 16;

    public static UnityAction AgeConfirmed;
    
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

    private void AgeEntered(string age)
    {
        int confirmedAge = Convert.ToInt32(age);
        
        if (confirmedAge >= MinNecessaryAge)
            AgeConfirmed?.Invoke();
    }

    private void OnPurchaseTried()
    {
        _background.SetActive(false);
        _panelUI.SetActive(false);
        
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
        }
    }
    
}
