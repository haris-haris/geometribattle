using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI wavetxt;
    public static int score;
    public static int wave;
   // MainMenuScript MainMenuScript;
    // Start is called before the first frame update
    void Start()
    {
        //MainMenuScript.gameStart = false;
        score = GameManager.instance.score;
        wave = GameManager.instance.curWave;
        scoretxt.text = "Score: " + score.ToString();
        wavetxt.text = "Wave Cleared: " + wave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            Application.LoadLevel(0);
        }
    }
}
