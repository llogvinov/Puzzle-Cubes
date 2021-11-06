using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TextHolder
{
    private static Dictionary<string, List<string>> _generalTextsDictionary = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> _namesDictionary = new Dictionary<string, List<string>>();

    private static Font _defaultFont;
    private static Font _plFont;
    private static Font _cnFont;
    private static Font _jpFont;
    private static Font _krFont;

    private static Font _defaultThinFont;
    private static Font _cnThinFont;
    private static Font _jpThinFont;
    private static Font _krThinFont;

    static TextHolder()
    {
        _namesDictionary = ReadCSV("NamesLocalization");
        _generalTextsDictionary = ReadCSV("GeneralTexts");
        
        AdjustFonts();
    }

    public static Font GetFont(SystemLanguage language)
    {
        return language switch
        {
            SystemLanguage.Polish => _plFont,
            SystemLanguage.ChineseSimplified => _cnFont,
            SystemLanguage.Japanese => _jpFont,
            SystemLanguage.Korean => _krFont,
            _ => _defaultFont
        };
    }
    
    public static Font GetThinFont(SystemLanguage language)
    {
        return language switch
        {
            SystemLanguage.ChineseSimplified => _cnThinFont,
            SystemLanguage.Japanese => _jpThinFont,
            SystemLanguage.Korean => _krThinFont,
            _ => _defaultThinFont
        };
    }
    
    public static string GetName(SystemLanguage language, string name)
    {
        return GetTextFromDictionary(_namesDictionary, language, name);
    }

    public static string GetTitle(SystemLanguage language, string title)
    {
        return GetTextFromDictionary(_generalTextsDictionary, language, title);
    }
    
    private static string GetTextFromDictionary(Dictionary<string, List<string>> dictionary, SystemLanguage language, string key)
    {
        int index = Array.IndexOf(GameDataManager.AvailableLanguages, language);
        if (index == -1)
            throw new ArgumentException();
        
        string itemTitle = dictionary[key][index];
        return itemTitle;
    }

    private static Dictionary<string, List<string>> ReadCSV(string path)
    {
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        TextAsset dataset = Resources.Load<TextAsset>(path);
        string[] splitDataset = dataset.text.Split('\n');
 
        for (int i = 1; i < splitDataset.Length; i++) 
        {
            string[] row = splitDataset[i].Split(';');

            string key = row[0];
            List<string> list = row.ToList();
            list.RemoveAt(0);
            
            dictionary.Add(key, list);
        }
        
        return dictionary;
    }

    private static void AdjustFonts()
    {
        _defaultFont = Resources.Load<Font>("Fonts/default_font");
        _plFont = Resources.Load<Font>("Fonts/pl_font");
        _cnFont = Resources.Load<Font>("Fonts/cn_font");
        _jpFont = Resources.Load<Font>("Fonts/jp_font");
        _krFont = Resources.Load<Font>("Fonts/kr_font");

        _defaultThinFont = Resources.Load<Font>("Fonts/default_thin_font");
        _cnThinFont = Resources.Load<Font>("Fonts/cn_thin_font");
        _jpThinFont = Resources.Load<Font>("Fonts/jp_thin_font");
        _krThinFont = Resources.Load<Font>("Fonts/kr_thin_font");
    }
}
