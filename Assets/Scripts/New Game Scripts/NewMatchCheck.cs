using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewMatchCheck : MonoBehaviour
{
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;

    private int _middleChildIndex;

    public static UnityAction PlayerWon;
    public static UnityAction<int> PartsMatched;
    
    private void OnEnable() => NewSliderInput.Swiped += CheckPartsMatched;

    private void OnDisable() => NewSliderInput.Swiped -= CheckPartsMatched;

    private void Start()
    {
        _middleChildIndex = _headParts.childCount / 2;
    }

    private void CheckPartsMatched()
    {
        int headItemID = _headParts.GetChild(_middleChildIndex).GetComponent<ItemPart>().ItemID;
        int bodyItemID = _bodyParts.GetChild(_middleChildIndex).GetComponent<ItemPart>().ItemID;
        int legsItemID = _legsParts.GetChild(_middleChildIndex).GetComponent<ItemPart>().ItemID;

        if (headItemID != bodyItemID || headItemID != legsItemID) 
            return;
        
        PlayerWon?.Invoke();
        PartsMatched?.Invoke(headItemID);
        Debug.Log("parts matched");
    }
}
