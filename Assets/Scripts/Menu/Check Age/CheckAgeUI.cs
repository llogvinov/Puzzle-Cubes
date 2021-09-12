using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckAgeUI : PanelUI
{
    [SerializeField] private EntryField _entryField;
    
    public static int CurrentPanelID;
    public static UnityAction AgeConfirmed;
    public static UnityAction PanelOpened;
    
    #region Singleton

    public static CheckAgeUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    private new void OnAgeConfirmed() => HidePanel();
    
    private void OnEnable() => AgeConfirmed += OnAgeConfirmed;

    private void OnDisable() => AgeConfirmed -= OnAgeConfirmed;

    private void Start()
    {
        if (_entryField == null)
            _entryField = FindObjectOfType<EntryField>();
        
        AddPanelButtonsEvents();
    }
    
    public void OnCheckAge(int id)
    {
        Debug.Log("opened");
    
        CurrentPanelID = id;
        
        PanelOpened?.Invoke();
        
        ShowPanel();
        HideButtons();
    }
    
}
