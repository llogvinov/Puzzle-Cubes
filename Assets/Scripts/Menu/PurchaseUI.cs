using System;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseUI : PanelUI
{
     private void OnEnable() => CheckAgeUI.AgeConfirmed += OnAgeConfirmed;

     private void OnDisable() => CheckAgeUI.AgeConfirmed -= OnAgeConfirmed;
     
     private void Start()
     {
          AddPanelButtonsEvents();
     }

}
