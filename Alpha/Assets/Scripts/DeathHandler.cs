using UnityEngine;
using UnityEngine.SceneManagement;  // 用于场景管理
using UnityEngine.UI;               // 用于UI控制

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPanel;      // 死亡通知面板
    public GameObject jumpObject;      // 跳出的物体
    public float delayBeforeDeath = 2f;  // 跳出物体后延迟时间，单位：秒
    public string mainMenuSceneName = "MainMenu";  // 主菜单场景的名称

    private JumpOnDoorOpen jumpObjectstatus;  // 检测 jumpObject 是否出现

    // Update is called once per frame
    void Start() 
    {
        deathPanel.SetActive(false);
    }
    void Update()
    {
        // 如果 jumpObject 出现并且计时尚未开始
        if (jumpObjectstatus.jumpObjectAppeared())
        {
            Invoke("ShowDeathMessage", delayBeforeDeath);  // 在 delayBeforeDeath 秒后显示死亡信息
        }
    }

    // 显示死亡通知
    void ShowDeathMessage()
    {
        deathPanel.SetActive(true);  // 激活死亡通知 UI
        Invoke("ReturnToMainMenu", 2f);  // 在2秒后返回主菜单
    }

    // 返回主菜单
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);  // 加载主菜单场景
    }
}