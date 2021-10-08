using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundsUI : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _soundsMixer;
    [Space]
    [Range(-80f, 0f)]
    [SerializeField] private float _maxMusicVolume;
    [Range(-80f, 0f)]
    [SerializeField] private float _maxSoundsVolume;
    [Header("Buttons")]
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [Header("Sprites")]
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
            TurnOffMusic(true);
        if (PlayerPrefs.GetInt("sounds") == 0) 
            TurnOffSounds(true);
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
            TurnOffMusic();
        else if (_musicButton.image.sprite == _noMusicSprite)
            TurnOnMusic();
        else
            throw new Exception("Invalid button sprite");

        MusicVolumeChanged?.Invoke(PlayerPrefs.GetInt("music"));
    }

    // controls Sounds UI element
    // turns on or off the sounds
    private void Sound()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        if (_soundButton.image.sprite == _soundSprite)
            TurnOffSounds();
        else if (_soundButton.image.sprite == _noSoundSprite)
            TurnOnSounds();
        else
            throw new Exception("Invalid button sprite");

        SoundsVolumeChanged?.Invoke(PlayerPrefs.GetInt("sounds"));
    }

    private void TurnOffMusic(bool valueChecked = false)
    {
        _musicButton.image.sprite = _noMusicSprite;
        _musicMixer.audioMixer.SetFloat("MusicVolume", -80f);
        if (valueChecked)
            return;
        
        PlayerPrefs.SetInt("music", 0);
    }

    private void TurnOnMusic(bool valueChecked = false)
    {
        _musicButton.image.sprite = _musicSprite;
        _musicMixer.audioMixer.SetFloat("MusicVolume", _maxMusicVolume);
        if (valueChecked)
            return;
        
        PlayerPrefs.SetInt("music", 1);
    }

    private void TurnOffSounds(bool valueChecked = false)
    {
        _soundButton.image.sprite = _noSoundSprite;
        _soundsMixer.audioMixer.SetFloat("SoundsVolume", -80f);
        if (valueChecked)
            return;
        
        PlayerPrefs.SetInt("sounds", 0);
    }

    private void TurnOnSounds(bool valueChecked = false)
    {
        _soundButton.image.sprite = _soundSprite;
        _soundsMixer.audioMixer.SetFloat("SoundsVolume", _maxSoundsVolume);
        if (valueChecked)
            return;
        
        PlayerPrefs.SetInt("sounds", 1);
    }
    
}
