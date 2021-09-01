using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category
{
    public int ID;
    public string Name;
    public Sprite Sprite;
    public ItemDatabase ItemDatabase;

    public bool IsUnlocked;
}
