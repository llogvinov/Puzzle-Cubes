using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckAgeUI : PanelUI
{
    [SerializeField] private EntryField _entryField;
    
    private const int PermissibleAge = 16;
    
    public static int CurrentPanelID;
    public static UnityAction AgeConfirmed;
    
    #region Singleton

    public static CheckAgeUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion
    
    private new void OnAgeConfirmed() => HidePanel();
    
    private void OnEnable()
    {
        EntryField.AgeTyped += OnAgeTyped;
        AgeConfirmed += HidePanel;
    } 

    private void OnDisable()
    {
        EntryField.AgeTyped -= OnAgeTyped;
        AgeConfirmed -= HidePanel;
    }

    private void Start()
    {
        if (_entryField == null)
            _entryField = FindObjectOfType<EntryField>();
        
        AddPanelButtonsEvents();
    }
    
    public void OnCheckAge(int id)
    {
        CurrentPanelID = id;

        ShowBackground();
        ShowPanel();
        HideButtons();
    }
    
    private void OnAgeTyped(string ageText)
    {
        // count age
        int confirmedAge = Convert.ToInt32(ageText);
        int age = DateTime.Today.Year - confirmedAge;

        if (age > PermissibleAge)
        {
            AgeConfirmed?.Invoke();
        }
        else
        {
            HidePanel();
            HideBackground();
            ShowButtons();
        }
    }
}
