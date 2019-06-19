using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBulletScript : MonoBehaviour
{
    public float speed = 10.0f;
    public Quaternion rotationQ;
    float ExistTime;
    public float LimitTime = 5;
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

        ExistTime += Time.deltaTime;

        if (ExistTime >= LimitTime)
        {
            Destroy(this.gameObject);
        }
    }
}
