using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", 
    menuName = "Database/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public Item[] Items;
}
