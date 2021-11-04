using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundsUI : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _soundsMixer;
    [Space]
    [Range(-80f, 20f)]
    [SerializeField] private float _maxMusicVolume;
    [Range(-80f, 20f)]
    [SerializeField] private float _maxSoundsVolume;
    [Header("Buttons")]
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [Header("Sprites")]
    [SerializeField] private Sprite _musicSprite;
    [SerializeField] private Sprite _soundSprite;
    [SerializeField] private Sprite _noMusicSprite;
    [SerializeField] private Sprite _noSoundSprite;
    
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
        _musicButton.onClick.AddListener(ControlMusicVolume);

        _soundButton.onClick.RemoveAllListeners();
        _soundButton.onClick.AddListener(ControlSoundsVolume);
    }

    // controls Music UI element
    // turns on or off the music
    private void ControlMusicVolume()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        if (_musicButton.image.sprite == _musicSprite)
            TurnOffMusic();
        else if (_musicButton.image.sprite == _noMusicSprite)
            TurnOnMusic();
        else
            throw new Exception("Invalid button sprite");
    }

    // controls Sounds UI element
    // turns on or off the sounds
    private void ControlSoundsVolume()
    {
        MenuSoundsManager.Instance.PlayClickedSound();
        
        if (_soundButton.image.sprite == _soundSprite)
            TurnOffSounds();
        else if (_soundButton.image.sprite == _noSoundSprite)
            TurnOnSounds();
        else
            throw new Exception("Invalid button sprite");
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
