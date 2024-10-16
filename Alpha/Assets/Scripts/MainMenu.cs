using UnityEngine;
using UnityEngine.SceneManagement; // 必须在其他类定义之前

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public string gameSceneName = "SampleScene"; // 你要加载的场景名

    // 游戏开始时显示主菜单
    void Start()
    {
        // mainMenu.SetActive(false); // 隐藏主菜单
        mainMenu.SetActive(true); // 显示主菜单
    }

    // 点击开始按钮后调用该函数
    public void OnStartGame()
    {
        Debug.Log("Start button clicked!");
        mainMenu.SetActive(false); // 隐藏主菜单
        SceneManager.LoadScene(gameSceneName); // 加载游戏场景
    }

    // 点击退出按钮后调用该函数
    public void OnQuitGame()
    {
        Debug.Log("Quit button clicked!");
        mainMenu.SetActive(false); // 隐藏主菜单
        Application.Quit(); // 退出游戏（仅在打包后的游戏中生效）
    }
}