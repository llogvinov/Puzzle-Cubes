using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundsUI : MonoBehaviour
{
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [Space(20f)]
    [SerializeField] private Sprite _musicSprite;
    [SerializeField] private Sprite _soundSprite;
    [SerializeField] private Sprite _noMusicSprite;
    [SerializeField] private Sprite _noSoundSprite;

    public static UnityAction<int> MusicVolumeChanged;
    public static UnityAction<int> SoundsVolumeChanged;
    
    private void Start()
    {
        AddSoundsButtonsEvents();

        if (PlayerPrefs.GetInt("music") == 0) 
            _musicButton.image.sprite = _noMusicSprite;
        if (PlayerPrefs.GetInt("sounds") == 0) 
            _soundButton.image.sprite = _noSoundSprite;
    }

    private void AddSoundsButtonsEvents()
    {
        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(Music);

        _soundButton.onClick.RemoveAllListeners();
        _soundButton.onClick.AddListener(Sound);
    }

    // controls Music UI element
    // turns on or off the music
    private void Music()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        if (_musicButton.image.sprite == _musicSprite)
        {
            _musicButton.image.sprite = _noMusicSprite;
            PlayerPrefs.SetInt("music", 0);
        }
        else if (_musicButton.image.sprite == _noMusicSprite)
        {
            _musicButton.image.sprite = _musicSprite;
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            throw new Exception("Invalid button sprite");
        }
        
        MusicVolumeChanged?.Invoke(PlayerPrefs.GetInt("music"));
    }

    // controls Sounds UI element
    // turns on or off the sounds
    private void Sound()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        if (_soundButton.image.sprite == _soundSprite)
        {
            _soundButton.image.sprite = _noSoundSprite;
            PlayerPrefs.SetInt("sounds", 0);
        }
        else if (_soundButton.image.sprite == _noSoundSprite)
        {
            _soundButton.image.sprite = _soundSprite;
            PlayerPrefs.SetInt("sounds", 1);
        }
        else
        {
            throw new Exception("Invalid button sprite");
        }
        
        SoundsVolumeChanged?.Invoke(PlayerPrefs.GetInt("sounds"));
    }
    
}
