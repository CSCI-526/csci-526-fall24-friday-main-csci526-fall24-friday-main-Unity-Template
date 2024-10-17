using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;  // 用于场景管理

public class MainMenuController : MonoBehaviour
{
    // 加载 SampleScene
    public string gameScene = "SampleScene";
    public void StartGame()
    {
        Debug.Log("start");
        ReturnToGame();  // 加载游戏的主场景
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("quit");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // 如果在编辑器中，停止播放
        #else
            Application.Quit();  // 在构建后退出游戏
        #endif
    }

    void ReturnToGame()
    {
        SceneManager.LoadScene(gameScene);  // 加载主菜单场景
    }
}