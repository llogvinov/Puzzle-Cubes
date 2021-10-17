using UnityEngine;
using UnityEngine.Events;

public class Purchase : MonoBehaviour
{
    public static UnityAction FullVersionPurchased;
    
    public static void PurchaseFullVersion()
    {
        GameDataManager.PurchaseFullVersion();
        
        FullVersionPurchased?.Invoke();
    }
}