using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CategoryDatabase", 
    menuName = "Database/CategoryDatabase")]
public class CategoryDatabase : ScriptableObject
{
    public static UnityAction FullVersionPurchased;
    
    public Category[] Categories;

    public int GetLength() => Categories.Length;

    public Category GetCategory(int index) => Categories[index];

    public void PurchaseFullVersion()
    {
        FullVersionPurchased?.Invoke();
        
        for (int i = 0; i < Categories.Length; i++)
        {
            if (Categories[i].IsLocked)
            {
                Categories[i].IsLocked = false;
                GameDataManager.AddPurchasedCategory(i);
            }
        }
    }
}
