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
    [SerializeField] private Text _fullVersion;
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
        TranslateWord(_rateUs, language, "rate us");
        TranslateWord(_contactUs, language, "contact us");
        TranslateWord(_fullVersionSettings, language, "full version");
        TranslateWord(_privacyPolicy, language, "privacy policy");
        TranslateWord(_website, language, "website");
        TranslateWord(_restore, language, "restore");
    }

    private void TranslateCheckAge(SystemLanguage language)
    {
        TranslateWord(_checkAge, language, "check age");
    }

    private void TranslateFullVersion(SystemLanguage language)
    {
        TranslateWord(_fullVersion, language, "full version");
        TranslateWord(_openFullVersion, language, "open full version");
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
