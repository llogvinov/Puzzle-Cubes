using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", 
    menuName = "Database/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public Item[] Items;
    
    public int GetLength() => Items.Length;

    public Item GetItem(int index) => Items[index];
}
