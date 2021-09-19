using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CategoriesData
{
    public List<int> PurchasedCategoriesIndexes = new List<int>();
}

[System.Serializable]
public class PlayerData
{
    public SystemLanguage Language;
    
    public bool FullVersionOpened;
    public int SelectedCategoryID;
}

public static class GameDataManager
{
    private static PlayerData _playerData = new PlayerData();

    private static CategoriesData _categoriesData = new CategoriesData();
    private static Category _selectedCategory = new Category();

    static GameDataManager()
    {
        LoadPlayerData();
        LoadCategoriesData();
        
        SetLanguage(Application.systemLanguage);
    }

    public static void SetLanguage(SystemLanguage language)
    {
        _playerData.Language = language;
        AudioHolder.SetAudioClipsList(language);
        
#if UNITY_EDITOR
        Debug.Log("Language changed to " + language);
#endif
    }

    public static SystemLanguage GetLanguage() => _playerData.Language;
    
    public static void OpenFullVersion() => _playerData.FullVersionOpened = true;
    
    public static bool IsFullVersionOpened() => _playerData.FullVersionOpened;
    
    public static Category GetSelectedCategory => _selectedCategory;
    
    public static void SetSelectedCategory(Category category, int categoryID)
    {
        _selectedCategory = category;
        _playerData.SelectedCategoryID = categoryID;
        SavePlayerData();
    }
    
    public static int GetSelectedCategoryID() => _playerData.SelectedCategoryID;

    public static void SetSelectedCategoryID(int newCategoryID) => _playerData.SelectedCategoryID = newCategoryID;
    
    private static void SavePlayerData()
    {
        BinarySerializer.Save(_playerData, "player-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadPlayerData()
    {
        _playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }

    public static void AddPurchasedCategory(int categoryID)
    {
        _categoriesData.PurchasedCategoriesIndexes.Add(categoryID);
        SaveCategoriesData();
    }

    public static List<int> GetAllPurchasedCategories() => _categoriesData.PurchasedCategoriesIndexes;

    public static int GetPurchasedCategory(int categoryID) => _categoriesData.PurchasedCategoriesIndexes[categoryID];
    
    private static void SaveCategoriesData()
    {
        BinarySerializer.Save(_categoriesData, "categories-data.txt");
#if UNITY_EDITOR
        Debug.Log("Saved.");
#endif
    }

    private static void LoadCategoriesData()
    {
        _categoriesData = BinarySerializer.Load<CategoriesData>("categories-data.txt");
#if UNITY_EDITOR
        Debug.Log("Loaded.");
#endif
    }
    
}
