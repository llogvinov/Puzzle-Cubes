using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackButton : MonoBehaviour
{
    private Button _backButton;
   
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
        
        SceneControl.LoadMenuScene();
    }
}
