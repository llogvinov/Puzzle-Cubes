using UnityEngine;
using UnityEngine.UI;

public class UnPurchase : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameDataManager.UnpurchaseFullVersion();
#if UNITY_EDITOR
            Debug.Log("full version purchased: " + GameDataManager.GetFullVersionPurchased());
#endif
        }
    }
}
