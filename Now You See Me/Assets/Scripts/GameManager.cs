using UnityEngine;
using UnityEngine.UI;  
using TMPro;         

public class GameManager : MonoBehaviour
{
    public GameObject winPanel; 
    public TextMeshProUGUI winText; 

    void Start()
    {
        
        winPanel.SetActive(false);
    }

    // ���������������ʾ����
    public void ShowWinPanel()
    {
        winText.text = "You Win!"; 
        winPanel.SetActive(true);  
    }

    // ������Ϸ�߼�...
}
