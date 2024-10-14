using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{

    public float sanityDecline = 1.0f;
    public float playerSanity = 100.0f;
    public TMPro.TextMeshProUGUI textUI;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerSanity -= sanityDecline * Time.deltaTime;
        textUI.text = "Sanity: " + ((int)playerSanity).ToString();
        //print(playerSanity);
    }
}
