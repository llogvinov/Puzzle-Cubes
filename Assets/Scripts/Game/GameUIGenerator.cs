using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameUIGenerator : MonoBehaviour
{
    [Header("Containers")]
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;
    [Space]
    [SerializeField] private int _numberOfItems;
    [Space] 
    [SerializeField] private ItemPart _itemPart;
    
    [SerializeField] private CategoryDatabase _categoriesDb;
    [HideInInspector] public ItemDatabase ItemsDb;

    public static float SpriteWidth;
    
    private bool _hasMatchesAtStart;
    private int _halfNumberOfItems;
    
    private void Awake()
    {
        ItemsDb = _categoriesDb.Categories[GameDataManager.GetSelectedCategoryID()].ItemDatabase;
        _halfNumberOfItems = (_numberOfItems - 1) / 2;
        
        PLaceItemsParts(_halfNumberOfItems, _itemPart, new[] {_headParts, _bodyParts, _legsParts});

        GenerateUI();
    }

    public void GenerateUI()
    {
        _hasMatchesAtStart = true;
        
        List<Item> items = SelectRandomItems();
        while (_hasMatchesAtStart)
        {
            SetHeadItems(Shuffle(items));
            SetBodyItems(Shuffle(items));
            SetLegsItems(Shuffle(items));
            
            CheckNearbyPartsMatch(_halfNumberOfItems);
        }
    }
    
    private void PLaceItemsParts(int halfNumberOfItems, ItemPart itemPartPrefab, Transform[] containers)
    {
        SpriteRenderer itemPartSprite = itemPartPrefab.GetComponent<SpriteRenderer>();
        SpriteWidth = itemPartSprite.bounds.extents.x;

        foreach (var container in containers)
        {
            PlaceItemPartInContainer(container, halfNumberOfItems, itemPartPrefab, SpriteWidth);
        }
    }

    private void PlaceItemPartInContainer(Transform container, int halfNumberOfItems, ItemPart itemPartPrefab, float spriteWidth)
    {
        for (int i = -halfNumberOfItems; i <= halfNumberOfItems; i++)
        {
            Instantiate(itemPartPrefab, 
                itemPartPrefab.transform.position + Vector3.right * i * spriteWidth, 
                Quaternion.identity, 
                container);
        }
    }
    
    private List<Item> SelectRandomItems()
    {
        List<Item> finalItems = new List<Item>();

        List<Item> items = new List<Item>();
        for (int i = 0; i < ItemsDb.GetLength(); i++)
        {
            Item item = ItemsDb.GetItem(i);
            items.Add(item);
        }

        for (int i = 0; i < _numberOfItems; i++)
        {
            int index = UnityEngine.Random.Range(0, items.Count);
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
    
    private void CheckNearbyPartsMatch(int halfNumberOfItems)
    {
        int headItemID = _headParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;

        if (headItemID == bodyItemID || bodyItemID == legsItemID)
            return;

        _hasMatchesAtStart = false;
    }
}
