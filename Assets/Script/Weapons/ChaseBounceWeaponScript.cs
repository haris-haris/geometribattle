using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBounceWeaponScript : MonoBehaviour
{
    bool fire;

    // amount of bullets per second
    public float rate = 2.0f;

    float deltaTime = 0.0f;
    float currentTime = 0.0f;

    public Quaternion rotationQ;
    public GameObject bullet;
    public Vector3 playerPosition;
    ChaseBounceBulletScript chaseBounceScript;

    GameObject[] BulletExists;
    // Use this for initialization
    void Start()
    {

        chaseBounceScript = bullet.GetComponent<ChaseBounceBulletScript>();

        deltaTime = 1.0f / rate;
    }

    // Update is called once per frame
    void Update()
    {
        BulletExists = GameObject.FindGameObjectsWithTag("BounceBullet");
        if (fire)
        {
            //if(BulletExists.Length <= 1)
            //{
            //    bullet.transform.position = playerPosition;
            //    bullet.transform.rotation = rotationQ;
            //    Instantiate(bullet);
            //}
            //print("Firing: " + currentTime + " " + deltaTime);
            if (currentTime > deltaTime)
            {
                bullet.transform.position = playerPosition;
                bullet.transform.rotation = rotationQ;
                Instantiate(bullet);
                currentTime = 0.0f;
            }
        }
        currentTime += Time.deltaTime;

    }

    public void SetFire(bool fire)
    {
        this.fire = fire;
    }
}
