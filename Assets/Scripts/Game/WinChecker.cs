using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinChecker : MonoBehaviour
{
    [Header("Containers")]
    public Transform HeadsContainer;
    public Transform BodiesContainer;
    public Transform LegsContainer;
    
    public static UnityAction PlayerWon;
    public static UnityAction<int> ItemCollected;
    
    public void CheckAllPartsMatch(int halfNumberOfItems)
    {
        int headItemID = HeadsContainer.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int bodyItemID = BodiesContainer.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;
        int legsItemID = LegsContainer.GetChild(halfNumberOfItems).GetComponent<ItemPart>().ItemID;

        if (headItemID != bodyItemID || headItemID != legsItemID) 
            return;
        
        PlayerWon?.Invoke();
        ItemCollected?.Invoke(headItemID);
        Debug.Log("parts matched");
    }
}
