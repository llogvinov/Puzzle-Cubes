using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NumberButton : MonoBehaviour
{
    [SerializeField] private EntryField _entryField;
    [SerializeField] private Button _buttonComponent;
    
    [SerializeField] private int _buttonValue;
    
    private void Start()
    {
        if (_entryField == null)
            _entryField = FindObjectOfType<EntryField>();
        
        if (_buttonComponent == null)
            _buttonComponent = gameObject.GetComponent<Button>();
        
        _buttonComponent.onClick.RemoveAllListeners();
        _buttonComponent.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        int index = _entryField.CurrentInputFieldIndex();
        _entryField.EnterNumber(index, _buttonValue);

        if (index != _entryField.GetEntryCellsCount() - 1) 
            return;

        _entryField.AllEntryCellsFilled();
    }
}
