using System;
using UnityEngine;
using UnityEngine.UI;

public class EntryField : MonoBehaviour
{
    [SerializeField] private GameObject _entryField;
    [SerializeField] private Button _backspaceButton;
    
    private Text[] _inputTexts;

    private const int PermissibleAge = 16;

    private void OnEnable() => CheckAgeUI.PanelOpened += ClearInputFields;

    private void OnDisable() => CheckAgeUI.PanelOpened -= ClearInputFields;

    public int GetEntryCellsCount() => _inputTexts.Length;
    
    private void Start()
    {
        AdjustTextComponents();
        ClearInputFields();
        
        _backspaceButton.onClick.RemoveAllListeners();
        _backspaceButton.onClick.AddListener(DeleteLastNumber);
    }

    private void AdjustTextComponents()
    {
        // exclude backspace button
        _inputTexts = new Text[_entryField.transform.childCount - 1];

        for (int i = 0; i < _inputTexts.Length; i++)
        {
            _inputTexts[i] = _entryField.transform.GetChild(i)
                .transform.GetChild(0).GetComponent<Text>();
        }
    }
    
    private void ClearInputFields()
    {
        foreach (var inputText in _inputTexts)
        {
            inputText.text = "";
        }
        
        Debug.Log("cleared");
    }

    public int CurrentInputFieldIndex()
    {
        for (int i = 0; i < _inputTexts.Length; i++)
        {
            if (_inputTexts[i].text == "")
                return i;
        }

        return -1;
    }

    public void EnterNumber(int index, int value)
    {
        if (index == -1)
            return;

        _inputTexts[index].text = value + "";
    }

    public void AllEntryCellsFilled()
    {
        string age = "";
        foreach (var inputText in _inputTexts)
        {
            age += inputText.text;
        }
        
        int confirmedAge = Convert.ToInt32(age);
        int yearsAge = DateTime.Today.Year - confirmedAge;
        
        if (yearsAge >= PermissibleAge)
        {
            CheckAgeUI.AgeConfirmed?.Invoke();
        }
    }

    private void DeleteLastNumber()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        for (int i = 0; i < _inputTexts.Length; i++)
        {
            if (i == _inputTexts.Length - 1)
            {
                _inputTexts[i].text = "";
                break;
            }

            if (_inputTexts[i + 1].text != "")
            {
                continue;
            }

            _inputTexts[i].text = "";
            break;
        }
    }

}
