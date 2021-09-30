using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;
    
    public static UnityAction PartsMatched;
    public static UnityAction<int> ItemCollected;
    
    public void CheckAllPartsMatch(int halfNumberOfItems)
    {
        int headItemID = _headParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;

        if (headItemID != bodyItemID || headItemID != legsItemID) 
            return;
        
        PartsMatched?.Invoke();
        ItemCollected?.Invoke(headItemID);
        Debug.Log("parts matched");
    }
}
