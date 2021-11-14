using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScrollUI : MonoBehaviour
{
    [Header("Containers")]
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;
    [Space]
    [SerializeField] private int _numberOfItems;
    
    [SerializeField] private CategoryDatabase _categoriesDb;
    [HideInInspector] public ItemDatabase ItemsDb;
    
    private bool _hasMatchesAtStart;
    
    public static float[] Positions;
    public static float Distance;

    public static int MiddlePositionIndex;
    
    private void Awake()
    {
        ItemsDb = _categoriesDb.Categories[GameDataManager.GetSelectedCategoryID()].ItemDatabase;
        
        AdjustPositions();
        GenerateUI();
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
            SetHeadItems(ShuffleList(items));
            SetBodyItems(ShuffleList(items));
            SetLegsItems(ShuffleList(items));

            CheckNearbyPartsMatch(MiddlePositionIndex);
        }
    }

    private void Test(List<Item> list)
    {
        string s = "";
        foreach (var elementID in list)
        {
            s += elementID.ID;
        }
        Debug.Log(s);
    }
    
    private void AdjustPositions()
    {
        Positions = new float[_headParts.childCount];
        Distance = 1f / (Positions.Length - 1f);

        for (int i = 0; i < Positions.Length; i++)
        {
            Positions[i] = Distance * i;
        }
        
        MiddlePositionIndex = Positions.Length / 2;
    }
    
    /*
    private bool HasMatchesAtStart(int middlePositionIndex)
    {
        int headItemID = _headItems[middlePositionIndex].ID;
        int bodyItemID = _bodyItems[middlePositionIndex].ID;
        int legsItemID = _legsItems[middlePositionIndex].ID;

        if (headItemID == bodyItemID)
            Debug.Log("head and body");
        
        if (bodyItemID == legsItemID)
            Debug.Log("body and legs");

        return (headItemID == bodyItemID || bodyItemID == legsItemID);
    }
    */
    
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
    
    private void CheckNearbyPartsMatch(int middlePositionIndex)
    {
        int headItemID = _headParts.GetChild(middlePositionIndex).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(middlePositionIndex).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(middlePositionIndex).GetComponent<ItemPart>().ItemID;

        if (headItemID == bodyItemID)
            Debug.Log("head and body");
        
        if (bodyItemID == legsItemID)
            Debug.Log("body and legs");
        
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
            int index = Random.Range(0, items.Count);
            finalItems.Add(items[index]);
            items.Remove(items[index]);
        }
        
        return finalItems;
    }
    
    private List<Item> ShuffleList(List<Item> list) 
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

    private void MoveElementsRight(List<Item> list)
    {
        var lastElement = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        
        list.Insert(0, lastElement);
    }
    
}
