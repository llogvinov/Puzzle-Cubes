using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class TextHolder
{
    private static Dictionary<string, List<string>> _generalTextsDictionary = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> _namesDictionary = new Dictionary<string, List<string>>();

    private static TMP_FontAsset _categoryTMP;
    private static TMP_FontAsset _mainTMP;
    private static TMP_FontAsset _cnTMP;
    private static TMP_FontAsset _jpTMP;
    private static TMP_FontAsset _krTMP;

    private static Font _mainFont;
    private static Font _cnFont;
    private static Font _jpFont;
    private static Font _krFont;

    static TextHolder()
    {
        _namesDictionary = ReadCsv("NamesLocalization");
        _generalTextsDictionary = ReadCsv("GeneralTexts");
        
        AdjustFonts();
    }

    public static Font GetFont(SystemLanguage language)
    {
        return language switch
        {
            SystemLanguage.ChineseSimplified => _cnFont,
            SystemLanguage.Japanese => _jpFont,
            SystemLanguage.Korean => _krFont,
            _ => _mainFont
        };
    }

    public static TMP_FontAsset GetCategoryFont(SystemLanguage language)
    {
        return language switch
        {
            SystemLanguage.ChineseSimplified => _cnTMP,
            SystemLanguage.Japanese => _jpTMP,
            SystemLanguage.Korean => _krTMP,
            _ => _categoryTMP
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
        
        var itemTitle = dictionary[key][index];
        return itemTitle;
    }

    private static Dictionary<string, List<string>> ReadCsv(string path)
    {
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        // Loading the dataset from Unity's Resources folder
        var dataset = Resources.Load<TextAsset>(path);
 
        // Splitting the dataset in the end of line
        var splitDataset = dataset.text.Split(new char[] {'\n'});
 
        // Iterating through the split dataset in order to split into rows
        for (int i = 1; i < splitDataset.Length; i++) 
        {
            string[] row = splitDataset[i].Split(new char[] {';'});

            string key = row[0];
            List<string> list = row.ToList();
            list.RemoveAt(0);
            
            dictionary.Add(key, list);
        }
        
        return dictionary;
    }

    private static void AdjustFonts()
    {
        _mainFont = Resources.Load<Font>("Fonts/Other/ButtonsAndItemsNames");
        _cnFont = Resources.Load<Font>("Fonts/Chineese/NotoSansSC-Medium");
        _jpFont = Resources.Load<Font>("Fonts/Japaneese/KosugiMaru-Regular");
        _krFont = Resources.Load<Font>("Fonts/Korean/Gugi-Regular");

        _categoryTMP = Resources.Load<TMP_FontAsset>("Fonts/TMPro Fonts/Categories");
        _mainTMP = Resources.Load<TMP_FontAsset>("Fonts/TMPro Fonts/Other");
        _cnTMP = Resources.Load<TMP_FontAsset>("Fonts/TMPro Fonts/Chinese");
        _jpTMP = Resources.Load<TMP_FontAsset>("Fonts/TMPro Fonts/Japanese");
        _krTMP = Resources.Load<TMP_FontAsset>("Fonts/TMPro Fonts/Korean");
    }

}
