using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossWeaponScript : MonoBehaviour
{

    public float rate = 1.0f;

    float NdeltaTime = 0.0f;
    float currentTime = 0.0f;

    public Quaternion rotationQ;
    public GameObject BossBullet;
    public Vector3 BossWeaponPosition;
    BossBulletScript BossBulletScipt;
    // Start is called before the first frame update
    void Start()
    {
        BossBulletScipt = BossBullet.GetComponent<BossBulletScript>();

        NdeltaTime = 1.0f / rate;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > NdeltaTime)
        {
            BossBullet.transform.position = transform.position;
            //BossBullet.transform.rotation = rotationQ;
            Instantiate(BossBullet);
            currentTime = 0.0f;
        }
        currentTime += Time.deltaTime;
    }
}
