using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform _headParts;
    [SerializeField] private Transform _bodyParts;
    [SerializeField] private Transform _legsParts;
    
    [SerializeField] private int _numberOfItems;

    [SerializeField] private SpriteRenderer _headPart;
    [SerializeField] private SpriteRenderer _bodyPart;
    [SerializeField] private SpriteRenderer _legsPart;

    private void Start()
    {
        PLaceBodyParts((_numberOfItems - 1) / 2, _headPart, _headParts);
        PLaceBodyParts((_numberOfItems - 1) / 2, _bodyPart, _bodyParts);
        PLaceBodyParts((_numberOfItems - 1) / 2, _legsPart, _legsParts);
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
}
