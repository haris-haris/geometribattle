using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    public float speed = 10.0f;
    public Quaternion rotationQ;
    float currentTime;
    public GameObject playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(playerTarget.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        //exists limit
        if (transform.position.x < -50 || transform.position.x > 50 ||
            transform.position.z < -50 || transform.position.z > 50 ||
            currentTime > 5)
        {
            Destroy(this.gameObject);
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}

