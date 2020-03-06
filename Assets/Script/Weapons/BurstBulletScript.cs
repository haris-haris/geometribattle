using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstBulletScript : MonoBehaviour
{

    public float speed = 10.0f;

    public Quaternion rotationQ;

    public Vector3 sourcePosition;

    public float maxDistance = 10.0f;

    // Use this for initialization
    void Start()
    {
        sourcePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = speed * Time.deltaTime;

        Vector3 position = transform.rotation * (new Vector3(x, 0, 0));

        transform.position = transform.position + position;

        float distance = (transform.position - sourcePosition).magnitude;

        if (distance >= maxDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
