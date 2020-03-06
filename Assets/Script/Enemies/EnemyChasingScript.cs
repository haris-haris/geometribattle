using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingScript : MonoBehaviour
{

    public GameObject playerTarget;

    float health = 75.0f;

    float InitialSpeed = 4.0f;
    public float ChasingSpeed = 30.0f;

    float movingTime,spawnTime,searchTime;

    bool isSearching, isChasing, isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        isSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //enemy states
        if (isSpawning)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime < 2)
                transform.position += transform.forward * Time.deltaTime * InitialSpeed;
            if(spawnTime >= 2)
            {
                isSpawning = false;
                isSearching = true;
                spawnTime = 0;
            }
        }
        if(isSearching)
        {
            searchTime += Time.deltaTime;
            transform.LookAt(playerTarget.transform.position);
            if (searchTime >= 10)
            {
                isSearching = false;
                isChasing = true;
                searchTime = 0;
            }
        }
        if(isChasing)
        {
            movingTime += Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * ChasingSpeed;

            //chase limit
            if (transform.position.x < -48 || transform.position.x > 48 ||
            transform.position.z < -48 || transform.position.z > 48 ||
            movingTime > 3.5f)
            {
                movingTime = 0;
                isChasing = false;
                isSearching = true;
            }
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject objectCollide = other.gameObject;
        if (objectCollide.tag == "bullet")
        {
            health -= 30;

            Destroy(objectCollide);
        }
        if (other.gameObject.tag == "BounceBullet")
        {
            health -= 40;
        }
    }
}
