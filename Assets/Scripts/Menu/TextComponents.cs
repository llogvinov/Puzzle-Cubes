using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponents : MonoBehaviour
{
    [Header("Settings Panel")]
    [SerializeField] private Text _rateUs;
    [SerializeField] private Text _contactUs;
    [SerializeField] private Text _privacyPolicy;
    [SerializeField] private Text _website;
    [SerializeField] private Text _restore;

    [Header("Check Age Panel")]
    [SerializeField] private Text _checkAge;

    [Header("Full Version Panel")]
    [SerializeField] private Text _fullVersion;
    [SerializeField] private Text _openFullVersion;

    private void OnEnable()
    {
        LanguageChanger.LanguageChanged += TranslateAllTexts;
    }

    private void OnDisable()
    {
        LanguageChanger.LanguageChanged -= TranslateAllTexts;
    }

    private void Start()
    {
        TranslateAllTexts();
    }

    private void TranslateAllTexts()
    {
        var lang = GameDataManager.GetLanguage();
        
        TranslateSettings(lang);
        TranslateCheckAge(lang);
        TranslateFullVersion(lang);
    }
    
    private void TranslateSettings(SystemLanguage language)
    {
        _rateUs.text = TranslateWord(language, "rate us");
        _contactUs.text = TranslateWord(language, "contact us");
        _privacyPolicy.text = TranslateWord(language, "privacy policy");
        _website.text = TranslateWord(language, "website");
        _restore.text = TranslateWord(language, "restore");
    }

    private void TranslateCheckAge(SystemLanguage language)
    {
        _checkAge.text = TranslateWord(language, "check age");
    }

    private void TranslateFullVersion(SystemLanguage language)
    {
        _fullVersion.text = TranslateWord(language, "full version");
        _openFullVersion.text = TranslateWord(language, "open full version");
    }

    private string TranslateWord(SystemLanguage language, string key)
    {
        return TextHolder.GetTitle(language, key);
    }
}
