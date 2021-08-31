using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        AddSoundsButtonsEvents();

        if (PlayerPrefs.GetFloat("music") == 0) 
            _musicButton.image.sprite = _noMusicSprite;
        if (PlayerPrefs.GetFloat("sounds") == 0) 
            _soundButton.image.sprite = _noSoundSprite;
    }

    private void AddSoundsButtonsEvents()
    {
        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(Music);

        _soundButton.onClick.RemoveAllListeners();
        _soundButton.onClick.AddListener(Sound);
    }

    //Controls Music UI element
    //Turns on or off the music
    private void Music()
    {
        if (_musicButton.image.sprite == _musicSprite)
        {
            _musicButton.image.sprite = _noMusicSprite;
            PlayerPrefs.SetFloat("music", 0f);
        }
        else if (_musicButton.image.sprite == _noMusicSprite)
        {
            _musicButton.image.sprite = _musicSprite;
            PlayerPrefs.SetFloat("music", 1f);
        }
        else
        {
            throw new Exception("Invalid button sprite");
        }
    }

    //Controls Sounds UI element
    //Turns on or off the sounds
    private void Sound()
    {
        if (_soundButton.image.sprite == _soundSprite)
        {
            _soundButton.image.sprite = _noSoundSprite;
            PlayerPrefs.SetFloat("sounds", 0f);
        }
        else if (_soundButton.image.sprite == _noSoundSprite)
        {
            _soundButton.image.sprite = _soundSprite;
            PlayerPrefs.SetFloat("sounds", 1f);
        }
        else
        {
            throw new Exception("Invalid button sprite");
        }
    }
    
    
    
}
