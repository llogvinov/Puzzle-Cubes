using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CategoryDatabase", 
    menuName = "Database/CategoryDatabase")]
public class CategoryDatabase : ScriptableObject
{
    public Category[] Categories;

    public int GetLength() => Categories.Length;
    
    public Category GetCategory(int index) => Categories[index];

    public void CheckPurchase()
    {
        Debug.Log("full version purchased: " + GameDataManager.GetFullVersionPurchased());
        
        if (!GameDataManager.GetFullVersionPurchased())
        {
            FullVersionNotPurchased();
        }
        else
        {
            FullVersionPurchased();
        }
    }

    private void FullVersionNotPurchased()
    {
        for (int i = 0; i < GetLength(); i++)
        {
            if (i == 0)
            {
                PurchaseCategory(i);
            }
            else
            {
                LockCategory(i);
            }
        }
    }

    private void FullVersionPurchased()
    {
        for (int i = 0; i < GetLength(); i++)
        {
            if (!Categories[i].IsLocked)
                continue;
            
            PurchaseCategory(i);
        }
    }
    
    private void PurchaseCategory(int id)
    {
        Categories[id].IsLocked = false;
    }

    private void LockCategory(int id)
    {
        Categories[id].IsLocked = true;
    }
}
