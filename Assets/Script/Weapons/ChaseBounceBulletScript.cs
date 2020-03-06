using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBounceBulletScript : MonoBehaviour
{
    GameObject NearestEnemy,secondNearestEnemy;
    public float BulletSpeed;
    int TimesBounced;
    bool isCollided;
    float currentTime;

    GameObject[] gos;
    float[] distances;
    float closestDistance = 999, secondClosest = 999;
    int closestIndex, secondIndex;
    // Start is called before the first frame update
    void Start()
    {
        isCollided = false;
        TimesBounced = 0;
        NearestEnemy = null;
        secondNearestEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        //movement mode
        if (isCollided)
        {
            //print("collided");
            transform.LookAt(secondNearestEnemy.transform.position);
            transform.position += transform.forward * Time.deltaTime * BulletSpeed;
        }
        else
        {
            float x = BulletSpeed * Time.deltaTime;
            Vector3 position = transform.rotation * (new Vector3(x, 0, 0));

            transform.position = transform.position + position;
        }
        //max bounce times
        if(TimesBounced >= 2)
        {
            Destroy(this.gameObject);
        }
        //exists limit
        if (transform.position.x < -50 || transform.position.x > 50 ||
            transform.position.z < -50 || transform.position.z > 50 ||
            currentTime > 3)
        {
            Destroy(this.gameObject);
            currentTime = 0;
        }
        currentTime += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBoss")
        {
            TimesBounced++;
            isCollided = true;
            FindEnemy();
            transform.LookAt(secondNearestEnemy.transform.position);

        }
    }

    public void FindEnemy()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        distances = new float[gos.Length];
        for (int i = 0; i < gos.Length; i++)
        {
            distances[i] = FindDistance(gos[i], this.gameObject);
            if (distances[i] < closestDistance)
            {
                closestDistance = distances[i];
                closestIndex = i;
            }
            else if (distances[i] < secondClosest)
            {
                secondClosest = distances[i];
                secondIndex = i;
            }
        }
        secondNearestEnemy = gos[secondIndex];
    }

    float FindDistance(GameObject go, GameObject origin)
    {
        return Vector3.Distance(go.transform.position, origin.transform.position);
    }
}
