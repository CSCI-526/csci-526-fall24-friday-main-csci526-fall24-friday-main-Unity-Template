using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoryCheck : MonoBehaviour
{

    public GameObject victoryCanvas; // 将类型改为 GameObject

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            victoryCanvas.SetActive(true); // 显示胜利的 Canvas
            Invoke("QuitGame", 3f);
        }
    }

    void QuitGame()
    {
        Debug.Log("Quitting the game...");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
