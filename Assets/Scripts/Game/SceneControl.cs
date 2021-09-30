using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
    private static int _menuSceneIndex = 0;
    private static int _gameSceneIndex = 1;

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }
}
