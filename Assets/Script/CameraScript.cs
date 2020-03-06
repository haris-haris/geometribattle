using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    PlayerScript playerScript;
    float xCameraLimit = 36.8f;
    float zCameraLimit = 44.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        Quaternion quat = Quaternion.Euler(90, 0, 0);
        transform.rotation = quat;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            float x = player.transform.position.x;
            float y = 10.0f;
            float z = player.transform.position.z;

            if (x <= -xCameraLimit)
            {
                x = -xCameraLimit;
            }
            if (x >= xCameraLimit)
            {
                x = xCameraLimit;
            }
            if (z <= -zCameraLimit)
            {
                z = -zCameraLimit;
            }
            if (z >= zCameraLimit)
            {
                z = zCameraLimit;
            }
            transform.position = new Vector3(x, y, z);
        }
    }
}
