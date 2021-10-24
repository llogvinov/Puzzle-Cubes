using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScrollUI : MonoBehaviour
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
    
    private bool _hasMatchesAtStart;
    private int _halfNumberOfItems;

    private Transform[] _allContainers;
    
    private void Awake()
    {
        _allContainers = new[] {_headParts, _bodyParts, _legsParts};
        
        ItemsDb = _categoriesDb.Categories[GameDataManager.GetSelectedCategoryID()].ItemDatabase;
        _halfNumberOfItems = (_numberOfItems - 1) / 2;

        // ClearAllContainers(_allContainers);
        // FillAllContainers(_allContainers);

        GenerateUI();
    }

    private void ClearAllContainers(Transform[] containers)
    {
        foreach (var container in containers)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
    }

    private void FillAllContainers(Transform[] containers)
    {
        foreach (var container in containers)
        {
            for (int i = -_halfNumberOfItems; i <= _halfNumberOfItems; i++)
            {
                Instantiate(_itemPart, container);
            }
        }
    }
    
    public void GenerateUI()
    {
        _hasMatchesAtStart = true;
        
        List<Item> items = SelectRandomItems();
        while (_hasMatchesAtStart)
        {
#if UNITY_EDITOR
            Debug.Log("check matches at start");
#endif
            SetHeadItems(Shuffle(items));
            SetBodyItems(Shuffle(items));
            SetLegsItems(Shuffle(items));

            CheckNearbyPartsMatch(_halfNumberOfItems);
        }
    }

    private void SetHeadItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _headParts.GetChild(i).GetComponent<Image>().sprite = items[i].Head; 
            _headParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
    }
    
    private void SetBodyItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _bodyParts.GetChild(i).GetComponent<Image>().sprite = items[i].Body;
            _bodyParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
    }
    
    private void SetLegsItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            _legsParts.GetChild(i).GetComponent<Image>().sprite = items[i].Legs;
            _legsParts.GetChild(i).GetComponent<ItemPart>().ItemID = items[i].ID;
        }
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
