using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguageUI : PanelUI
{
    [SerializeField] private Button[] _languageButtons;
    [SerializeField] private Button _openButton;

    [SerializeField] private Sprite[] _languageSprites;

    private Image[] _buttonsBackgroundImages;
    
    private int _lastSelectedLanguageIndex;
    
    public static UnityAction<SystemLanguage> LanguageChanged;

    private void Start()
    {
        _buttonsBackgroundImages = new Image[_languageButtons.Length];
        for (int i = 0; i < _buttonsBackgroundImages.Length; i++)
        {
            _buttonsBackgroundImages[i] = _languageButtons[i].transform.GetChild(0).GetComponent<Image>();
        }
        
        DeselectAllButtons();
        SetLanguageUI(GameDataManager.GetLanguage());
        
        AddButtonsEvents();
    }

    private void AddButtonsEvents()
    {
        AddPanelButtonsEvents();
        
        _openButton.onClick.RemoveAllListeners();
        _openButton.onClick.AddListener(OpenLanguageUI);

        foreach (var button in _languageButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        
        _languageButtons[0].onClick.AddListener(SetEnglish);
        _languageButtons[1].onClick.AddListener(SetRussian);
        _languageButtons[2].onClick.AddListener(SetGerman);
        _languageButtons[3].onClick.AddListener(SetFrench);
        _languageButtons[4].onClick.AddListener(SetItalian);
        _languageButtons[5].onClick.AddListener(SetSpanish);
        _languageButtons[6].onClick.AddListener(SetPortuguese);
        _languageButtons[7].onClick.AddListener(SetPolish);
        _languageButtons[8].onClick.AddListener(SetChinese);
        _languageButtons[9].onClick.AddListener(SetJapanese);
        _languageButtons[10].onClick.AddListener(SetKorean);
    }

    private void OpenLanguageUI()
    {
        MenuSoundsManager.Instance.PlayClickedSound();

        ShowBackground();
        ShowPanel();
        HideButtons();
    }

    private void SetEnglish() => SetLanguage(SystemLanguage.English);
    private void SetRussian() => SetLanguage(SystemLanguage.Russian);
    private void SetGerman() => SetLanguage(SystemLanguage.German);
    private void SetFrench() => SetLanguage(SystemLanguage.French);
    private void SetItalian() => SetLanguage(SystemLanguage.Italian);
    private void SetSpanish() => SetLanguage(SystemLanguage.Spanish);
    private void SetPortuguese() => SetLanguage(SystemLanguage.Portuguese);
    private void SetPolish() => SetLanguage(SystemLanguage.Polish);
    private void SetChinese() => SetLanguage(SystemLanguage.ChineseSimplified);
    private void SetJapanese() => SetLanguage(SystemLanguage.Japanese);
    private void SetKorean() => SetLanguage(SystemLanguage.Korean);
    
    private void SetLanguage(SystemLanguage language)
    {
        SetBackgroundImageAlpha(_buttonsBackgroundImages[_lastSelectedLanguageIndex]);
        
        MenuSoundsManager.Instance.PlayClickedSound();
        
        GameDataManager.SetLanguage(language);
        SetLanguageUI(language);
        
        LanguageChanged?.Invoke(language);
    }

    private void SetLanguageUI(SystemLanguage language)
    {
        int index = Array.IndexOf(GameDataManager.AvailableLanguages, language);
        
        _openButton.image.sprite = _languageSprites[index];
        SetBackgroundImageAlpha(_buttonsBackgroundImages[index], 1f);

        _lastSelectedLanguageIndex = index;
    }

    private void SetBackgroundImageAlpha(Image backgroundImage, float alpha = 0f)
    {
        if (Math.Abs(backgroundImage.color.a - alpha) < 0.01f)
            return;
        
        var color = backgroundImage.color;
        color.a = alpha;
        backgroundImage.color = color;
    }

    private void DeselectAllButtons()
    {
        foreach (var backgroundImage in _buttonsBackgroundImages)
        {
            SetBackgroundImageAlpha(backgroundImage);
        }
    }

}
