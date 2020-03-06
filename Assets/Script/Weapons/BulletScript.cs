using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10.0f;
    public Quaternion rotationQ;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = speed * Time.deltaTime;
        Vector3 position = transform.rotation * (new Vector3(x, 0, 0));

        transform.position = transform.position + position;

        if(transform.position.x < -50 || transform.position.x > 50 ||
            transform.position.z <-50 || transform.position.z > 50 ||
            currentTime > 3)
        {
            Destroy(this.gameObject);
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
