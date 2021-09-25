using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category
{
    public int ID;
    public string Name;
    public Color NameColor;
    public Color DarkShadow;
    public Color LightShadow;
    public Sprite Sprite;
    public ItemDatabase ItemDatabase;

    public bool IsLocked;
}
