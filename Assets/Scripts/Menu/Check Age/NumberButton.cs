using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NumberButton : MonoBehaviour
{
    [SerializeField] private int _buttonValue;
    
    [SerializeField] private MenuSoundsManager _soundsManager;

    private EntryField _entryField;

    private void Start()
    {
        _entryField = FindObjectOfType<EntryField>();
        
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        _soundsManager.PlayClickedSound();
        
        int index = _entryField.CurrentInputFieldIndex();
        _entryField.EnterNumber(index, _buttonValue);

        if (index != _entryField.GetEntryCellsCount() - 1) 
            return;

        _entryField.AllEntryCellsFilled();
    }
    
}
