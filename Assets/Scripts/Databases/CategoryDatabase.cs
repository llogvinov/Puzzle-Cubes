using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CategoryDatabase", 
    menuName = "Database/CategoryDatabase")]
public class CategoryDatabase : ScriptableObject
{
    public Category[] Categories;

    public int GetLength() => Categories.Length;

    public Category GetCategory(int index) => Categories[index];
}
