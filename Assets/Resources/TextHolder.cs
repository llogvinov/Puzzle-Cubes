using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public static class TextHolder
{
    private static Dictionary<string, List<string>> _generalTextsDictionary = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> _namesDictionary = new Dictionary<string, List<string>>();
    
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
        int index = GetLanguageIndex(language);
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
    }

    private static int GetLanguageIndex(SystemLanguage language)
    {
        return language switch
        {
            SystemLanguage.English => 0,
            SystemLanguage.Russian => 1,
            SystemLanguage.German => 2,
            SystemLanguage.French => 3,
            SystemLanguage.Italian => 4,
            SystemLanguage.Spanish => 5,
            SystemLanguage.Portuguese => 6,
            SystemLanguage.Polish => 7,
            SystemLanguage.ChineseSimplified => 8,
            SystemLanguage.Japanese => 9,
            SystemLanguage.Korean => 10,
            _ => -1
        };
    }

}
