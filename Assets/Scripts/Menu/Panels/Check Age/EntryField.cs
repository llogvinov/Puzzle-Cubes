using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntryField : MonoBehaviour
{
    [SerializeField] private Button _backspaceButton;
    
    private Text[] _inputTexts;

    public static UnityAction<string> AgeTyped;
    private UnityAction PanelClosed;

    private void OnEnable() => PanelClosed += ClearInputFields;

    private void OnDisable() => PanelClosed -= ClearInputFields;

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
        _inputTexts = new Text[gameObject.transform.childCount - 1];

        for (int i = 0; i < _inputTexts.Length; i++)
        {
            _inputTexts[i] = gameObject.transform.GetChild(i)
                .transform.GetChild(1).GetComponent<Text>();
        }
    }
    
    private void ClearInputFields()
    {
        if (_inputTexts == null)
            return;
        
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
        if (index == -1)
            return;

        _inputTexts[index].text = value + "";
    }

    public void AllEntryCellsFilled()
    {
        string ageText = "";
        foreach (var inputText in _inputTexts)
        {
            ageText += inputText.text;
        }
        
        AgeTyped?.Invoke(ageText);
        PanelClosed?.Invoke();
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
