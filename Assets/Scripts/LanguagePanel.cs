using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguagePanel : PanelUI
{
    [SerializeField] private Button[] _languageButtons;
    [SerializeField] private Button _openButton;

    [SerializeField] private Sprite[] _languageSprites;
    
    public static UnityAction LanguageChanged;

    private void Start()
    {
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
        DeselectAllButtons();
        
        MenuSoundsManager.Instance.PlayClickedSound();
        
        GameDataManager.SetLanguage(language);
        SetLanguageUI(language);
        
        LanguageChanged?.Invoke();
    }

    private void SetLanguageUI(SystemLanguage language)
    {
        int index = Array.IndexOf(GameDataManager.AvailableLanguages, language);
        
        _openButton.gameObject.GetComponent<Image>().sprite = _languageSprites[index];
        SetBackgroundImageAlpha(_languageButtons[index], 1f);
    }

    private void SetBackgroundImageAlpha(Button button, float alpha = 0f)
    {
        if (Math.Abs(button.image.color.a - alpha) < .01f)
            return;
        
        var color = button.image.color;
        color.a = alpha;
        button.image.color = color;
    }

    private void DeselectAllButtons()
    {
        foreach (var button in _languageButtons)
        {
            SetBackgroundImageAlpha(button);
        }
    }

}
