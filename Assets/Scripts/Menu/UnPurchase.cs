using UnityEngine;
using UnityEngine.UI;

public class UnPurchase : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameDataManager.UnpurchaseFullVersion();
            GameDataManager.UnWatchTutorial();
#if UNITY_EDITOR
            Debug.Log("full version purchased: " + GameDataManager.GetFullVersionPurchased());
            Debug.Log("tutorial watched: " + GameDataManager.GetTutorialWatched());
#endif
        }
    }
}
