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
    
    public static UnityAction AgeConfirmed;
    
    private void OnAgeConfirmed() => ClosePanelUI();
    
    private void OnEnable() => AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => AgeConfirmed += OnAgeConfirmed;

    private void Start() => AddButtonsEvents();
    
    private void AddButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    // TODO: add event on category buttons, invoke event whenever user clicks closed buttons
    // calls when user clicks closed category
    private void OnPurchaseTried()
    {
        ShowPanel();
        HideButtons();
    }

    private void ShowPanel()
    {
        _background.SetActive(false);
        _panelUI.SetActive(false);
    }

    private void HideButtons()
    {
        foreach (var button in _buttonsToHide)
        {
            button.gameObject.SetActive(true);
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
