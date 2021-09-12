using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;
    
    [SerializeField] private int _numberOfItems;

    [SerializeField] private SpriteRenderer _headPart;
    [SerializeField] private SpriteRenderer _bodyPart;
    [SerializeField] private SpriteRenderer _legsPart;

    [SerializeField] private CategoryDatabase _categoriesDb;
    private ItemDatabase _itemsDb;
    
    private void Start()
    {
        _itemsDb = _categoriesDb.Categories[GameDataManager.GetSelectedCategoryID()].ItemDatabase;
        
        PLaceBodyParts((_numberOfItems - 1) / 2, _headPart, _headParts);
        PLaceBodyParts((_numberOfItems - 1) / 2, _bodyPart, _bodyParts);
        PLaceBodyParts((_numberOfItems - 1) / 2, _legsPart, _legsParts);
        
        List<Item> items = SelectRandomItems();

        SetHeadItems(Shuffle(items));
        SetBodyItems(Shuffle(items));
        SetLegsItems(Shuffle(items));
    }

    private void PLaceBodyParts(int numberOfItems, SpriteRenderer spritePrefab, Transform container)
    {
        float spriteWidth = spritePrefab.bounds.extents.x;
        
        for (int i = -numberOfItems; i <= numberOfItems; i++)
        {
            SpriteRenderer spriteRenderer = 
                Instantiate(spritePrefab, 
                    spritePrefab.transform.position + Vector3.right *i * spriteWidth, 
                    Quaternion.identity, 
                    container);
        }
    }

    private List<Item> SelectRandomItems()
    {
        List<Item> finalItems = new List<Item>();

        List<Item> items = new List<Item>();
        for (int i = 0; i < _itemsDb.GetLength(); i++)
        {
            Item item = _itemsDb.GetItem(i);
            items.Add(item);
        }

        for (int i = 0; i < _numberOfItems; i++)
        {
            int index = UnityEngine.Random.Range(0,items.Count);
            finalItems.Add(items[index]);
            items.Remove(items[index]);
        }
        
        return finalItems;
    }

    private void SetHeadItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _headParts.GetChild(i).GetComponent<SpriteRenderer>().sprite = items[i].Head;
            _headParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
    }
    
    private void SetBodyItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _bodyParts.GetChild(i).GetComponent<SpriteRenderer>().sprite = items[i].Body;
            _bodyParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
    }
    
    private void SetLegsItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _legsParts.GetChild(i).GetComponent<SpriteRenderer>().sprite = items[i].Legs;
            _legsParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
    }

    private void CheckMatch()
    {
        int headItemID = _headParts.GetChild((_numberOfItems - 1) / 2).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild((_numberOfItems - 1) / 2).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild((_numberOfItems - 1) / 2).GetComponent<ItemPart>().ItemID;

        if (headItemID != bodyItemID || headItemID != legsItemID) 
            return;
        
        Debug.Log("kaif");
    }
    
    private List<Item> Shuffle(List<Item> list) 
    {
        int n = list.Count;
        var rnd = new System.Random();
        while (n > 1) 
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}
