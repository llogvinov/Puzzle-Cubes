using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    private const int MenuSceneIndex = 0;
    private const int GameSceneIndex = 1;

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(MenuSceneIndex);
    }
}
