using UnityEngine;
using UnityEngine.UI;

public class PurchaseUI : PanelUI
{
     [SerializeField] private Button _purchaseButton;

     private void OnEnable()
     {
          Purchase.FullVersionPurchased += ClosePanel;
          CheckAgeUI.AgeConfirmed += OnAgeConfirmed;
     }

     private void OnDisable() 
     {
          Purchase.FullVersionPurchased -= ClosePanel;
          CheckAgeUI.AgeConfirmed -= OnAgeConfirmed;
     }
     
     private void Start()
     {
          AddPanelButtonsEvents();
          
          _purchaseButton.onClick.RemoveAllListeners();
          _purchaseButton.onClick.AddListener(Purchase.PurchaseFullVersion);
     }

     private void ClosePanel()
     {
          HidePanel();
          HideBackground();
          ShowButtons();
     }

}
