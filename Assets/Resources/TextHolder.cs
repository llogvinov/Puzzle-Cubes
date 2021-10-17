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

    private static Font _categoriesFont;
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

    public static Font GetCategoryFont()
    {
        return _categoriesFont;
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
        var dataset = Resources.Load<TextAsset>(path);
        var splitDataset = dataset.text.Split(new char[] {'\n'});
 
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
        _categoriesFont = Resources.Load<Font>("Fonts/Other/NamesOnCategories");
        _mainFont = Resources.Load<Font>("Fonts/Other/ButtonsAndItemsNames");
        _cnFont = Resources.Load<Font>("Fonts/Chineese/NotoSansSC-Medium");
        _jpFont = Resources.Load<Font>("Fonts/Japaneese/KosugiMaru-Regular");
        _krFont = Resources.Load<Font>("Fonts/Korean/Gugi-Regular");
    }

}
