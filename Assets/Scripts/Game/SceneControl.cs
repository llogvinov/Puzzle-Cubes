using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    private Button _backButton;
    
    private int _menuSceneIndex = 0;

    private void Start()
    {
        _backButton = GetComponent<Button>();

        AddButtonsEvents();
    }

    private void AddButtonsEvents()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(BackToMenu);
    }
    
    private void BackToMenu()
    {
        GameSoundsManager.Instance.PlayButtonSound();
        
        SceneManager.LoadScene(_menuSceneIndex);
    }
}
