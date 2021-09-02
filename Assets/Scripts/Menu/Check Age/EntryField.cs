using System;
using UnityEngine;
using UnityEngine.UI;

public class EntryField : MonoBehaviour
{
    [SerializeField] private GameObject _entryField;
    [SerializeField] private Button _backspaceButton;
    
    private Text[] _inputTexts;

    public int GetEntryCellsCount() => _inputTexts.Length;
    
    private void Awake()
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
        if (yearsAge >= 16)
            CheckAgeUI.AgeConfirmed?.Invoke();
    }

    private void DeleteLastNumber()
    {
        for (int i = 0; i < _inputTexts.Length; i++)
        {
            if (_inputTexts[i + 1].text != "")
                continue;
            
            _inputTexts[i].text = "";
            return;
        }
    }

}
