using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckAgeUIold : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _panelUI;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] private Button[] _buttonsToHide;

    [SerializeField] private EntryField _entryField;
    
    public static int PanelID;
    
    public static UnityAction AgeConfirmed;

    #region Singleton

    public static CheckAgeUIold Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    private void OnAgeConfirmed() => ClosePanelUI();
    
    private void OnEnable() => AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => AgeConfirmed += OnAgeConfirmed;

    private void Start()
    {
        if (_entryField == null)
            _entryField = FindObjectOfType<EntryField>();
        
        AddButtonsEvents();
    }
    
    private void AddButtonsEvents()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(ClosePanelUI);
    }
    
    public void OnCheckAge(int id)
    {
        PanelID = id;
        
        // _entryField.ClearInputFields(); 
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
        MenuSoundsManager.Instance.PlayClickedSound();
        
        HidePanel();
        // ShowButtons();
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
