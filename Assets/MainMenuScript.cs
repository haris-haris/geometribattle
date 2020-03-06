using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public bool gameStart;
    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            gameStart = true;
            Application.LoadLevel(1);        }
    }
}
