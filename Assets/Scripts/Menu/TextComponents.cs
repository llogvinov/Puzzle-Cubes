using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponents : MonoBehaviour
{
    [Header("Settings Panel")]
    [SerializeField] private Text _rateUs;
    [SerializeField] private Text _contactUs;
    [SerializeField] private Text _fullVersionSettings;
    [SerializeField] private Text _privacyPolicy;
    [SerializeField] private Text _website;
    [SerializeField] private Text _restore;

    [Header("Check Age Panel")]
    [SerializeField] private Text _checkAge;

    [Header("Full Version Panel")]
    [SerializeField] private Text _fullVersionTitle;
    [SerializeField] private Text _openFullVersion;

    private void OnEnable() => LanguageUI.LanguageChanged += TranslateAllTexts;

    private void OnDisable() => LanguageUI.LanguageChanged -= TranslateAllTexts;

    private void Start()
    {
        TranslateAllTexts(GameDataManager.GetLanguage());
    }

    private void TranslateAllTexts(SystemLanguage language)
    {
        TranslateSettings(language);
        TranslateCheckAge(language);
        TranslateFullVersion(language);
    }
    
    private void TranslateSettings(SystemLanguage language)
    {
        TranslateWordThin(_rateUs, language, "rate us");
        TranslateWordThin(_contactUs, language, "contact us");
        TranslateWordThin(_fullVersionSettings, language, "full version");
        TranslateWordThin(_privacyPolicy, language, "privacy policy");
        TranslateWordThin(_website, language, "website");
        TranslateWordThin(_restore, language, "restore");
    }

    private void TranslateCheckAge(SystemLanguage language)
    {
        TranslateWordThin(_checkAge, language, "check age");
    }

    private void TranslateFullVersion(SystemLanguage language)
    {
        TranslateWord(_fullVersionTitle, language, "purchase full version");
        TranslateWordThin(_openFullVersion, language, "open full version");
    }

    private void TranslateWordThin(Text title, SystemLanguage language, string key)
    {
        title.text = TextHolder.GetTitle(language, key);

        var font = TextHolder.GetThinFont(language);
        if (title.font == font)
            return;
        
        title.font = font;
    }

    private void TranslateWord(Text title, SystemLanguage language, string key)
    {
        title.text = TextHolder.GetTitle(language, key);

        var font = TextHolder.GetFont(language);
        if (title.font == font)
            return;
        
        title.font = font;
    }
}
