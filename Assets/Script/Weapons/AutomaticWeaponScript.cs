using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeaponScript : MonoBehaviour
{
    bool fire;

    // amount of bullets per second
    public float rate = 1.0f;

    float deltaTime = 0.0f;
    float currentTime = 0.0f;

    public Quaternion rotationQ;
    public GameObject bullet;
    public Vector3 playerPosition;
    BulletScript bulletScipt;

    // Use this for initialization
    void Start()
    {

        bulletScipt = bullet.GetComponent<BulletScript>();

        deltaTime = 1.0f / rate;
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
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
